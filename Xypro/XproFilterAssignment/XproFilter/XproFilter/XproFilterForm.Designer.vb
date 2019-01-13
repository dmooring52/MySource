<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XproFilterForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.dataGrid = New System.Windows.Forms.DataGridView()
        Me.lineNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.employeeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.firstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jobDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.departmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.managerId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dateHired = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lastPasswordChange = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.statusPanel = New System.Windows.Forms.StatusStrip()
        Me.statusLabelOne = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusLabelTwo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.statusLabelThree = New System.Windows.Forms.ToolStripStatusLabel()
        Me.KeyTimer = New System.Windows.Forms.Timer(Me.components)
        Me.lblConnectionString = New System.Windows.Forms.Label()
        Me.txtConnectionString = New System.Windows.Forms.TextBox()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.txtTable = New System.Windows.Forms.TextBox()
        Me.txtPageSize = New System.Windows.Forms.TextBox()
        Me.lblPageSize = New System.Windows.Forms.Label()
        Me.txtCacheSize = New System.Windows.Forms.TextBox()
        Me.lblCacheSize = New System.Windows.Forms.Label()
        Me.statusLabelFour = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(12, 45)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(75, 23)
        Me.btnFilter.TabIndex = 0
        Me.btnFilter.Text = "Filter"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'dataGrid
        '
        Me.dataGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.lineNumber, Me.employeeId, Me.firstName, Me.lastName, Me.jobDescription, Me.departmentName, Me.managerId, Me.dateHired, Me.lastPasswordChange})
        Me.dataGrid.Location = New System.Drawing.Point(12, 74)
        Me.dataGrid.MinimumSize = New System.Drawing.Size(947, 418)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.Size = New System.Drawing.Size(947, 493)
        Me.dataGrid.TabIndex = 1
        Me.dataGrid.VirtualMode = True
        '
        'lineNumber
        '
        Me.lineNumber.Frozen = True
        Me.lineNumber.HeaderText = "Line"
        Me.lineNumber.Name = "lineNumber"
        Me.lineNumber.Width = 75
        '
        'employeeId
        '
        Me.employeeId.DataPropertyName = "employeeId"
        Me.employeeId.HeaderText = "Employee ID"
        Me.employeeId.Name = "employeeId"
        '
        'firstName
        '
        Me.firstName.DataPropertyName = "firstName"
        Me.firstName.HeaderText = "First Name"
        Me.firstName.Name = "firstName"
        '
        'lastName
        '
        Me.lastName.DataPropertyName = "lastName"
        Me.lastName.HeaderText = "Last Name"
        Me.lastName.Name = "lastName"
        '
        'jobDescription
        '
        Me.jobDescription.DataPropertyName = "jobDescription"
        Me.jobDescription.HeaderText = "Job Description"
        Me.jobDescription.Name = "jobDescription"
        '
        'departmentName
        '
        Me.departmentName.DataPropertyName = "departmentName"
        Me.departmentName.HeaderText = "Department"
        Me.departmentName.Name = "departmentName"
        '
        'managerId
        '
        Me.managerId.DataPropertyName = "managerId"
        Me.managerId.HeaderText = "Manager ID"
        Me.managerId.Name = "managerId"
        '
        'dateHired
        '
        Me.dateHired.DataPropertyName = "dateHired"
        Me.dateHired.HeaderText = "Hire Date"
        Me.dateHired.Name = "dateHired"
        '
        'lastPasswordChange
        '
        Me.lastPasswordChange.DataPropertyName = "lastPasswordChange"
        Me.lastPasswordChange.HeaderText = "Last Password Change Date"
        Me.lastPasswordChange.Name = "lastPasswordChange"
        '
        'statusPanel
        '
        Me.statusPanel.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusLabelOne, Me.statusLabelTwo, Me.statusLabelThree, Me.statusLabelFour})
        Me.statusPanel.Location = New System.Drawing.Point(0, 570)
        Me.statusPanel.Name = "statusPanel"
        Me.statusPanel.Size = New System.Drawing.Size(971, 22)
        Me.statusPanel.TabIndex = 2
        Me.statusPanel.Text = "statusPanel"
        '
        'statusLabelOne
        '
        Me.statusLabelOne.Name = "statusLabelOne"
        Me.statusLabelOne.Size = New System.Drawing.Size(0, 17)
        '
        'statusLabelTwo
        '
        Me.statusLabelTwo.Name = "statusLabelTwo"
        Me.statusLabelTwo.Size = New System.Drawing.Size(0, 17)
        '
        'statusLabelThree
        '
        Me.statusLabelThree.Name = "statusLabelThree"
        Me.statusLabelThree.Size = New System.Drawing.Size(0, 17)
        '
        'KeyTimer
        '
        Me.KeyTimer.Enabled = True
        Me.KeyTimer.Interval = 2000
        '
        'lblConnectionString
        '
        Me.lblConnectionString.AutoSize = True
        Me.lblConnectionString.Location = New System.Drawing.Point(12, 9)
        Me.lblConnectionString.Name = "lblConnectionString"
        Me.lblConnectionString.Size = New System.Drawing.Size(61, 13)
        Me.lblConnectionString.TabIndex = 3
        Me.lblConnectionString.Text = "Connection"
        '
        'txtConnectionString
        '
        Me.txtConnectionString.Location = New System.Drawing.Point(79, 6)
        Me.txtConnectionString.Name = "txtConnectionString"
        Me.txtConnectionString.Size = New System.Drawing.Size(365, 20)
        Me.txtConnectionString.TabIndex = 4
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(450, 9)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(34, 13)
        Me.lblTable.TabIndex = 5
        Me.lblTable.Text = "Table"
        '
        'txtTable
        '
        Me.txtTable.Location = New System.Drawing.Point(490, 6)
        Me.txtTable.Name = "txtTable"
        Me.txtTable.Size = New System.Drawing.Size(179, 20)
        Me.txtTable.TabIndex = 6
        '
        'txtPageSize
        '
        Me.txtPageSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPageSize.Location = New System.Drawing.Point(742, 6)
        Me.txtPageSize.Name = "txtPageSize"
        Me.txtPageSize.Size = New System.Drawing.Size(59, 20)
        Me.txtPageSize.TabIndex = 8
        '
        'lblPageSize
        '
        Me.lblPageSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPageSize.AutoSize = True
        Me.lblPageSize.Location = New System.Drawing.Point(702, 9)
        Me.lblPageSize.Name = "lblPageSize"
        Me.lblPageSize.Size = New System.Drawing.Size(32, 13)
        Me.lblPageSize.TabIndex = 7
        Me.lblPageSize.Text = "Page"
        '
        'txtCacheSize
        '
        Me.txtCacheSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCacheSize.Location = New System.Drawing.Point(853, 6)
        Me.txtCacheSize.Name = "txtCacheSize"
        Me.txtCacheSize.Size = New System.Drawing.Size(59, 20)
        Me.txtCacheSize.TabIndex = 10
        '
        'lblCacheSize
        '
        Me.lblCacheSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCacheSize.AutoSize = True
        Me.lblCacheSize.Location = New System.Drawing.Point(809, 9)
        Me.lblCacheSize.Name = "lblCacheSize"
        Me.lblCacheSize.Size = New System.Drawing.Size(38, 13)
        Me.lblCacheSize.TabIndex = 9
        Me.lblCacheSize.Text = "Cache"
        '
        'statusLabelFour
        '
        Me.statusLabelFour.Name = "statusLabelFour"
        Me.statusLabelFour.Size = New System.Drawing.Size(0, 17)
        '
        'XproFilterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(971, 592)
        Me.Controls.Add(Me.txtCacheSize)
        Me.Controls.Add(Me.lblCacheSize)
        Me.Controls.Add(Me.txtPageSize)
        Me.Controls.Add(Me.lblPageSize)
        Me.Controls.Add(Me.txtTable)
        Me.Controls.Add(Me.lblTable)
        Me.Controls.Add(Me.txtConnectionString)
        Me.Controls.Add(Me.lblConnectionString)
        Me.Controls.Add(Me.statusPanel)
        Me.Controls.Add(Me.dataGrid)
        Me.Controls.Add(Me.btnFilter)
        Me.MinimumSize = New System.Drawing.Size(987, 577)
        Me.Name = "XproFilterForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Filtered View"
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusPanel.ResumeLayout(False)
        Me.statusPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFilter As System.Windows.Forms.Button
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents statusPanel As System.Windows.Forms.StatusStrip
    Friend WithEvents statusLabelOne As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents statusLabelTwo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents statusLabelThree As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents KeyTimer As System.Windows.Forms.Timer
    Friend WithEvents lineNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents employeeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents firstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents jobDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents departmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents managerId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dateHired As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lastPasswordChange As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblConnectionString As System.Windows.Forms.Label
    Friend WithEvents txtConnectionString As System.Windows.Forms.TextBox
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents txtTable As System.Windows.Forms.TextBox
    Friend WithEvents txtPageSize As System.Windows.Forms.TextBox
    Friend WithEvents lblPageSize As System.Windows.Forms.Label
    Friend WithEvents txtCacheSize As System.Windows.Forms.TextBox
    Friend WithEvents lblCacheSize As System.Windows.Forms.Label
    Friend WithEvents statusLabelFour As System.Windows.Forms.ToolStripStatusLabel

End Class
