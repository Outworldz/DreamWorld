<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEmail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEmail))
        Me.SubjectTextBox = New System.Windows.Forms.TextBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SubjectLabel = New System.Windows.Forms.Label()
        Me.SendButton = New System.Windows.Forms.Button()
        Me.RichTextBox = New System.Windows.Forms.RichTextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SubjectTextBox
        '
        Me.SubjectTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SubjectTextBox.Location = New System.Drawing.Point(186, 5)
        Me.SubjectTextBox.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.SubjectTextBox.Name = "SubjectTextBox"
        Me.SubjectTextBox.Size = New System.Drawing.Size(402, 20)
        Me.SubjectTextBox.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SubjectLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SendButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SubjectTextBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RichTextBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(600, 365)
        Me.SplitContainer1.SplitterDistance = 31
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 3
        '
        'SubjectLabel
        '
        Me.SubjectLabel.AutoSize = True
        Me.SubjectLabel.Location = New System.Drawing.Point(112, 12)
        Me.SubjectLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SubjectLabel.Name = "SubjectLabel"
        Me.SubjectLabel.Size = New System.Drawing.Size(43, 13)
        Me.SubjectLabel.TabIndex = 4
        Me.SubjectLabel.Text = "Subject"
        '
        'SendButton
        '
        Me.SendButton.Image = Global.Outworldz.My.Resources.Resources.mail_into
        Me.SendButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SendButton.Location = New System.Drawing.Point(9, 6)
        Me.SendButton.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.SendButton.Name = "SendButton"
        Me.SendButton.Size = New System.Drawing.Size(99, 25)
        Me.SendButton.TabIndex = 3
        Me.SendButton.Text = "Send"
        Me.SendButton.UseVisualStyleBackColor = True
        '
        'RichTextBox
        '
        Me.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox.Name = "RichTextBox"
        Me.RichTextBox.Size = New System.Drawing.Size(600, 331)
        Me.RichTextBox.TabIndex = 0
        Me.RichTextBox.Text = ""
        '
        'FormEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 365)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.Name = "FormEmail"
        Me.Text = "Email"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SubjectTextBox As TextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SubjectLabel As Label
    Friend WithEvents SendButton As Button
    Friend WithEvents RichTextBox As RichTextBox
End Class
