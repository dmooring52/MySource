Imports System.Data.SqlClient
Imports System.Xml
Imports System.Threading

Public Class XproFilterForm
#Region "Global fields"
    'Public Shared CONNECTION_STRING As String = "Server=DMMLENOVO1;Database=Xypro;Trusted_Connection=true"
    Public Shared CONNECTION_STRING As String = "Data Source=SQL5012.Smarterasp.net;Initial Catalog=DB_9BA515_LocalSingingClub;User Id=DB_9BA515_LocalSingingClub_admin;Password=Groovy52!;"
    Public Shared TABLE_NAME As String = "Employees"
    Public Shared CACHE_LIST_SIZE As Long = 5000
    Public Shared VIEW_LIST_SIZE As Long = 20
    Public Shared ReadOnly INITIAL_SCROLL_DELAY = 100
    Public Shared ReadOnly SCROLL_DELAY = 100
    Public Shared ReadOnly NUMBER_OF_COLUMNS = 9
    Private Shared loadingCache As Queue = New Queue()

    Private _currentCacheList As CacheBuffer
    Private _upCacheList As CacheBuffer
    Private _downCacheList As CacheBuffer

    Private _threadPage As Thread
    Private WithEvents _pageDataLoader As DataLoader

    Private _clauseList As List(Of FilterClause)
    Private _viewList As List(Of Employee)
    Private _fState As FilterState
