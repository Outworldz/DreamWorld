﻿Imports EmailValidation
Imports MailKit.Net.Smtp
Imports MimeKit

Public Class FormEmail

    Private ReadOnly Contacts As New Dictionary(Of String, String)

    Public Sub Init(L As ListView)
        If L Is Nothing Then Return

        If Settings.SmtPropUserName = "LoginName@gmail.com" Then
            MsgBox(My.Resources.No_Email, vbInformation, "Oops")
#Disable Warning CA2000
            Dim FormDiva As New FormDiva
#Enable Warning CA2000
            FormDiva.Activate()
            FormDiva.Visible = True
            FormDiva.Select()
            FormDiva.BringToFront()
            Me.Close()
            Return
        End If

        Dim counter = 0
        For Each X As ListViewItem In L.Items
            If X.Checked Then
                Contacts.Add(X.Text, X.SubItems(1).Text)
                counter += 1
            End If
        Next

        Me.Text = CStr(counter) & " " & My.Resources.Emails_Selected

        If counter = 0 Then
            MsgBox(My.Resources.No_Emails_Selected, vbInformation, "Oops")
            Close()
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SendButton.Click

        BringToFront()

        If SubjectTextBox.TextLength = 0 Then
            MsgBox(My.Resources.No_Subject)
            Return
        End If

        Dim Message = New MimeMessage()
        Message.From.Add(New MailboxAddress("", Settings.AdminEmail))

        For Each Contact In Contacts
            If EmailValidator.Validate(Contact.Value) Then
                Message.Bcc.Add(New MailboxAddress(Contact.Key, Contact.Value))
            End If
        Next

        Message.Subject = SubjectTextBox.Text

        Dim builder = New BodyBuilder With {
            .TextBody = EditorBox.BodyText,
            .HtmlBody = EditorBox.BodyHtml
        }
        Message.Body = builder.ToMessageBody()
        Using client As New SmtpClient()
            Try
                client.Connect(Settings.SmtpHost, Settings.SmtpPort, False)
            Catch ex As Exception
                MsgBox("Could Not Connect:" & ex.Message, vbExclamation, "Error")
                Return
            End Try
            Try
                client.Authenticate(Settings.SmtPropUserName, Settings.SmtpPassword)
            Catch ex As Exception
                MsgBox("Could Not Log In:" & ex.Message, vbExclamation, "Error")
                Return
            End Try
            Try
                client.Send(Message)
            Catch ex As Exception
                MsgBox("Could Not Send:" & ex.Message, vbExclamation, "Error")
                Return
            End Try
            Try
                client.Disconnect(True)
            Catch
            End Try

        End Using

        Me.Close()
    End Sub

    Private Sub Email_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        EditorBox.DocumentText = "<html><body></body></html>"
        SubjectLabel.Text = My.Resources.Subject_word
        SendButton.Text = My.Resources.Send_word

    End Sub

End Class