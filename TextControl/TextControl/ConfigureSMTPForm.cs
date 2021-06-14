using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace LiveSwitch.TextControl
{
    public enum SMTPAuthenticationType
    {
        None,
        Machine,
        UsernamePassword
    }

    public partial class ConfigureSMTPForm : Form
    {
        private MailMessage msg;

        public ConfigureSMTPForm(string hostname, int port, SMTPAuthenticationType authType, string username, string password, bool tls, MailMessage msg)
        {
            InitializeComponent();
            portTextBox.KeyPress += new KeyPressEventHandler(portTextBox_KeyPress);
            hostTextBox.Text = hostname;
            portTextBox.Text = port < 1 ? 25.ToString() : port.ToString();
            tlsCheckBox.Checked = tls;
            this.msg = msg;
            switch (authType)
            {
                case SMTPAuthenticationType.None:
                    authNoneRadioButton.Checked = true;
                    break;
                case SMTPAuthenticationType.Machine:
                    authMachineRadioButton.Checked = true;
                    break;
                case SMTPAuthenticationType.UsernamePassword:
                    authUsernameRadioButton.Checked = true;
                    break;
                default:
                    authNoneRadioButton.Checked = true;
                    break;
            }
            usernameTextBox.Text = username;
            passwordTextBox.Text = password;
        }

        private void portTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void hostTextBox_TextChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

        private void SomethingChanged()
        {
            bool up_valid = true;
            if (authUsernameRadioButton.Checked)
            {
                up_valid = usernameTextBox.Text.Trim().Length > 0 && passwordTextBox.Text.Length > 0;
            }
            bool valid = hostTextBox.Text.Trim().Length > 0 && portTextBox.Text.Trim().Length > 0 && up_valid;
            okButton.Enabled = valid;
        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var client = new SmtpClient();
            client.Host = Hostname;
            client.Port = Port;
            switch (AuthType)
            {
                case SMTPAuthenticationType.Machine:
                    client.UseDefaultCredentials = true;
                    break;
                case SMTPAuthenticationType.None:
                    break;
                case SMTPAuthenticationType.UsernamePassword:
                    client.Credentials = new NetworkCredential(Username, Password);
                    break;
            }
            client.EnableSsl = TLS;
            try
            {
                if (msg != null)
                {
                    msg.From = new MailAddress(fromTextBox.Text.Trim());
                    msg.To.Add(new MailAddress(toTextBox.Text.Trim()));
                    msg.Subject = subjectTextBox.Text.Trim();
                    client.Send(msg);
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error sending email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string Hostname
        {
            get
            {
                return hostTextBox.Text.Trim();
            }
        }

        public int Port
        {
            get
            {
                return int.Parse(portTextBox.Text.Trim());
            }
        }

        public SMTPAuthenticationType AuthType
        {
            get
            {
                SMTPAuthenticationType auth_type;
                if (authNoneRadioButton.Checked)
                    auth_type = SMTPAuthenticationType.None;
                else if (authMachineRadioButton.Checked)
                    auth_type = SMTPAuthenticationType.Machine;
                else
                    auth_type = SMTPAuthenticationType.UsernamePassword;
                return auth_type;
            }
        }

        public string Username
        {
            get
            {
                string val = usernameTextBox.Text.Trim();
                return string.IsNullOrEmpty(val) ? null : val;
            }
        }

        public string Password
        {
            get
            {
                string val = passwordTextBox.Text.Trim();
                return string.IsNullOrEmpty(val) ? null : val;
            }
        }

        public bool TLS
        {
            get
            {
                return tlsCheckBox.Checked;
            }
        }

        private void ConfigureSMTPForm_Load(object sender, EventArgs e)
        {
            hostTextBox.Focus();
            SomethingChanged();
            hostTextBox.Focus();
        }

        private void authNoneRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

        private void authMachineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

        private void authUsernameRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            SomethingChanged();
        }

    }
}