#End Region
#Region "Form and Key Timer Event Handlers"
    Private Sub XproFilterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCacheSize.Text = CACHE_LIST_SIZE.ToString()
        txtPageSize.Text = VIEW_LIST_SIZE.ToString()
        txtConnectionString.Text = CONNECTION_STRING
        txtTable.Text = TABLE_NAME
        KeyTimer.Enabled = True
        KeyTimer.Stop()
        dataGrid.AllowUserToAddRows = False
        _fState = New FilterState()
        If UserSettings() Then
            SetInitialLoad()
        End If
    End Sub
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        If UserSettings() Then
            Dim clauseText As String = GetFilterClause()
            ReLoadClause(clauseText)
        End If
    End Sub
    Private Function UserSettings() As Boolean
        If txtConnectionString.Text.Trim().Length > 0 Then
            _fState.connectionString = txtConnectionString.Text.Trim()
        End If
        If txtTable.Text.Trim().Length > 0 Then
            _fState.tableName = txtTable.Text.Trim()
        End If
        Dim tps As String = ""
        Dim tcs As String = ""
        Dim itps As Long = -1
        Dim itcs As Long = -1
        Dim IsOk As Boolean = False
        If txtPageSize.Text.Trim().Length > 0 Then
            tps = txtPageSize.Text.Trim()
        End If
        If txtCacheSize.Text.Trim().Length > 0 Then
            tcs = txtCacheSize.Text.Trim()
        End If
        If tps.Trim().Length > 0 And tcs.Trim().Length > 0 Then
            If Long.TryParse(tps, itps) And Long.TryParse(tcs, itcs) Then
                If (itps >= 20 And itcs >= 100 And (itps * 4) < itcs) Then
                    _fState.cacheListSize = itcs
                    _fState.viewListSize = itps
                    IsOk = True
                End If
            End If
        End If
        If Not IsOk Then
            txtCacheSize.Text = CACHE_LIST_SIZE.ToString()
            txtPageSize.Text = VIEW_LIST_SIZE.ToString()
            MsgBox("Invalid Sizes - cache size must be at least 100 and \npage size must be at least 10 and \ncache must be at least 4 times larger than page")
            Return False
        End If
        Return True
    End Function
    Private Function GetFilterClause() As String
        Dim tagState As List(Of String)
        Dim clauseList As List(Of FilterClause) = New List(Of FilterClause)
        tagState = dataGrid.Columns(ColumnInfo.employeeId).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_employeeId, ColumnInfo.eConditionType.integerType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.firstName).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_firstName, ColumnInfo.eConditionType.textType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.lastName).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_lastName, ColumnInfo.eConditionType.textType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.jobDescription).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_jobDescription, ColumnInfo.eConditionType.textType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.departmentName).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_departmentName, ColumnInfo.eConditionType.textType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.managerId).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_managerId, ColumnInfo.eConditionType.integerType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.dateHired).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_dateHired, ColumnInfo.eConditionType.dateType)
        End If
        tagState = dataGrid.Columns(ColumnInfo.lastPasswordChange).HeaderCell.Tag
        If (IsNothing(tagState) = False AndAlso tagState.Count() > 1) Then
            AddClause(clauseList, tagState, ColumnInfo.field_lastPasswordChange, ColumnInfo.eConditionType.dateType)
        End If
        Dim clauseText As String = ""
        If (clauseList.Count() > 0) Then
            clauseText = clauseList(0).sqlCondition
            clauseText = "WHERE" + clauseText.Substring(4)
            If (clauseList.Count() > 1) Then
                For cnt As Integer = 1 To clauseList.Count() - 1
                    clauseText += clauseList(cnt).sqlCondition
                Next
            End If
        End If
        Return clauseText
    End Function
    Private Sub AddClause(clauseList As List(Of FilterClause), tagState As List(Of String), columnName As String, conditionType As ColumnInfo.eConditionType)
        clauseList.Add(New FilterClause(columnName, conditionType, tagState(0), tagState(1), "AND"))
        If (tagState.Count() > 3) Then
            Dim AndOr As String = "AND"
            If (tagState.Count() > 4) Then
                If (tagState(4) = "OR") Then
                    AndOr = "OR"
                End If
            End If
            clauseList.Add(New FilterClause(columnName, conditionType, tagState(2), tagState(3), AndOr))
        End If
    End Sub
    Private Sub KeyTimer_Tick(sender As Object, e As EventArgs) Handles KeyTimer.Tick
        KeyTimer.Interval = SCROLL_DELAY
        If Not IsNothing(KeyTimer.Tag) Then
            If KeyTimer.Tag = Keys.Up Then
                KeyHandlerArrow(eDirection.up)
            ElseIf KeyTimer.Tag = Keys.PageUp Then
                KeyHandlerPage(eDirection.up)
            ElseIf KeyTimer.Tag = Keys.Down Then
                KeyHandlerArrow(eDirection.down)
            ElseIf KeyTimer.Tag = Keys.PageDown Then
                KeyHandlerPage(eDirection.down)
            End If
        End If
        KeyTimer.Start()
    End Sub
#End Region
#Region "Thread Tasks"
    Private Sub PageTask(loadDataPtr As Long)
        If loadingCache.Count = 0 Then
            _clauseList = New List(Of FilterClause)
            Dim clauseText As String = GetFilterClause()
            _pageDataLoader = New DataLoader(_fState, clauseText, loadDataPtr)
            _threadPage = New Thread(New ThreadStart(AddressOf _pageDataLoader.SetLoader))
            loadingCache.Enqueue(New String("Hello"))
            _threadPage.Start()
        End If
    End Sub
    Sub _pageDataLoader_EventHandler() Handles _pageDataLoader.XEvent
        If (_fState.CacheBufferQueued.CacheDirection = eCacheBuffer.up) Then
            statusPanel.Items(0).Text = "UP"
        ElseIf (_fState.CacheBufferQueued.CacheDirection = eCacheBuffer.down) Then
            statusPanel.Items(1).Text = "DOWN"
        End If
        loadingCache.Dequeue()
        DoBind()
    End Sub
