using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web;

namespace LiveSwitch.TextControl
{
    public partial class LinkDialog : Form
    {
        private bool _accepted = false;

        public LinkDialog()
        {
            InitializeComponent();
            LoadUrls();
            linkEdit.TextChanged += new EventHandler(linkEdit_TextChanged);
        }

        void linkEdit_TextChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.Text + linkEdit.Text;
        }

        public string URL
        {
            get
            {
                return linkEdit.Text.Trim();
            }
            set
            {
                linkEdit.Text = value;
            }
        }

        public string Scheme
        {
            get
            {
                return comboBox1.Text;
            }
            set
            {
                comboBox1.Text = value;
            }
        }

        public bool Accepted
        {
            get
            {
                return _accepted;
            }
        }

        private void LinkDialog_Load(object sender, EventArgs e)
        {
            label1.Text = comboBox1.Text + URL;
            BeginInvoke((MethodInvoker)delegate
            {
                linkEdit.Focus();
            });
        }

        private void LoadUrls()
        {
            string glob = Properties.Settings.Default.LinkDialogURLs;
            string[] urls = glob.Split(null);
            if (urls != null)
            {
                foreach (string url in urls)
                {
                    linkEdit.Items.Add(url);
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string url = linkEdit.Text;
            string glob = Properties.Settings.Default.LinkDialogURLs;
            if (glob == null) glob = "";
            if (!glob.Contains(url))
            {
                if (glob.Length > 0)
                    glob += "\n";
                glob += url;
            }
            Properties.Settings.Default.LinkDialogURLs = glob;
            Properties.Settings.Default.Save();
            _accepted = true;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _accepted = false;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.Text + URL;
        }
    }
}