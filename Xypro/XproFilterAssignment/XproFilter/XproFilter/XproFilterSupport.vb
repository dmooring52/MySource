Imports System.Xml
Imports System.Data.SqlClient
Delegate Sub DoBindCallback()
Public Enum eState
    'Data fill states
    undefined
    empty
    filled
    filling
End Enum
Public Enum eDirection
    'Scrolling directions
    undefined
    up
    down
End Enum
Public Enum eCacheBuffer
    'Scrolling directions
    undefined
    up
    down
End Enum

Public Class ColumnInfo
    Public Shared Property lineNumber As Integer = 0
    Public Shared Property employeeId As Integer = 1
    Public Shared Property firstName As Integer = 2
    Public Shared Property lastName As Integer = 3
    Public Shared Property jobDescription As Integer = 4
    Public Shared Property departmentName As Integer = 5
    Public Shared Property managerId As Integer = 6
    Public Shared Property dateHired As Integer = 7
    Public Shared Property lastPasswordChange As Integer = 8

    Public Shared Property field_lineNumber As String = "lineNumber"
    Public Shared Property field_employeeId As String = "employeeId"
    Public Shared Property field_firstName As String = "firstName"
    Public Shared Property field_lastName As String = "lastName"
    Public Shared Property field_jobDescription As String = "jobDescription"
    Public Shared Property field_departmentName As String = "departmentName"
    Public Shared Property field_managerId As String = "managerId"
    Public Shared Property field_dateHired As String = "dateHired"
    Public Shared Property field_lastPasswordChange As String = "lastPasswordChange"
    Public Enum eConditionType
        undefined
        integerType
        textType
        dateType
    End Enum
    Public Shared integerTypes() As String = New String() {"Equal to", "Not equal to", "Greater than", "Greater than or equal to", "Less than", "Less than or equal to"}
    Public Shared stringTypes() As String = New String() {"Equal to", "Not equal to", "Contains", "Starts With", "Ends with"}
    Public Shared dateTypes() As String = New String() {"Equal to", "Not equal to", "After", "After or equal to", "Before", "Before or equal to"}