#End Region
#Region "Form and data support methods"
    Private Sub SetInitialLoad()
        _fState.dataPtr = 0
        _upCacheList = New CacheBuffer(_fState.cacheListSize, eCacheBuffer.up)
        _upCacheList.currentState = eState.empty
        _downCacheList = New CacheBuffer(_fState.cacheListSize, eCacheBuffer.down)
        _downCacheList.currentState = eState.empty
        _currentCacheList = _upCacheList
        _fState.CacheBufferQueued = New ThreadManagerInfo(_currentCacheList, eDirection.up)
        PageTask(0)
    End Sub

    Private Sub ReLoadClause(clauseText As String)
        KeyTimer.Stop()
        _fState.dataPtr = 0
        _fState.cacheListPtr = 0
        _fState.viewListPtr = 0
        _upCacheList = New CacheBuffer(_fState.cacheListSize, eCacheBuffer.up)
        _upCacheList.currentState = eState.empty
        _downCacheList = New CacheBuffer(_fState.cacheListSize, eCacheBuffer.down)
        _downCacheList.currentState = eState.empty
        _currentCacheList = _upCacheList
        _fState.CacheBufferQueued = New ThreadManagerInfo(_currentCacheList, eDirection.up)
        PageTask(0)
    End Sub
    Private Sub ReloadCache(direction As eDirection)
        KeyTimer.Stop()
        Dim tempCacheUp As CacheBuffer = _upCacheList
        Dim tempCacheDown As CacheBuffer = _downCacheList
        If (direction = eDirection.up) Then
            _fState.dataPtr -= _fState.cacheListSize
            If _fState.dataPtr < 0 Then
                _fState.cacheListPtr = 0
                _fState.dataPtr = 0
            Else
                _upCacheList = tempCacheDown
                _downCacheList = tempCacheUp
                _upCacheList.queueDirection = eDirection.up
                _downCacheList.queueDirection = eDirection.up
                _currentCacheList = _upCacheList
                DoBind()
            End If
        ElseIf (direction = eDirection.down) Then
            _fState.dataPtr += _fState.cacheListSize
            If _fState.dataPtr >= _fState.dataSize Then
                _fState.dataPtr = _fState.dataSize
                _fState.cacheListPtr = _fState.cacheListSize
            Else
                _upCacheList = tempCacheDown
                _downCacheList = tempCacheUp
                _upCacheList.queueDirection = eDirection.down
                _downCacheList.queueDirection = eDirection.down
                _currentCacheList = _upCacheList
                DoBind()
            End If
        End If
    End Sub
