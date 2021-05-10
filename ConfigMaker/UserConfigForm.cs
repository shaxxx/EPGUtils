using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ConfigMaker.Commands;
using ConfigMaker.Model;
using ConfigMaker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigMaker
{
    public partial class UserConfigForm : XtraForm
    {
        private GrabConfiguration _configuration;
        private bool isSaved;
        private bool isEdit;
        private bool _countriesLoaded;

        private void OnStatusChanged(object sender, Model.StatusChangedEventArgs e)
        {
            SplashManager.ShowSplashScreen(e.Status, e.Description);
        }

        public GrabConfiguration Configuration { get => _configuration; private set => _configuration = value; }

        private BindingList<Country> _sitePackCountries;

        public UserConfigForm()
        {
            InitializeComponent();
        }

        public GrabConfiguration Add(settings template)
        {
            isEdit = false;
            Configuration = new GrabConfiguration(Locations.OutputDirectory);
            Configuration.Config = template;
            LoadSitePackCountries();
            LoadChannels();
            _countriesLoaded = true;
            SplashManager.CloseSplashScreen();
            ShowDialog();
            if (!isSaved) return null;
            return Configuration;
        }

        public GrabConfiguration Edit(GrabConfiguration configuration)
        {
            isEdit = true;
            Configuration = configuration;
            txtName.Text = configuration.Name;
            LoadSitePackCountries();
            SelectUserCountries();
            _countriesLoaded = true;
            LoadChannels();
            SplashManager.CloseSplashScreen();
            ShowDialog();
            if (!isSaved) return null;
            return Configuration;
        }

        private void LoadSitePackCountries()
        {
            var sitePackDir = new DirectoryInfo(Path.Combine(Locations.WorkingDirectory.FullName, Settings.Default.SiteIniPack));
            var listAllCountries = new ListCountries(sitePackDir);
            listAllCountries.StatusChanged += OnStatusChanged;
            _sitePackCountries = listAllCountries.Execute();
            gridCountries.DataSource = _sitePackCountries;
        }

        private void SelectUserCountries()
        {
            var userSites = Configuration.Config.channel.Select(x => x.site).Distinct().ToList();
            var countriesList = (BindingList<Country>)gridCountries.DataSource;
            var sitePackSites = countriesList.SelectMany(x => x.Sites).ToList();
            var result = userSites.Join(
                sitePackSites,
                arg => arg.ToLower(),
                arg => arg.Path.Name.ToLower().Replace(".channels.xml",string.Empty),
                (first, second) => new { channel = first, site = second }).ToList();
            var userCountries = result.Select(x => x.site.Country).Distinct().ToList();
            gridCountries.SuspendLayout();
            foreach (var userCountry in userCountries)
            {
                var sitePackCountry = _sitePackCountries.SingleOrDefault(x => x.Name.ToLower() == userCountry.Name.ToLower());
                if (sitePackCountry != null)
                {
                    var index = gvCountries.FindRow(sitePackCountry);
                    gvCountries.SetRowCellValue(index, colSelectedCountry, true);
                }
            }
            gridCountries.ResumeLayout();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isSaved = false;
            Hide();
        }

        private void LoadChannels()
        {
            gridChannels.SuspendLayout();
            var countriesList = (BindingList<Country>)gridCountries.DataSource;
            var selectedCountries = new BindingList<Country>(countriesList.Where(x => x.Selected).ToList());
            var countriesChannelsList = selectedCountries.SelectMany(x => x.Sites)
                .SelectMany(x => x.channels)
                .OrderBy(x => x.site)
                .OrderBy(x => x.Value).ToList();
            gridChannels.DataSource = new BindingList<siteChannel>(countriesChannelsList);
            MarkSelectedChannels();
            gridChannels.ResumeLayout();
        }

        private void MarkSelectedChannels()
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            var userChannels = Configuration.Config.channel;
            foreach (var userChannel in userChannels)
            {
                var match = countriesChannelsList.FirstOrDefault(x => x.site == userChannel.site && x.site_id == userChannel.site_id);
                if (match != null)
                {
                    match.Selected = true;
                    if (match.xmltv_id != userChannel.xmltv_id)
                    {
                        match.UserXmltvId = userChannel.xmltv_id;
                    }
                }
            }
        }

        // https://stackoverflow.com/questions/331279/how-to-change-diacritic-characters-to-non-diacritic-ones
        // \p{Mn} or \p{Non_Spacing_Mark}: 
        //   a character intended to be combined with another 
        //   character without taking up extra space 
        //   (e.g. accents, umlauts, etc.). 
        private readonly static Regex nonSpacingMarkRegex =
            new Regex(@"\p{Mn}", RegexOptions.Compiled);

        public string RemoveDiacritics(string text)
        {
            if (chkRemoveHypens.Checked)
            {
                var normalizedText = text.Normalize(NormalizationForm.FormD);
                return nonSpacingMarkRegex.Replace(normalizedText, string.Empty);
            }
            return text;
        }

        private void btnXmltvId_Click(object sender, EventArgs e)
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            var selectedChannels = gvChannels.GetSelectedRows();
            foreach (var index in selectedChannels)
            {
                var item = (siteChannel)gvChannels.GetRow(index);
                var cleaned = SmartXmltvId(item);
                cleaned = AddXmltvPrefix(cleaned);
                cleaned = AddXmltvSuffix(cleaned);
                cleaned = RemoveDiacritics(cleaned);
                if (cleaned != item.xmltv_id)
                {
                    item.UserXmltvId = cleaned;
                }
            }
            gridChannels.RefreshDataSource();
        }

        private string SmartXmltvId(siteChannel item)
        {
            if (!string.IsNullOrEmpty(item.xmltv_id))
            {
                var cleaned = item.xmltv_id;
                if (chkXmltvSmart.Checked)
                {
                    cleaned = cleaned.Trim();
                    cleaned = Regex.Replace(cleaned, @"(@|'|\(|\)|<|>|#|\.|,|:|;|\*|\?|=|/|%|\$|!|~|´)", "");
                    cleaned = Regex.Replace(cleaned, @"(\s+|-+|&+)", "_");
                    if (cleaned.Contains("+"))
                    {
                        cleaned = cleaned.Replace("+", "_plus_");
                    }
                    cleaned = cleaned.Trim('_');
                    cleaned = Regex.Replace(cleaned, @"_+", "_");
                    cleaned = cleaned.ToLower();

                }
                return cleaned;
            }
            return item.xmltv_id;
        }

        private string AddXmltvPrefix(string text)
        {
            if (!string.IsNullOrEmpty(txtXmltvPrefix.Text))
            {
                if (text.ToLower().StartsWith(txtXmltvPrefix.Text.ToLower()))
                {
                    text = text.Substring(txtXmltvPrefix.Text.Length);
                }
                text = txtXmltvPrefix.Text + text;
            }
            return text;
        }

        private string AddXmltvSuffix(string text)
        {
            if (!string.IsNullOrEmpty(txtXmltvSuffix.Text))
            {
                if (text.ToLower().EndsWith(txtXmltvSuffix.Text.ToLower()))
                {
                    var length = text.Length - txtXmltvPrefix.Text.Length;
                    text = text.Substring(0, txtXmltvPrefix.Text.Length);
                }
                text += txtXmltvSuffix.Text;
            }
            return text;
        }

        private void gvChannels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && gvChannels.FocusedColumn == colxmltv_id)
            {
                var siteChannel = (siteChannel)gvChannels.GetFocusedRow();
                if (siteChannel != null)
                {
                    var cleaned = SmartXmltvId(siteChannel);
                    cleaned = AddXmltvPrefix(cleaned);
                    cleaned = AddXmltvSuffix(cleaned);
                    cleaned = RemoveDiacritics(cleaned);
                    if (cleaned != siteChannel.xmltv_id)
                    {
                        siteChannel.UserXmltvId = cleaned;
                    }
                    gvChannels.SetFocusedRowCellValue(colUserXmlTvId, cleaned);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                Save();
            }
        }

        private bool IsValid()
        {
            bool valid = true;
            txtName.ErrorText = string.Empty;
            var targetConfigDir = new DirectoryInfo(System.IO.Path.Combine(Locations.UserConfigDirectory.FullName, txtName.Text));
            if (!targetConfigDir.Exists)
            {
                try
                {
                    targetConfigDir.Create();
                    targetConfigDir.Delete();
                }
                catch (Exception)
                {
                    valid = false;
                    txtName.ErrorText = "Invalid directory name";
                }
            }
            else if (Configuration.Name.ToLower() != targetConfigDir.Name.ToLower())
            {
                valid = false;
                txtName.ErrorText = "Configuration already exists";
            }

            UnMarkDuplicates();
            var siteDuplicates = CheckDuplicateSiteXmltvs();
            var userDuplicates = CheckDuplicateUserXmltvs();
            gridChannels.RefreshDataSource();
            if (siteDuplicates || userDuplicates)
            {

                valid = false;
                Log.Error(string.Format("Duplicate xmltv_id detected.{0}Please fix items marked red", Environment.NewLine));
                XtraMessageBox.Show(string.Format("Duplicate xmltv_id detected.{0}Please fix items marked red", Environment.NewLine), "Error");
            }

            return valid;
        }

        private void UnMarkDuplicates()
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            var selected = countriesChannelsList.Where(x => x.Selected).ToList();
            gridChannels.SuspendLayout();
            foreach (var item in selected)
            {
                item.Duplicate = false;
            }
            gridChannels.ResumeLayout();
        }

        private bool CheckDuplicateSiteXmltvs()
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            var selected = countriesChannelsList.Where(x => x.Selected).ToList();
            var duplicates = selected.GroupBy(x => x.site + x.xmltv_id)
              .Where(g => g.Count() > 1)
              .Select(y => y.ToList())
              .ToList();
            foreach (var duplicate in duplicates)
            {
                foreach (var channel in duplicate)
                {
                    channel.Duplicate = true;
                }
            }
            return duplicates.Any();
        }

        private bool CheckDuplicateUserXmltvs()
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            var selected = countriesChannelsList.Where(x => x.Selected).ToList();
            var duplicates = selected.GroupBy(x => (x.UserXmltvId ?? x.xmltv_id))
              .Where(g => g.Count() > 1)
              .Select(y => y)
              .ToList();
            foreach (var duplicate in duplicates)
            {
                foreach (var channel in duplicate)
                {
                    channel.Duplicate = true;
                }
            }
            return duplicates.Any();
        }

        private void Save()
        {
            try
            {
                var targetConfigDir = new DirectoryInfo(System.IO.Path.Combine(Locations.UserConfigDirectory.FullName, txtName.Text));
                var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
                var selected = countriesChannelsList.Where(x => x.Selected).ToList();
                var newConfig = Configuration.Clone();
                newConfig.SetPath(targetConfigDir);
                newConfig.Config = newConfig.Config.Clone();
                newConfig.Config.Path = new FileInfo(Path.Combine(targetConfigDir.FullName, Settings.Default.WebGrabConfigFileName));
                newConfig.Config.channel = new BindingList<siteChannel>(selected);
                if (isEdit)
                {
                    if (targetConfigDir.FullName.ToLower() != Configuration.Path?.FullName.ToLower())
                    {
                        Configuration.Path.MoveTo(targetConfigDir.FullName);
                    }
                }
                //else
                //{
                var xmlOutputFileName = Path.Combine(Locations.OutputDirectory.FullName, txtName.Text.ToLower().Replace(" ", "_") + ".xml");
                if (xmlOutputFileName.ToLower().StartsWith(Locations.OutputDirectory.FullName.ToLower()))
                {
                    xmlOutputFileName = Path.Combine("..\\..", Locations.OutputDirectory.Name, Path.GetFileName(xmlOutputFileName));
                }
                newConfig.Config.filename = xmlOutputFileName;
                //}        
                foreach (var channel in selected)
                {
                    var clonedChannel = channel.Clone();
                    if (!string.IsNullOrEmpty(channel.UserXmltvId))
                    {
                        channel.xmltv_id = channel.UserXmltvId;
                    }
                }
                var selectedCountries = selected.Select(x => x.ParentSite).Select(x => x.Country).Distinct().ToList();
                var selectedSites = selected.Select(x => x.ParentSite).Distinct().ToList();
                var countries = new BindingList<Country>();
                foreach (var selectedCountry in selectedCountries)
                {
                    var clonedCountry = new Country(selectedCountry.Path);
                    clonedCountry.Sites = new BindingList<site>();
                    countries.Add(clonedCountry);
                    var sitesForCountry = selectedSites.Where(x => x.Country == selectedCountry).ToList();
                    foreach (var site in sitesForCountry)
                    {
                        var clonedSite = site.Clone();
                        clonedSite.Country = clonedCountry;
                        clonedCountry.Sites.Add(clonedSite);
                    }
                }
                newConfig.Countries = countries;
                var saveConfCommand = new SaveConfiguration();
                saveConfCommand.StatusChanged += OnStatusChanged;
                saveConfCommand.Execute(newConfig);
                Configuration = newConfig;
                isSaved = true;
                Hide();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Failed to save configuration.{0}{1}", Environment.NewLine, ex.Message), ex);
                XtraMessageBox.Show(string.Format("Failed to save configuration.{0}{1}", Environment.NewLine, ex.Message), "Error");
            }
        }

        private void UserConfigForm_KeyDown(object sender, KeyEventArgs e)
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

        private void gvCountries_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colSelectedCountry)
            {
                if (!_countriesLoaded) return;
                LoadChannels();
            }
        }

        private void repCountrySelected_EditValueChanged(object sender, EventArgs e)
        {
            gvCountries.PostEditor();
        }

        private void repChannelSelected_EditValueChanged(object sender, EventArgs e)
        {
            gvChannels.PostEditor();
        }

        private void gvChannels_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                int rowHandle = e.HitInfo.RowHandle;
                e.Menu.Items.Clear();
                DXMenuItem menuCheckSelected = new DXMenuItem("Check selected", new EventHandler(OnCheckSelected));
                e.Menu.Items.Add(menuCheckSelected);
                DXMenuItem menuUncheckSelected = new DXMenuItem("Uncheck selected", new EventHandler(OnUncheckSelected));
                e.Menu.Items.Add(menuUncheckSelected);
                DXMenuItem menuInvertSelected = new DXMenuItem("Invert selected", new EventHandler(OnInvertSelected));
                e.Menu.Items.Add(menuInvertSelected);
                DXMenuItem menuSelectAll = new DXMenuItem("Check All", new EventHandler(OnCheckAll));
                e.Menu.Items.Add(menuSelectAll);
                DXMenuItem menuDeselectAll = new DXMenuItem("Uncheck All", new EventHandler(OnUncheckAll));
                e.Menu.Items.Add(menuDeselectAll);
                DXMenuItem menuInvertAll = new DXMenuItem("Invert all", new EventHandler(OnInvertAll));
                e.Menu.Items.Add(menuInvertAll);
            }
        }

        void OnCheckSelected(object sender, EventArgs e)
        {
            gridChannels.SuspendLayout();
            var selectedRows = gvChannels.GetSelectedRows();
            foreach (var index in selectedRows)
            {
                gvChannels.SetRowCellValue(index, colSelected, true);
            }
            gridChannels.ResumeLayout();
        }

        void OnUncheckSelected(object sender, EventArgs e)
        {
            gridChannels.SuspendLayout();
            var selectedRows = gvChannels.GetSelectedRows();
            foreach (var index in selectedRows)
            {
                gvChannels.SetRowCellValue(index, colSelected, false);
            }
            gridChannels.ResumeLayout();
        }

        void OnInvertSelected(object sender, EventArgs e)
        {
            gridChannels.SuspendLayout();
            var selectedRows = gvChannels.GetSelectedRows();
            foreach (var index in selectedRows)
            {
                gvChannels.SetRowCellValue(index, colSelected, !(bool)gvChannels.GetRowCellValue(index, colSelected));
            }
            gridChannels.ResumeLayout();
        }

        void OnCheckAll(object sender, EventArgs e)
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            gridChannels.SuspendLayout();
            foreach (var channel in countriesChannelsList)
            {
                channel.Selected = true;
            }
            gridChannels.ResumeLayout();
            gridChannels.RefreshDataSource();
        }

        void OnUncheckAll(object sender, EventArgs e)
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            gridChannels.SuspendLayout();
            foreach (var channel in countriesChannelsList)
            {
                channel.Selected = false;
            }
            gridChannels.ResumeLayout();
            gridChannels.RefreshDataSource();
        }

        void OnInvertAll(object sender, EventArgs e)
        {
            var countriesChannelsList = (BindingList<siteChannel>)gridChannels.DataSource;
            gridChannels.SuspendLayout();
            foreach (var channel in countriesChannelsList)
            {
                channel.Selected = !channel.Selected;
            }
            gridChannels.ResumeLayout();
            gridChannels.RefreshDataSource();
        }

        private void gvChannels_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                bool duplicate = (bool)View.GetRowCellValue(e.RowHandle, colDuplicate);
                if (duplicate)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.HighPriority = true;
                }
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            gvChannels.ExpandAllGroups();
        }

        private void btnColapseAll_Click(object sender, EventArgs e)
        {
            gvChannels.CollapseAllGroups();
        }
    }
}
