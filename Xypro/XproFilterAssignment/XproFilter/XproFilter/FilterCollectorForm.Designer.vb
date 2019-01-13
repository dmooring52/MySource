<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilterCollectorForm
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
        Me.cmbCondition1 = New System.Windows.Forms.ComboBox()
        Me.lblCondition = New System.Windows.Forms.Label()
        Me.txtCondition1 = New System.Windows.Forms.TextBox()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtCondition2 = New System.Windows.Forms.TextBox()
        Me.cmbCondition2 = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.radioAnd = New System.Windows.Forms.RadioButton()
        Me.radioOr = New System.Windows.Forms.RadioButton()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbCondition1
        '
        Me.cmbCondition1.FormattingEnabled = True
        Me.cmbCondition1.Location = New System.Drawing.Point(12, 25)
        Me.cmbCondition1.Name = "cmbCondition1"
        Me.cmbCondition1.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondition1.TabIndex = 2
        '
        'lblCondition
        '
        Me.lblCondition.AutoSize = True
        Me.lblCondition.Location = New System.Drawing.Point(12, 9)
        Me.lblCondition.Name = "lblCondition"
        Me.lblCondition.Size = New System.Drawing.Size(51, 13)
        Me.lblCondition.TabIndex = 0
        Me.lblCondition.Text = "Condition"
        '
        'txtCondition1
        '
        Me.txtCondition1.Location = New System.Drawing.Point(139, 25)
        Me.txtCondition1.Name = "txtCondition1"
        Me.txtCondition1.Size = New System.Drawing.Size(236, 20)
        Me.txtCondition1.TabIndex = 3
        '
        'lblValue
        '
        Me.lblValue.AutoSize = True
        Me.lblValue.Location = New System.Drawing.Point(139, 9)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(34, 13)
        Me.lblValue.TabIndex = 1
        Me.lblValue.Text = "Value"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(381, 24)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(43, 20)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Location = New System.Drawing.Point(430, 24)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(43, 20)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtCondition2
        '
        Me.txtCondition2.Location = New System.Drawing.Point(139, 82)
        Me.txtCondition2.Name = "txtCondition2"
        Me.txtCondition2.Size = New System.Drawing.Size(236, 20)
        Me.txtCondition2.TabIndex = 6
        '
        'cmbCondition2
        '
        Me.cmbCondition2.FormattingEnabled = True
        Me.cmbCondition2.Location = New System.Drawing.Point(12, 81)
        Me.cmbCondition2.Name = "cmbCondition2"
        Me.cmbCondition2.Size = New System.Drawing.Size(121, 21)
        Me.cmbCondition2.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.radioOr)
        Me.Panel1.Controls.Add(Me.radioAnd)
        Me.Panel1.Location = New System.Drawing.Point(12, 52)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(363, 24)
        Me.Panel1.TabIndex = 4
        '
        'radioAnd
        '
        Me.radioAnd.AutoSize = True
        Me.radioAnd.Location = New System.Drawing.Point(3, 3)
        Me.radioAnd.Name = "radioAnd"
        Me.radioAnd.Size = New System.Drawing.Size(44, 17)
        Me.radioAnd.TabIndex = 0
        Me.radioAnd.TabStop = True
        Me.radioAnd.Text = "And"
        Me.radioAnd.UseVisualStyleBackColor = True
        '
        'radioOr
        '
        Me.radioOr.AutoSize = True
        Me.radioOr.Location = New System.Drawing.Point(53, 3)
        Me.radioOr.Name = "radioOr"
        Me.radioOr.Size = New System.Drawing.Size(36, 17)
        Me.radioOr.TabIndex = 1
        Me.radioOr.TabStop = True
        Me.radioOr.Text = "Or"
        Me.radioOr.UseVisualStyleBackColor = True
        '
        'FilterCollectorForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClear
        Me.ClientSize = New System.Drawing.Size(480, 113)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtCondition2)
        Me.Controls.Add(Me.cmbCondition2)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblValue)
        Me.Controls.Add(Me.txtCondition1)
        Me.Controls.Add(Me.lblCondition)
        Me.Controls.Add(Me.cmbCondition1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FilterCollectorForm"
        Me.Text = "Filter Conditions"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbCondition1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblCondition As System.Windows.Forms.Label
    Friend WithEvents txtCondition1 As System.Windows.Forms.TextBox
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents txtCondition2 As System.Windows.Forms.TextBox
    Friend WithEvents cmbCondition2 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents radioOr As System.Windows.Forms.RadioButton
    Friend WithEvents radioAnd As System.Windows.Forms.RadioButton
End Class
