using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ConfigMaker.Commands;
using ConfigMaker.Model;
using ConfigMaker.Properties;
using System.Configuration;

namespace ConfigMaker
{
    public partial class mainForm : XtraForm
    {

        public mainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            bar2.Visible = false;
        }

        private void SetMenusEnabled()
        {
            var enabled = gv.RowCount > 0;
            editConfigItem.Enabled = enabled;
            editSelectedWebGrabConfigItem.Enabled = enabled;
            deleteItem.Enabled = enabled;
            updateSiteInisItem.Enabled = enabled;
            updateSiteKeys.Enabled = enabled;
            channelsMapItem.Enabled = enabled;
            runConfigurationItem.Enabled = enabled;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Settings.Default.WebGrabExePath))
            {
                runConfigurationItem.Visible = false;
            }
            Reload();
        }

        private void Reload()
        {
            LoadConfigurations();
            SetMenusEnabled();
        }

        private void LoadConfigurations()
        {
            var listConfigurations = new ListConfigurations(Locations.UserConfigDirectory);
            listConfigurations.StatusChanged += OnStatusChanged;
            var configList = listConfigurations.Execute();
            SplashManager.CloseSplashScreen();
            grid.DataSource = configList;
        }

        private async void downloadSiteIni_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (AskQuestion(string.Format("Download fresh siteini.pack into working folder?", Environment.NewLine)))
            {
                await updateSiteIniPack();
            }
        }

        private async Task updateSiteIniPack()
        {
            try
            {
                navBarControl.Enabled = false;
                var siteIniUpdater = new SiteIniPackUpdater(Locations.WorkingDirectory);
                siteIniUpdater.StatusChanged += OnStatusChanged;
                await siteIniUpdater.Execute();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Update failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Update failed.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                navBarControl.Enabled = true;
                SplashManager.CloseSplashScreen();
            }
        }

        private void OnStatusChanged(object sender, Model.StatusChangedEventArgs e)
        {
            SplashManager.ShowSplashScreen(e.Status, e.Description);
        }

        private void updateSiteInis_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (gv.GetFocusedRow() == null)
            {
                return;
            }
            if (AskQuestion(string.Format("Overwrite existing .xml and. ini files in siteini.user" +
                "{0}for selected configuration from current siteini.pack? ", Environment.NewLine)))
            {
                try
                {
                    var config = (GrabConfiguration)gv.GetFocusedRow();
                    navBarControl.Enabled = false;
                    var updateSiteIniUser = new UpdateSiteIniUser();
                    updateSiteIniUser.StatusChanged += OnStatusChanged;
                    updateSiteIniUser.Execute(config);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Update failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                    XtraMessageBox.Show(string.Format("Update failed.{0}{1}", Environment.NewLine, ex.Message), "Error");
                }
                finally
                {
                    navBarControl.Enabled = true;
                    SplashManager.CloseSplashScreen();
                }
            }
        }

        public static bool AskQuestion(string question)
        {
            return XtraMessageBox.Show(question, "Question", MessageBoxButtons.YesNo) != DialogResult.No;
        }

        private void editWebgrabConfigItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (settings.WorkingDirectoryConfigExists)
            {
                if (!AskQuestion(string.Format("Edit {0} located in working directory?" +
                "{1}Selected channels won't be changed. ", Settings.Default.WebGrabConfigFileName, Environment.NewLine)))
                {
                    return;
                }
            }
            else if (settings.AppDataConfigExists)
            {
                if (!AskQuestion(string.Format("Edit global {0} located in AppData directory?" +
                "{1}Selected channels won't be changed.", Settings.Default.WebGrabConfigFileName, Environment.NewLine)))
                {
                    return;
                }
            }
            else
            {
                if (!AskQuestion(string.Format("{0} not found in neither AppData or working directory." +
                "{1}Create new one in working directory?", Settings.Default.WebGrabConfigFileName, Environment.NewLine)))
                {
                    return;
                }
            }
            try
            {
                var defaultConfig = settings.LoadDefault();
                var frm = new WebGrabConfigForm(defaultConfig, null);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Edit failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Edit failed.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                SplashManager.CloseSplashScreen();
            }

        }

        private void updateSiteKeys_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var defaultConfig = settings.LoadDefault();
            if (settings.WorkingDirectoryConfigExists)
            {
                if ((defaultConfig.decryptkey?.Count ?? 0) == 0)
                {
                    XtraMessageBox.Show(string.Format("{0} in working directory has no site keys." +
                   "{1}Nothing to do.",
                   Settings.Default.WebGrabConfigFileName,
                   Environment.NewLine
                   ));
                    return;
                }
                if (!AskQuestion(string.Format("Copy decryption keys from {0}" +
                "{1}in working directory to all user configurations?" +
                "{1}Other settings won't be changed.", Settings.Default.WebGrabConfigFileName, Environment.NewLine)))
                {
                    return;
                }
            }
            else if (settings.AppDataConfigExists)
            {
                if ((defaultConfig.decryptkey?.Count ?? 0) == 0)
                {
                    XtraMessageBox.Show(string.Format("{0} in AppData directory has no site keys." +
                   "{1}Nothing to do.",
                   Settings.Default.WebGrabConfigFileName,
                   Environment.NewLine
                   ));
                    return;
                }
                if (!AskQuestion(string.Format("Copy decryption keys from {0}" +
                "{1}in AppData directory to all user configurations?" +
                "{1}Other settings won't be changed.", Settings.Default.WebGrabConfigFileName, Environment.NewLine)))
                {
                    return;
                }
            }
            else
            {
                XtraMessageBox.Show(string.Format("{0} not found in neither AppData or working directory." +
                    "{1}Nothing to do.",
                    Settings.Default.WebGrabConfigFileName,
                    Environment.NewLine
                    ));
                return;
            }

            try
            {
                navBarControl.Enabled = false;
                var updateSiteKeys = new UpdateSiteKeys();
                updateSiteKeys.StatusChanged += OnStatusChanged;
                updateSiteKeys.Execute(Locations.UserConfigDirectory, defaultConfig);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to update site decryption keys.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to update site decryption keys.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                navBarControl.Enabled = true;
                SplashManager.CloseSplashScreen();
            }
        }

        private void editConfigItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (gv.GetFocusedRow() == null)
                {
                    return;
                }
                var loadConfigCommand = new LoadConfiguration();
                var userConfig = (GrabConfiguration)gv.GetFocusedRow();
                var configFromDisk = loadConfigCommand.Execute(userConfig.Path);
                var frm = new UserConfigForm();
                var editResult = frm.Edit(configFromDisk);
                if (editResult != null)
                {
                    var handle = gv.FocusedRowHandle;
                    var configurations = (BindingList<GrabConfiguration>)grid.DataSource;
                    var currentIndex = configurations.IndexOf(userConfig);
                    configurations.Insert(currentIndex, editResult);
                    configurations.Remove(userConfig);
                    gv.FocusedRowHandle = handle;
                    grid.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to edit user configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to edit user configuration.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                navBarControl.Enabled = true;
                SplashManager.CloseSplashScreen();
            }
        }

        private void addItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                var template = settings.LoadDefault();
                template.channel.Clear();
                var frm = new UserConfigForm();
                var grabConfig = frm.Add(template);
                if (grabConfig != null)
                {
                    var configurations = (BindingList<GrabConfiguration>)grid.DataSource;
                    configurations.Add(grabConfig);
                    grid.RefreshDataSource();
                    SetMenusEnabled();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to create user configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to create user configuration.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                SplashManager.CloseSplashScreen();
            }
        }

        private void editSelectedWebGrabConfigItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var userConfig = (GrabConfiguration)gv.GetFocusedRow();
            if (userConfig != null)
            {
                if (!AskQuestion(string.Format("Edit grabbing settings for {0}?", userConfig.Name)))
                {
                    return;
                }
                try
                {
                    var loadConfigCommand = new LoadConfiguration();
                    var configFromDisk = loadConfigCommand.Execute(userConfig.Path);
                    var frm = new WebGrabConfigForm(configFromDisk.Config, null);
                    if (frm.Edit())
                    {
                        var handle = gv.FocusedRowHandle;
                        var configurations = (BindingList<GrabConfiguration>)grid.DataSource;
                        var currentIndex = configurations.IndexOf(userConfig);
                        configurations.Insert(currentIndex, configFromDisk);
                        configurations.Remove(userConfig);
                        gv.FocusedRowHandle = handle;
                        grid.RefreshDataSource();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Edit failed.{0}{1}", Environment.NewLine, ex.Message), ex);
                    XtraMessageBox.Show(string.Format("Edit failed.{0}{1}", Environment.NewLine, ex.Message), "Error");
                }
                finally
                {
                    SplashManager.CloseSplashScreen();
                }

            }
        }

        private void deleteItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var userConfig = (GrabConfiguration)gv.GetFocusedRow();
            if (userConfig == null) return;

            if (!AskQuestion(string.Format("Delete {0}?" +
                "{1}This action will PERMANENTLY delete all configuration files." +
                "{1}Are you sure you want to do this?",
                userConfig.Name, Environment.NewLine)))
            {
                return;
            }
            try
            {
                var delCommand = new DeleteConfiguration();
                delCommand.Execute(userConfig);
                gv.DeleteRow(gv.FocusedRowHandle);
                SetMenusEnabled();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to delete user configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to delete user configuration.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                SplashManager.CloseSplashScreen();
            }
        }

        private void aboutItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            new aboutForm().ShowDialog();
        }

        private void refreshItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Reload();
        }

        private void channelsMapItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!AskQuestion(string.Format("Create {0} in output directory?", Settings.Default.ChannelsXmlFileName)))
            {
                return;
            }
            try
            {
                var listConfigurations = new ListConfigurations(Locations.UserConfigDirectory);
                listConfigurations.StatusChanged += OnStatusChanged;
                var configList = listConfigurations.Execute();
                var map = new ChannelMap();
                map.Configurations = configList;
                var mapFileName = Path.Combine(Locations.OutputDirectory.FullName, Settings.Default.ChannelsXmlFileName);
                map.SaveToFile(mapFileName);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to create channels map.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to create channels map.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                SplashManager.CloseSplashScreen();
            }
        }

        private void workingDirectoryItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = Locations.UserConfigDirectory.FullName,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        private void outputDirectoryItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = Locations.OutputDirectory.FullName,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        private void runConfigurationItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            try
            {
                navBarControl.Enabled = false;
                var userConfig = (GrabConfiguration)gv.GetFocusedRow();
                var runConfigCommand = new RunConfiguration();
                runConfigCommand.StatusChanged += OnStatusChanged;
                var p = runConfigCommand.Execute(userConfig, false);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to run configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to run configuration.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                navBarControl.Enabled = true;
                SplashManager.CloseSplashScreen();
            }
        }

        private void runAllConfigurationsItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!AskQuestion("Run all configurations?"))
            {
                return;
            }
            try
            {
                navBarControl.Enabled = false;
                Program.PreRunTask();
                var listConfigurations = new ListConfigurations(Locations.UserConfigDirectory);
                listConfigurations.StatusChanged += OnStatusChanged;
                var configList = listConfigurations.Execute();
                var runConfigsCommand = new RunAllConfigurations();
                runConfigsCommand.StatusChanged += OnStatusChanged;
                runConfigsCommand.Execute(configList, false);
                Program.PreRunTask();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to run configurations.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to run configurations.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
            finally
            {
                navBarControl.Enabled = true;
                SplashManager.CloseSplashScreen();
            }
        }

        private void preRunTaskItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Task to run before all configurations";
                if (!string.IsNullOrEmpty(Properties.Settings.Default.PreRunTask))
                {
                    openFileDialog.FileName = Settings.Default.PreRunTask;
                }
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                Properties.Settings.Default.PreRunTask = openFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void postRunTaskItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Task to run after all configurations";
                if (!string.IsNullOrEmpty(Properties.Settings.Default.PostRunTask))
                {
                    openFileDialog.FileName = Settings.Default.PostRunTask;
                }
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                Properties.Settings.Default.PostRunTask = openFileDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }
    }
}