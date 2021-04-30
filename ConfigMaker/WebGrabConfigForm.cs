using DevExpress.XtraEditors;
using ConfigMaker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ConfigMaker
{
    public partial class WebGrabConfigForm : XtraForm
    {
        private readonly settings config;
        private readonly GrabConfiguration userConfig;
        private bool _isSaved;

        public WebGrabConfigForm(settings config, GrabConfiguration userConfig)
        {
            InitializeComponent();
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.userConfig = userConfig;
            FillForm();
        }

        public bool Edit()
        {
            _isSaved = false;
            ShowDialog();
            return _isSaved;
        }

        #region "Config2Form"

        private void FillFileName()
        {
            if (config.filename == null)
            {
                if (userConfig != null)
                {
                    var fileName = userConfig.Name.Replace(' ', '_').ToLower() + ".xml";
                    txtFileName.Text = Path.Combine(Locations.OutputDirectory.FullName, fileName);
                }
                txtFileName.Text = settings.DefaultDirectoryConfigPath;
            }
            else
            {
                txtFileName.Text = config.filename;
            }
        }

        private void FillMode()
        {
            if (!string.IsNullOrEmpty(config.mode))
            {
                var modes = config.mode.Split(' ', ',');
                foreach (var item in modes)
                {
                    var m = item.ToLower();
                    if (m.Length > 0)
                    {
                        if (m == "d" || m == "debug")
                        {
                            chkDebug.Checked = true;
                        }
                        if (m == "m" || m == "measure")
                        {
                            chkMeasure.Checked = true;
                        }
                        if (m == "n" || m == "nomark")
                        {
                            chkNomark.Checked = true;
                        }
                        if (m == "v" || m == "verify")
                        {
                            chkVerify.Checked = true;
                        }
                        if (m == "w" || m == "wget")
                        {
                            chkWget.Checked = true;
                        }
                    }
                }
            }
        }

        private void FillPostProcess()
        {
            if (config.postprocess != null)
            {
                foreach (var item in config.postprocess)
                {
                    if (string.IsNullOrEmpty(item.Value))
                    {
                        chkMdbRun.Checked = XmlBool(item.run ?? "y");
                        chkMdbGrab.Checked = XmlBool(item.grab ?? "y");
                        chkRexGrab.Checked = XmlBool(item.grab ?? "y");
                        chkRexRun.Checked = XmlBool(item.run ?? "y");
                        break;
                    }
                    else
                    {
                        var p = item.Value.ToLower();
                        if (p == "mdb")
                        {
                            chkMdbRun.Checked = XmlBool(item.run ?? "y");
                            chkMdbGrab.Checked = XmlBool(item.grab ?? "y");
                        }
                        else if (p == "rex")
                        {
                            chkRexRun.Checked = XmlBool(item.run ?? "y");
                            chkRexGrab.Checked = XmlBool(item.grab ?? "y");
                        }
                    }
                }
            }
        }

        private void FillProxy()
        {
            if (!string.IsNullOrEmpty(config.proxy?.Value))
            {
                Uri url;
                IPAddress ip;
                if (Uri.TryCreate(String.Format("http://{0}", config.proxy.Value), UriKind.Absolute, out url) &&
                   IPAddress.TryParse(url.Host, out ip))
                {
                    txtProxyAddress.Text = url.Host.ToString();
                    txtPort.Text = url.Port.ToString();
                    txtUsername.Text = config.proxy.user;
                    txtPassword.Text = config.proxy.password;
                }
            }
        }

        private void FillUserAgent()
        {
            if (!string.IsNullOrEmpty(config.useragent))
            {
                txtUserAgent.Text = config.useragent;
                if (config.useragent.ToLower() == "random")
                {
                    txtUserAgent.Text = config.useragent;
                    chkRandom.Checked = true;
                }
            }
        }

        private void FillRetry()
        {
            if (config.retry?.Value != null)
            {
                txtRetryTimes.Text = config.retry.Value.ToString();
                txtChannelDelay.Text = config.retry.channeldelay.ToString();
                txtIndexDelay.Text = config.retry.indexdelay.ToString();
                txtShowDelay.Text = config.retry.showdelay.ToString();
                txtTimeOut.Text = config.retry.timeout.ToString();
            }
            else
            {
                txtRetryTimes.Text = 6.ToString();
                txtChannelDelay.Text = 0.ToString();
                txtIndexDelay.Text = 0.ToString();
                txtShowDelay.Text = 0.ToString();
                txtTimeOut.Text = 10.ToString();
            }
        }

        private void FillSkip()
        {
            if (config.skip != null)
            {
                if (config.skip.ToLower() == "noskip")
                {
                    chkNoSkip.Checked = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(config.skip))
                    {
                        var s = config.skip.Split(',');
                        if (s.Length > 0)
                        {
                            txtSkipMaxHours.Text = s[0];
                        }
                        if (s.Length > 1)
                        {
                            txtSkipMinMinutes.Text = s[1];
                        }
                    }
                }
            }
        }

        private void FillTimeStamp()
        {
            if (!string.IsNullOrEmpty(config.timespan))
            {
                var spans = config.timespan.Split(',');
                if (spans.Length > 0)
                {
                    txtTimeSpanDays.Text = spans[0];
                }
                if (spans.Length > 1)
                {
                    txtTimeSpanTime.EditValue = spans[1];
                }
                else
                {
                    txtTimeSpanTime.EditValue = null;
                }
            }
        }

        private void FillLicense()
        {
            if (config.license != null)
            {
                txtLicenseUsername.Text = config.license.wgusername;
                txtLicensePassword.Text = config.license.password;
                txtLicenseEmail.Text = config.license.registeredemail;
                if (!string.IsNullOrEmpty(config.license.Value))
                {
                    chkLicenseForce.Checked = config.license.Value.ToLower() == "f";
                }
            }
        }

        private void FillUpdate()
        {
            if (config.update != null)
            {
                var u = config.update.ToLower();
                if (u == "c" || u == "channel list")
                {
                    comboUpdate.SelectedIndex = 0;
                    return;
                }
                if (u == "i" || u == "incremental")
                {
                    comboUpdate.SelectedIndex = 1;
                    return;
                }
                if (u == "l" || u == "light")
                {
                    comboUpdate.SelectedIndex = 2;
                    return;
                }
                if (u == "s" || u == "smart")
                {
                    comboUpdate.SelectedIndex = 3;
                    return;
                }
                if (u == "f" || u == "full")
                {
                    comboUpdate.SelectedIndex = 4;
                    return;
                }
                if (u == "force")
                {
                    comboUpdate.SelectedIndex = 5;
                    return;
                }
            }
        }

        private void FillLogging()
        {
            if (config.logging != null)
            {
                chkLogging.Checked = config.logging.ToLower() == "on";
            }
        }

        private void FillDecryptionKeys()
        {
            grid.DataSource = config.decryptkey;
        }

        private void FillForm()
        {
            FillFileName();
            FillMode();
            FillPostProcess();
            FillProxy();
            FillUserAgent();
            FillUserAgent();
            FillRetry();
            FillSkip();
            FillTimeStamp();
            FillLicense();
            FillUpdate();
            FillLogging();
            FillDecryptionKeys();
        }

        #endregion

        #region "Form2Config"

        private void UpdateConfig()
        {
            config.filename = txtFileName.Text;
            UpdateModes();
            UpdatePostProcess();
            UpdateProxy();
            UpdateUserAgent();
            UpdateRetry();
            UpdateSkip();
            UpdateTimespan();
            UpdateLicense();
            UpdateUpdate();
            UpdateLogging();
        }

        private void UpdateModes()
        {
            var modes = new List<string>();
            if (chkDebug.Checked)
            {
                modes.Add("d");
            }
            if (chkMeasure.Checked)
            {
                modes.Add("m");
            }
            if (chkNomark.Checked)
            {
                modes.Add("n");
            }
            if (chkVerify.Checked)
            {
                modes.Add("v");
            }
            if (chkWget.Checked)
            {
                modes.Add("w");
            }
            config.mode = string.Join(",", modes);
        }

        private void UpdatePostProcess()
        {
            if ((chkMdbGrab.Checked == chkRexGrab.Checked) && (chkMdbRun.Checked == chkRexRun.Checked))
            {
                config.postprocess = new BindingList<settingsPostprocess>();
                config.postprocess.Add(new settingsPostprocess() { grab = chkMdbGrab.Checked ? "y" : "n", run = chkMdbRun.Checked ? "y" : "n", Value = null });
            }
            else
            {
                config.postprocess = new BindingList<settingsPostprocess>();
                if (chkMdbRun.Checked || chkMdbGrab.Checked)
                {
                    config.postprocess.Add(new settingsPostprocess() { grab = chkMdbGrab.Checked ? "y" : "n", run = chkMdbRun.Checked ? "y" : "n", Value = "mdb" });
                }
                if (chkRexRun.Checked || chkRexGrab.Checked)
                {
                    config.postprocess.Add(new settingsPostprocess() { grab = chkRexGrab.Checked ? "y" : "n", run = chkRexRun.Checked ? "y" : "n", Value = "rex" });
                }
            }
        }

        private void UpdateProxy()
        {
            if (!string.IsNullOrEmpty(txtProxyAddress.Text))
            {
                config.proxy = new settingsProxy();
                if (!string.IsNullOrEmpty(txtPort.Text))
                {
                    config.proxy.Value = string.Join(":", new string[] { txtProxyAddress.Text, txtPort.Text });
                }
                else
                {
                    config.proxy.Value = txtProxyAddress.Text;
                }
                config.proxy.user = txtUsername.Text;
                config.proxy.password = txtPassword.Text;
            }
            else
            {
                config.proxy = null;
            }
        }

        private void UpdateUserAgent()
        {
            if (!string.IsNullOrEmpty(txtUserAgent.Text))
            {
                config.useragent = txtUserAgent.Text;
            }
            else
            {
                config.useragent = string.Empty;
            }
        }

        private void UpdateRetry()
        {
            config.retry = new settingsRetry();
            config.retry.Value = byte.Parse(txtRetryTimes.Text ?? "6"); ;
            config.retry.channeldelay = byte.Parse(txtChannelDelay.Text ?? "0");
            config.retry.indexdelay = byte.Parse(txtIndexDelay.Text ?? "0");
            config.retry.showdelay = byte.Parse(txtShowDelay.Text ?? "0");
            config.retry.timeout = byte.Parse(txtTimeOut.Text ?? "10");
        }

        private void UpdateSkip()
        {
            if (chkNoSkip.Checked)
            {
                config.skip = "noskip";
                return;
            }
            var skip = new string[2];
            skip[0] = "12";
            skip[1] = "0";
            if (!string.IsNullOrEmpty(txtSkipMaxHours.Text))
            {
                skip[0] = txtSkipMaxHours.Text;
            }
            if (!string.IsNullOrEmpty(txtSkipMinMinutes.Text))
            {
                skip[0] = txtSkipMinMinutes.Text;
            }
            config.skip = string.Join(",", skip);
        }

        private void UpdateTimespan()
        {
            config.timespan = "0";
            if (!string.IsNullOrEmpty(txtTimeSpanDays.Text))
            {
                config.timespan = txtTimeSpanDays.Text;
            }
            if (!string.IsNullOrEmpty(txtTimeSpanTime.Text))
            {
                config.timespan += "," + txtTimeSpanTime.Text;
            }
        }

        private void UpdateLicense()
        {
            if (!string.IsNullOrEmpty(txtLicenseUsername.Text)
                && !string.IsNullOrEmpty(txtLicensePassword.Text)
                && !string.IsNullOrEmpty(txtLicenseEmail.Text))
            {
                config.license = new settingsLicense();
                config.license.wgusername = txtUsername.Text;
                config.license.password = txtPassword.Text;
                config.license.registeredemail = txtLicenseEmail.Text;
                if (chkLicenseForce.Checked)
                {
                    config.license.Value = "f";
                }
            }
            else
            {
                config.license = null;
            }
        }

        private void UpdateUpdate()
        {
            if (comboUpdate.EditValue == null)
            {
                config.update = string.Empty;
                return;
            }
            switch (comboUpdate.SelectedIndex)
            {
                case 0:
                    config.update = "channel list";
                    break;
                case 1:
                    config.update = "incremental";
                    break;
                case 2:
                    config.update = "light";
                    break;
                case 3:
                    config.update = "smart";
                    break;
                case 4:
                    config.update = "full";
                    break;
                case 5:
                    config.update = "force";
                    break;
                default:
                    config.update = string.Empty;
                    break;
            }
        }

        private void UpdateLogging()
        {
            if (chkLogging.Checked)
            {
                config.logging = "on";
            }
            else
            {
                config.logging = "off";
            }
        }

        #endregion

        #region "Events"

        private void btnFileName_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XMLTV file|*.xml";
                saveFileDialog.Title = "XMLTV output file";
                saveFileDialog.OverwritePrompt = false;
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                if (saveFileDialog.FileName != "")
                {
                    txtFileName.Text = saveFileDialog.FileName;
                }
            }
        }

        private void chkRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandom.Checked)
            {
                txtUserAgent.Text = "random";
                txtUserAgent.ReadOnly = true;
            }
            else
            {
                txtUserAgent.Text = config.useragent;
                txtUserAgent.ReadOnly = false;
            }
        }

        private void chkNoSkip_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoSkip.Checked)
            {
                txtSkipMaxHours.ReadOnly = true;
                txtSkipMaxHours.Text = null;
                txtSkipMinMinutes.ReadOnly = true;
                txtSkipMinMinutes.Text = null;
            }
            else
            {
                txtSkipMaxHours.ReadOnly = false;
                txtSkipMinMinutes.ReadOnly = false;
                if (!string.IsNullOrEmpty(config.skip))
                {
                    var s = config.skip.Split(',');
                    if (s.Length > 0)
                    {
                        if (config.skip != "noskip")
                        {
                            txtSkipMaxHours.Text = s[0];
                        }
                    }
                    if (s.Length > 1)
                    {
                        txtSkipMinMinutes.Text = s[1];
                    }
                }
            }
        }

        private void comboUpdate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                comboUpdate.EditValue = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                Save();
            }
        }

        private void Save()
        {
            gv.UpdateCurrentRow();
            UpdateConfig();
            CleanErrors();
            if (IsValid())
            {
                try
                {
                    config.SaveToFile(config.Path.FullName);
                    _isSaved = true;
                    Hide();
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Failed to save configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                    XtraMessageBox.Show(string.Format("Failed to save configuration.{0}{1}", Environment.NewLine, ex.Message), "Error");
                }
            }
        }

        private void gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colsite)
            {
                if (string.IsNullOrEmpty(e.Value?.ToString()))
                {
                    gv.DeleteRow(e.RowHandle);
                }
            }
        }

        private void WebGrabConfigForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (IsValid())
                {
                    Save();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Dispose();
            }
        }

        #endregion

        private void CleanErrors()
        {
            txtFileName.ErrorText = string.Empty;
            txtLicenseEmail.ErrorText = string.Empty;
            txtLicensePassword.ErrorText = string.Empty;
            txtLicenseUsername.ErrorText = string.Empty;
        }

        private bool IsValid()
        {
            bool valid = true;
            if (txtFileName.Text == null)
            {
                txtFileName.ErrorText = "Please enter name of the output file";
                valid = false;
            }
            else
            {
                try
                {
                    var fi = new FileInfo(txtFileName.Text);
                    if (!fi.Exists)
                    {
                        fi.Create().Close();
                        fi.Delete();
                    }
                }
                catch (Exception ex)
                {
                    txtFileName.ErrorText = "Invalid name of the output file";
                    valid = false;
                }
            }
            valid = valid && ValidateLicenseInfo();          
            return valid;
        }

        private bool ValidateLicenseInfo()
        {
            bool valid = true;
            if (
                !string.IsNullOrEmpty(txtLicenseEmail.Text) 
                || !string.IsNullOrEmpty(txtLicenseUsername.Text) 
                || !string.IsNullOrEmpty(txtLicensePassword.Text))
            {
                if (string.IsNullOrEmpty(txtLicenseEmail.Text)){
                    txtLicenseEmail.ErrorText = "Invalid license email";
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtLicenseUsername.Text))
                {
                    txtLicenseUsername.ErrorText = "Invalid license username";
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtLicensePassword.Text))
                {
                    txtLicensePassword.ErrorText = "Invalid license password";
                    valid = false;
                }
            }
            return valid;
        }

        private bool XmlBool(string value)
        {
            var v = value.ToLower();
            if (v == "y") return true;
            if (v == "yes") return true;
            if (v == "true") return true;
            if (v == "on") return true;
            return false;
        }

    }
}
