Public Class FilterCollectorForm
    Public integerTypes1() As String = ColumnInfo.integerTypes.Clone()
    Public stringTypes1() As String = ColumnInfo.stringTypes.Clone()
    Public dateTypes1() As String = ColumnInfo.dateTypes.Clone()
    Public integerTypes2() As String = ColumnInfo.integerTypes.Clone()
    Public stringTypes2() As String = ColumnInfo.stringTypes.Clone()
    Public dateTypes2() As String = ColumnInfo.dateTypes.Clone()
    Public controlState As List(Of String)
    Private _conditionType As ColumnInfo.eConditionType
    Public Sub New(conditionType As ColumnInfo.eConditionType, tagState As List(Of String))
        ' This call is required by the designer.
        InitializeComponent()

        _conditionType = conditionType
        controlState = tagState
    End Sub
    Private Sub FilterCollectorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        radioAnd.Checked = True
        If (_conditionType = ColumnInfo.eConditionType.textType) Then
            cmbCondition1.DataSource = stringTypes1
            cmbCondition2.DataSource = stringTypes2
        ElseIf (_conditionType = ColumnInfo.eConditionType.integerType) Then
            cmbCondition1.DataSource = integerTypes1
            cmbCondition2.DataSource = integerTypes2
        ElseIf (_conditionType = ColumnInfo.eConditionType.dateType) Then
            cmbCondition1.DataSource = dateTypes1
            cmbCondition2.DataSource = dateTypes2
        End If
        If IsNothing(controlState) = False Then
            If cmbCondition1.Items.Count() > 0 And IsNothing(controlState) = False Then
                If controlState.Count() > 1 Then
                    For Each itm As String In cmbCondition1.Items
                        If itm.Trim() = controlState(0) Then
                            cmbCondition1.SelectedItem = itm
                        End If
                    Next
                    txtCondition1.Text = controlState(1)
                End If
                If controlState.Count() > 3 Then
                    For Each itm As String In cmbCondition2.Items
                        If itm.Trim() = controlState(2) Then
                            cmbCondition2.SelectedItem = itm
                        End If
                    Next
                    txtCondition2.Text = controlState(3)
                End If
                If (controlState.Count() = 5) Then
                    If controlState(4) = "AND" Then
                        radioAnd.Checked = True
                    ElseIf controlState(4) = "OR" Then
                        radioOr.Checked = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FilterCollectorForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        controlState = New List(Of String)
        If ClosingUp(cmbCondition1, txtCondition1) = False Then
            MsgBox("Invalid Value Entered")
            e.Cancel = True
            Return
        ElseIf controlState.Count() = 2 Then
            If ClosingUp(cmbCondition2, txtCondition2) = False Then
                MsgBox("Invalid Value Entered")
                e.Cancel = True
                Return
            End If
        End If
        If (controlState.Count() < 2) Then
            controlState = Nothing
        End If
    End Sub
    Private Function ClosingUp(cmb As ComboBox, txt As TextBox) As Boolean
        If Not IsNothing(cmb.SelectedItem) Then
            Dim selectedString As String = cmb.SelectedItem
            If IsNothing(selectedString) = False And selectedString.Length > 0 And IsNothing(txt.Text) = False And txt.Text.Trim().Length > 0 Then

                If (_conditionType = ColumnInfo.eConditionType.integerType) Then
                    If IsValidInteger(txt.Text.Trim()) = False Then
                        Return False
                    End If
                ElseIf (_conditionType = ColumnInfo.eConditionType.dateType) Then
                    Dim validDate As String
                    validDate = IsValidDate(txt.Text)
                    If validDate.Length = 0 Then
                        Return False
                    Else
                        txt.Text = validDate
                    End If
                End If

                If Not _conditionType = ColumnInfo.eConditionType.undefined Then
                    controlState.Add(selectedString)
                    controlState.Add(XmlUtility.SqlString(txt.Text.Trim()))
                    If (controlState.Count() = 4) Then
                        If radioOr.Checked = True Then
                            controlState.Add("OR")
                        Else
                            controlState.Add("AND")
                        End If
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Private Function IsValidInteger(txt As String) As Boolean
        Dim outInteger As Integer
        If IsNothing(txt) = False And txt.Trim().Length > 0 Then
            If (Integer.TryParse(txt, outInteger)) Then
                Return True
            End If
        End If
        Return False
    End Function
    Private Function IsValidDate(txt As String) As String
        Dim outDate As DateTime
        If IsNothing(txt) = False And txt.Trim().Length > 0 Then
            If (DateTime.TryParse(txt, outDate)) Then
                Return outDate
            End If
        End If
        Return ""
    End Function

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtCondition1.Text = ""
        txtCondition2.Text = ""
        Close()
    End Sub
End Class