End Class
Public Class FilterClause
    Public Sub New(columnName As String, conditionType As ColumnInfo.eConditionType, condition As String, conditionValue As String, andOr As String)
        Me.columnName = columnName
        Me.conditionType = conditionType
        Me.condition = condition
        Me.conditionValue = conditionValue
        Me.andOr = andOr
    End Sub
    Public Property conditionType As ColumnInfo.eConditionType
    Public Property columnName As String
    Public Property condition As String
    Public Property conditionValue As String
    Public Property andOr As String
    Public ReadOnly Property sqlCondition As String
        Get
            Dim clause As String = ""
            If conditionType = ColumnInfo.eConditionType.textType Then
                If condition = ColumnInfo.stringTypes(0) Then
                    clause = String.Format(" {0} {1} = '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.stringTypes(1) Then
                    clause = String.Format(" {0} {1} <> '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.stringTypes(2) Then
                    clause = String.Format(" {0} {1} LIKE '%{2}%'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.stringTypes(3) Then
                    clause = String.Format(" {0} {1} LIKE '{2}%'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.stringTypes(4) Then
                    clause = String.Format(" {0} {1} LIKE '%{2}'", andOr, columnName, conditionValue)
                End If
            ElseIf conditionType = ColumnInfo.eConditionType.integerType Then
                If condition = ColumnInfo.integerTypes(0) Then
                    clause = String.Format(" {0} {1} = {2}", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.integerTypes(1) Then
                    clause = String.Format(" {0} {1} <> {2}", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.integerTypes(2) Then
                    clause = String.Format(" {0} {1} > {2}", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.integerTypes(3) Then
                    clause = String.Format(" {0} {1} >= {2}", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.integerTypes(4) Then
                    clause = String.Format(" {0} {1} < {2}", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.integerTypes(5) Then
                    clause = String.Format(" {0} {1} <= {2}", andOr, columnName, conditionValue)
                End If
            ElseIf conditionType = ColumnInfo.eConditionType.dateType Then
                If condition = ColumnInfo.dateTypes(0) Then
                    clause = String.Format(" {0} {1} = '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.dateTypes(1) Then
                    clause = String.Format(" {0} {1} <> '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.dateTypes(2) Then
                    clause = String.Format(" {0} {1} > '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.dateTypes(3) Then
                    clause = String.Format(" {0} {1} >= '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.dateTypes(4) Then
                    clause = String.Format(" {0} {1} < '{2}'", andOr, columnName, conditionValue)
                ElseIf condition = ColumnInfo.dateTypes(5) Then
                    clause = String.Format(" {0} {1} <= '{2}'", andOr, columnName, conditionValue)
                End If
            End If
            Return clause
        End Get
    End Property

End Class
Public Class ThreadManagerInfo
    Private _currentCacheBuffer As CacheBuffer
    Public Sub New(aCacheBuffer As CacheBuffer, direction As eDirection)
        CurrentCacheBuffer = aCacheBuffer
        CacheDirection = direction
    End Sub

    Public Property CurrentCacheBuffer As CacheBuffer
        Get
            Return _currentCacheBuffer
        End Get
        Set(value As CacheBuffer)
            _currentCacheBuffer = value
        End Set
    End Property
    Public Property CacheDirection As eDirection
End Class
Public Class FilterState
    'Repository for cache, view and database pointers and sizes
    Public Sub New()
        CurrentState = eState.undefined
        cacheListSize = 0
        cacheListPtr = 0
        viewListSize = 0
        viewListPtr = 0
        dataSize = 0
        dataPtr = 0
        connectionString = ""
        tableName = ""
    End Sub
    Public Property CacheBufferQueued As ThreadManagerInfo
    Public Property CurrentState As eState
    Public Property cacheListSize As Long
    Public Property cacheListPtr As Long
    Public Property viewListSize As Long
    Public Property viewListPtr As Long
    Public Property dataSize As Long
    Public Property dataPtr As Long
    Public Property connectionString As String
    Public Property tableName As String
End Class

Public Class Employee
    'The container for Employee records
    'Properties are named exactly as the SQL table
    'There is an extra Property for record numbers
    Public Property lineNumber As Long
    Public Property employeeId As Integer
    Public Property firstName As String
    Public Property lastName As String
    Public Property jobDescription As String
    Public Property departmentName As String
    Public Property managerId As Integer
    Public Property dateHired As DateTime
    Public Property lastPasswordChange As DateTime
    Public Sub New()
    End Sub
End Class
Public Class CacheBuffer
    'Buffers for current filled and for previous and next buffers that are filling
    Public currentState As eState = eState.undefined
    Public queueDirection As eDirection = eDirection.undefined

    Public filledSize As Integer = 0
    Public cacheList As List(Of Employee)
    Public Sub New(cacheSize As Long, direction As eDirection)
        cacheList = New List(Of Employee)
        currentState = eState.empty
        queueDirection = direction
        filledSize = 0
        For cnt As Integer = 0 To cacheSize - 1
            cacheList.Add(New Employee())
        Next
    End Sub
    Public Sub LoadXml(node As XmlNode, row As Integer, line As Long)
        If (IsNothing(cacheList) = False And IsNothing(node) = False And row >= 0 And row < cacheList.Count()) Then
            Dim empl As Employee = cacheList(row)
            empl.lineNumber = line.ToString()
            empl.employeeId = XmlUtility.GetXmlInteger(node, "employeeId")
            empl.firstName = XmlUtility.GetXmlString(node, "firstName")
            empl.lastName = XmlUtility.GetXmlString(node, "lastName")
            empl.jobDescription = XmlUtility.GetXmlString(node, "jobDescription")
            empl.departmentName = XmlUtility.GetXmlString(node, "departmentName")
            empl.managerId = XmlUtility.GetXmlInteger(node, "managerId")
            empl.dateHired = XmlUtility.GetXmlDateTime(node, "dateHired")
            empl.lastPasswordChange = XmlUtility.GetXmlDateTime(node, "lastPasswordChange")
        End If
    End Sub
End Class

Public Class DataLoader
    Private _fState As FilterState
    Private _clauseText As String
    Private _loadDataPtr As Long
    Public Event XEvent()

    Public Sub New(fState As FilterState, clauseText As String, loadDataPtr As Long)
        _fState = fState
        _clauseText = clauseText
        _loadDataPtr = loadDataPtr
    End Sub
    Public Sub SetLoader()
        Using sqlconn = New SqlConnection(XproFilterForm.CONNECTION_STRING)
            sqlconn.Open()
            Using sqlcomm = New SqlCommand()
                sqlcomm.Connection = sqlconn
                sqlcomm.CommandType = CommandType.Text

                sqlcomm.CommandText = String.Format("Select Count(employeeId) from {0} {1}", _fState.tableName, _clauseText)
                Dim datacount = sqlcomm.ExecuteScalar()
                _fState.dataSize = datacount
                sqlcomm.CommandText = String.Format("Select * from {0} {1} ORDER BY employeeId OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY FOR XML PATH ('Record'), ROOT ('Records')", _fState.tableName, _clauseText, _loadDataPtr, _fState.cacheListSize)
                _fState.CacheBufferQueued.CurrentCacheBuffer.currentState = eState.filling

                Dim xmlstring As String = ""
                Dim o As Object = Nothing
                Dim nrow As Long = 0
                Using mySqlDataReader = sqlcomm.ExecuteReader()
                    While (mySqlDataReader.Read())
                        o = mySqlDataReader.GetValue(0)
                        If (Not IsNothing(o)) Then
                            xmlstring += o.ToString()
                        End If
                    End While
                    mySqlDataReader.Close()
                End Using
                If xmlstring.Length > 0 Then
                    Dim doc As XmlDocument
                    Dim nodes As XmlNodeList
                    doc = New XmlDocument()
                    doc.LoadXml(xmlstring)
                    nodes = doc.SelectNodes("/Records/Record")

                    For Each node As XmlNode In nodes
                        nrow += 1
                        _fState.CacheBufferQueued.CurrentCacheBuffer.LoadXml(node, nrow - 1, _loadDataPtr + nrow)
                    Next
                End If
                _fState.CacheBufferQueued.CurrentCacheBuffer.filledSize = nrow
                _fState.CacheBufferQueued.CurrentCacheBuffer.currentState = eState.filled
                RaiseEvent XEvent()
            End Using
        End Using
    End Sub
End Class