#End Region
#Region "dataGrid Event Handlers"
    Private Sub dataGrid_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles dataGrid.CellValueNeeded
        e.Value = GetRow(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub dataGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles dataGrid.KeyDown
        If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp) Then
            If dataGrid.CurrentRow.Index = 0 Then
                KeyTimer.Tag = e.KeyCode
                KeyTimer.Interval = SCROLL_DELAY
                KeyTimer.Start()
            End If
        ElseIf (e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown) Then
            If (dataGrid.CurrentRow.Index >= dataGrid.Rows.Count() - 1) Then
                KeyTimer.Tag = e.KeyCode
                KeyTimer.Interval = SCROLL_DELAY
                KeyTimer.Start()
            End If
        End If
    End Sub
    Private Sub dataGrid_KeyUp(sender As Object, e As KeyEventArgs) Handles dataGrid.KeyUp
        If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown) Then
            KeyTimer.Stop()
            KeyTimer.Interval = INITIAL_SCROLL_DELAY
            KeyTimer.Tag = Nothing
        End If
    End Sub
    Private Sub KeyHandlerArrow(direction As eDirection)
        If direction = eDirection.up Then
            If dataGrid.CurrentRow.Index = 0 Then
                If (_fState.cacheListPtr > 0) Then
                    _fState.cacheListPtr -= 1
                    CheckCaching(direction)
                    DoBind()
                Else
                    If loadingCache.Count = 0 Then
                        _fState.cacheListPtr = _fState.viewListSize
                        ReloadCache(direction)
                    End If
                End If
            End If
        ElseIf direction = eDirection.down Then
            If dataGrid.CurrentRow.Index = dataGrid.RowCount - 1 Then
                If (_fState.cacheListPtr < (_fState.cacheListSize - _fState.viewListSize)) Then
                    _fState.cacheListPtr += 1
                    CheckCaching(direction)
                    DoBind()
                Else
                    If loadingCache.Count = 0 Then
                        _fState.cacheListPtr = 0
                        ReloadCache(direction)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub KeyHandlerPage(direction As eDirection)
        If direction = eDirection.up Then
            If (_fState.cacheListPtr >= 0) Then
                If _fState.cacheListPtr >= _fState.viewListSize Then
                    _fState.cacheListPtr -= _fState.viewListSize
                    CheckCaching(direction)
                    DoBind()
                Else
                    If loadingCache.Count = 0 Then
                        _fState.cacheListPtr = _fState.cacheListSize - _fState.viewListSize
                        ReloadCache(direction)
                    End If
                End If
            End If
        ElseIf direction = eDirection.down Then
            If (_fState.dataPtr + _fState.cacheListPtr + _fState.viewListSize < _fState.dataSize) Then
                If _fState.cacheListPtr + _fState.viewListSize <= _fState.cacheListSize Then
                    If (_fState.cacheListPtr + _fState.viewListSize) <= (_fState.cacheListSize - _fState.viewListSize) Then
                        _fState.cacheListPtr += _fState.viewListSize
                        CheckCaching(direction)
                        DoBind()
                    Else
                        If loadingCache.Count = 0 Then
                            _fState.cacheListPtr = 0
                            ReloadCache(direction)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub CheckCaching(direction As eDirection)
        If direction = eDirection.down Then
            Dim percentScroll As Double = CType(_fState.cacheListPtr / _fState.cacheListSize, Double)
            If (percentScroll > 0.75) Then
                If loadingCache.Count = 0 Then
                    If (_fState.dataPtr + _fState.cacheListSize < _fState.dataSize) Then
                        _fState.CacheBufferQueued = New ThreadManagerInfo(_downCacheList, eDirection.down)
                        PageTask(_fState.dataPtr + _fState.cacheListSize)
                        statusPanel.Items(1).Text = _fState.cacheListPtr.ToString()
                        statusPanel.Items(2).Text = percentScroll.ToString()
                    End If
                End If
            End If
        ElseIf direction = eDirection.up Then
            Dim percentScroll As Double = CType(_fState.cacheListPtr / _fState.cacheListSize, Double)
            If (percentScroll < 0.25) Then
                If loadingCache.Count = 0 Then
                    If (_fState.dataPtr - _fState.cacheListSize >= 0) Then
                        _fState.CacheBufferQueued = New ThreadManagerInfo(_downCacheList, eDirection.up)
                        PageTask(_fState.dataPtr - _fState.cacheListSize)
                        statusPanel.Items(1).Text = _fState.cacheListPtr.ToString()
                        statusPanel.Items(2).Text = percentScroll.ToString()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub dataGrid_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dataGrid.ColumnHeaderMouseClick
        Dim rect As Rectangle = dataGrid.GetCellDisplayRectangle(e.ColumnIndex, 0, False)
        rect.X += dataGrid.Left
        rect.Y += dataGrid.Top

        Dim onCondition As ColumnInfo.eConditionType = ColumnInfo.eConditionType.undefined
        If e.ColumnIndex = ColumnInfo.employeeId Then
            onCondition = ColumnInfo.eConditionType.integerType
        ElseIf e.ColumnIndex = ColumnInfo.firstName Then
            onCondition = ColumnInfo.eConditionType.textType
        ElseIf e.ColumnIndex = ColumnInfo.lastName Then
            onCondition = ColumnInfo.eConditionType.textType
        ElseIf e.ColumnIndex = ColumnInfo.jobDescription Then
            onCondition = ColumnInfo.eConditionType.textType
        ElseIf e.ColumnIndex = ColumnInfo.departmentName Then
            onCondition = ColumnInfo.eConditionType.textType
        ElseIf e.ColumnIndex = ColumnInfo.managerId Then
            onCondition = ColumnInfo.eConditionType.integerType
        ElseIf e.ColumnIndex = ColumnInfo.dateHired Then
            onCondition = ColumnInfo.eConditionType.dateType
        ElseIf e.ColumnIndex = ColumnInfo.lastPasswordChange Then
            onCondition = ColumnInfo.eConditionType.dateType
        End If

        Dim tagState As List(Of String)
        tagState = dataGrid.Columns(e.ColumnIndex).HeaderCell.Tag
        If Not onCondition = ColumnInfo.eConditionType.undefined Then
            Dim formConditions As FilterCollectorForm = New FilterCollectorForm(onCondition, tagState)
            formConditions.StartPosition = FormStartPosition.Manual
            formConditions.Location = PointToScreen(New Point(rect.X, rect.Y))
            formConditions.ShowDialog()
            If (IsNothing(formConditions.controlState)) Then
                dataGrid.Columns(e.ColumnIndex).HeaderCell.Style.Font = New Font("Arial", 8, FontStyle.Regular)
                dataGrid.Columns(e.ColumnIndex).HeaderCell.Tag = Nothing
            Else
                dataGrid.Columns(e.ColumnIndex).HeaderCell.Style.Font = New Font("Arial", 8, FontStyle.Bold Or FontStyle.Italic)
                dataGrid.Columns(e.ColumnIndex).HeaderCell.Tag = formConditions.controlState
            End If
        End If
    End Sub
#End Region
#Region "dataGrid support methods"
    Private Function GetRow(nrow As Integer, ncol As Integer)
        If (nrow < _viewList.Count() And ncol < NUMBER_OF_COLUMNS) Then
            If ncol = ColumnInfo.lineNumber Then
                Return _viewList(nrow).lineNumber.ToString()
            ElseIf ncol = ColumnInfo.employeeId Then
                Return _viewList(nrow).employeeId.ToString()
            ElseIf ncol = ColumnInfo.firstName Then
                Return _viewList(nrow).firstName
            ElseIf ncol = ColumnInfo.lastName Then
                Return _viewList(nrow).lastName
            ElseIf ncol = ColumnInfo.jobDescription Then
                Return _viewList(nrow).jobDescription
            ElseIf ncol = ColumnInfo.departmentName Then
                Return _viewList(nrow).departmentName
            ElseIf ncol = ColumnInfo.managerId Then
                Return _viewList(nrow).managerId.ToString()
            ElseIf ncol = ColumnInfo.dateHired Then
                Return _viewList(nrow).dateHired.ToString()
            ElseIf ncol = ColumnInfo.lastPasswordChange Then
                Return _viewList(nrow).lastPasswordChange.ToString()
            End If
        End If
        Return ""
    End Function
    Private Sub DoBind()
        If (_currentCacheList.currentState = eState.filled) Then
            If Me.dataGrid.InvokeRequired Then
                Dim d As New DoBindCallback(AddressOf DoBinding)
                Me.Invoke(d)
            Else
                DoBinding()
            End If
        End If
    End Sub
    Private Sub DoBinding()
        _viewList = New List(Of Employee)
        If (_currentCacheList.currentState = eState.filled) Then
            For cnt As Integer = _fState.cacheListPtr To _fState.cacheListPtr + _fState.viewListSize - 1
                If cnt < _currentCacheList.filledSize Then
                    _viewList.Add(_currentCacheList.cacheList(cnt))
                End If
            Next
            If dataGrid.Rows.Count() <> _fState.viewListSize Then
                dataGrid.Rows.Clear()
                If (_fState.viewListSize > 0) Then
                    dataGrid.Rows.Add(CType(_fState.viewListSize, Integer))
                End If
            Else
                For cnt As Integer = 0 To _fState.viewListSize - 1
                    dataGrid.InvalidateRow(cnt)
                Next
            End If
        End If
    End Sub
#End Region
End Class
