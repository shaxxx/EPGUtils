namespace ConfigMaker
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.configurationGroup = new DevExpress.XtraNavBar.NavBarGroup();
            this.addItem = new DevExpress.XtraNavBar.NavBarItem();
            this.editConfigItem = new DevExpress.XtraNavBar.NavBarItem();
            this.editSelectedWebGrabConfigItem = new DevExpress.XtraNavBar.NavBarItem();
            this.deleteItem = new DevExpress.XtraNavBar.NavBarItem();
            this.refreshItem = new DevExpress.XtraNavBar.NavBarItem();
            this.runConfigurationItem = new DevExpress.XtraNavBar.NavBarItem();
            this.toolsGroup = new DevExpress.XtraNavBar.NavBarGroup();
            this.downloadSiteIniItem = new DevExpress.XtraNavBar.NavBarItem();
            this.editWebgrabConfigItem = new DevExpress.XtraNavBar.NavBarItem();
            this.updateSiteInisItem = new DevExpress.XtraNavBar.NavBarItem();
            this.updateSiteKeys = new DevExpress.XtraNavBar.NavBarItem();
            this.channelsMapItem = new DevExpress.XtraNavBar.NavBarItem();
            this.configDirectoryItem = new DevExpress.XtraNavBar.NavBarItem();
            this.outputDirectoryItem = new DevExpress.XtraNavBar.NavBarItem();
            this.aboutItem = new DevExpress.XtraNavBar.NavBarItem();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalCountries = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalSites = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalAvailableChannels = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConfig = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalSelectedChannels = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.mFile = new DevExpress.XtraBars.BarSubItem();
            this.iNew = new DevExpress.XtraBars.BarButtonItem();
            this.iOpen = new DevExpress.XtraBars.BarButtonItem();
            this.iClose = new DevExpress.XtraBars.BarButtonItem();
            this.iSave = new DevExpress.XtraBars.BarButtonItem();
            this.iSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.iExit = new DevExpress.XtraBars.BarButtonItem();
            this.mHelp = new DevExpress.XtraBars.BarSubItem();
            this.iAbout = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.mainLayoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.runAllConfigurationsItem = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl.IsSplitterFixed = true;
            this.splitContainerControl.Location = new System.Drawing.Point(0, 22);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Padding = new System.Windows.Forms.Padding(6);
            this.splitContainerControl.Panel1.Controls.Add(this.navBarControl);
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Controls.Add(this.layoutControl);
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(812, 541);
            this.splitContainerControl.SplitterPosition = 165;
            this.splitContainerControl.TabIndex = 0;
            this.splitContainerControl.Text = "splitContainerControl1";
            // 
            // navBarControl
            // 
            this.navBarControl.ActiveGroup = this.configurationGroup;
            this.navBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl.ExplorerBarShowGroupButtons = false;
            this.navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.configurationGroup,
            this.toolsGroup});
            this.navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.addItem,
            this.editConfigItem,
            this.deleteItem,
            this.downloadSiteIniItem,
            this.updateSiteInisItem,
            this.editWebgrabConfigItem,
            this.updateSiteKeys,
            this.editSelectedWebGrabConfigItem,
            this.aboutItem,
            this.refreshItem,
            this.channelsMapItem,
            this.configDirectoryItem,
            this.outputDirectoryItem,
            this.runConfigurationItem,
            this.runAllConfigurationsItem});
            this.navBarControl.Location = new System.Drawing.Point(0, 0);
            this.navBarControl.Name = "navBarControl";
            this.navBarControl.OptionsNavPane.ExpandedWidth = 165;
            this.navBarControl.OptionsNavPane.ShowExpandButton = false;
            this.navBarControl.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControl.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl.OptionsNavPane.ShowSplitter = false;
            this.navBarControl.Size = new System.Drawing.Size(165, 529);
            this.navBarControl.StoreDefaultPaintStyleName = true;
            this.navBarControl.TabIndex = 0;
            this.navBarControl.Text = "navBarControl1";
            // 
            // configurationGroup
            // 
            this.configurationGroup.Caption = "Configurations";
            this.configurationGroup.Expanded = true;
            this.configurationGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.addItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.editConfigItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.editSelectedWebGrabConfigItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.deleteItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.refreshItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.runConfigurationItem)});
            this.configurationGroup.Name = "configurationGroup";
            this.configurationGroup.SmallImage = ((System.Drawing.Image)(resources.GetObject("configurationGroup.SmallImage")));
            // 
            // addItem
            // 
            this.addItem.Caption = "New";
            this.addItem.Name = "addItem";
            this.addItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("addItem.SmallImage")));
            this.addItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.addItem_LinkClicked);
            // 
            // editConfigItem
            // 
            this.editConfigItem.Caption = "Edit";
            this.editConfigItem.Name = "editConfigItem";
            this.editConfigItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("editConfigItem.SmallImage")));
            this.editConfigItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.editConfigItem_LinkClicked);
            // 
            // editSelectedWebGrabConfigItem
            // 
            this.editSelectedWebGrabConfigItem.Caption = "WebGrab++.config.xml";
            this.editSelectedWebGrabConfigItem.Name = "editSelectedWebGrabConfigItem";
            this.editSelectedWebGrabConfigItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("editSelectedWebGrabConfigItem.SmallImage")));
            this.editSelectedWebGrabConfigItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.editSelectedWebGrabConfigItem_LinkClicked);
            // 
            // deleteItem
            // 
            this.deleteItem.Caption = "Delete";
            this.deleteItem.Name = "deleteItem";
            this.deleteItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("deleteItem.SmallImage")));
            this.deleteItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.deleteItem_LinkClicked);
            // 
            // refreshItem
            // 
            this.refreshItem.Caption = "Refresh";
            this.refreshItem.Name = "refreshItem";
            this.refreshItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("refreshItem.SmallImage")));
            this.refreshItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.refreshItem_LinkClicked);
            // 
            // runConfigurationItem
            // 
            this.runConfigurationItem.Caption = "Run configuration";
            this.runConfigurationItem.Name = "runConfigurationItem";
            this.runConfigurationItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("runConfigurationItem.SmallImage")));
            this.runConfigurationItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.runConfigurationItem_LinkClicked);
            // 
            // toolsGroup
            // 
            this.toolsGroup.Caption = "Tools";
            this.toolsGroup.Expanded = true;
            this.toolsGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.downloadSiteIniItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.editWebgrabConfigItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.updateSiteInisItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.updateSiteKeys),
            new DevExpress.XtraNavBar.NavBarItemLink(this.channelsMapItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.configDirectoryItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.outputDirectoryItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.runAllConfigurationsItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.aboutItem)});
            this.toolsGroup.Name = "toolsGroup";
            this.toolsGroup.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolsGroup.SmallImage")));
            // 
            // downloadSiteIniItem
            // 
            this.downloadSiteIniItem.Caption = "Download siteini.pack";
            this.downloadSiteIniItem.Name = "downloadSiteIniItem";
            this.downloadSiteIniItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("downloadSiteIniItem.SmallImage")));
            this.downloadSiteIniItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.downloadSiteIni_LinkClicked);
            // 
            // editWebgrabConfigItem
            // 
            this.editWebgrabConfigItem.Caption = "WebGrab++.config.xml";
            this.editWebgrabConfigItem.Name = "editWebgrabConfigItem";
            this.editWebgrabConfigItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("editWebgrabConfigItem.SmallImage")));
            this.editWebgrabConfigItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.editWebgrabConfigItem_LinkClicked);
            // 
            // updateSiteInisItem
            // 
            this.updateSiteInisItem.Caption = "Update siteini.user";
            this.updateSiteInisItem.Name = "updateSiteInisItem";
            this.updateSiteInisItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("updateSiteInisItem.SmallImage")));
            this.updateSiteInisItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.updateSiteInis_LinkClicked);
            // 
            // updateSiteKeys
            // 
            this.updateSiteKeys.Caption = "Update site keys";
            this.updateSiteKeys.Name = "updateSiteKeys";
            this.updateSiteKeys.SmallImage = ((System.Drawing.Image)(resources.GetObject("updateSiteKeys.SmallImage")));
            this.updateSiteKeys.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.updateSiteKeys_LinkClicked);
            // 
            // channelsMapItem
            // 
            this.channelsMapItem.Caption = "channels.xml map";
            this.channelsMapItem.Name = "channelsMapItem";
            this.channelsMapItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("channelsMapItem.SmallImage")));
            this.channelsMapItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.channelsMapItem_LinkClicked);
            // 
            // configDirectoryItem
            // 
            this.configDirectoryItem.Caption = "Configurations directory";
            this.configDirectoryItem.Name = "configDirectoryItem";
            this.configDirectoryItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("configDirectoryItem.SmallImage")));
            this.configDirectoryItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.workingDirectoryItem_LinkClicked);
            // 
            // outputDirectoryItem
            // 
            this.outputDirectoryItem.Caption = "Output directory";
            this.outputDirectoryItem.Name = "outputDirectoryItem";
            this.outputDirectoryItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("outputDirectoryItem.SmallImage")));
            this.outputDirectoryItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.outputDirectoryItem_LinkClicked);
            // 
            // aboutItem
            // 
            this.aboutItem.Caption = "About";
            this.aboutItem.Name = "aboutItem";
            this.aboutItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("aboutItem.SmallImage")));
            this.aboutItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.aboutItem_LinkClicked);
            // 
            // layoutControl
            // 
            this.layoutControl.AllowCustomization = false;
            this.layoutControl.Controls.Add(this.grid);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.mainLayoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(630, 529);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(8, 2);
            this.grid.MainView = this.gv;
            this.grid.MenuManager = this.barManager;
            this.grid.Name = "grid";
            this.grid.ShowOnlyPredefinedDetails = true;
            this.grid.Size = new System.Drawing.Size(620, 525);
            this.grid.TabIndex = 4;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            // 
            // gv
            // 
            this.gv.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colPath,
            this.colTotalCountries,
            this.colTotalSites,
            this.colTotalAvailableChannels,
            this.colConfig,
            this.colTotalSelectedChannels});
            this.gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gv.GridControl = this.grid;
            this.gv.Name = "gv";
            this.gv.OptionsBehavior.Editable = false;
            this.gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gv.OptionsView.ShowGroupPanel = false;
            this.gv.OptionsView.ShowIndicator = false;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colPath
            // 
            this.colPath.FieldName = "Path";
            this.colPath.Name = "colPath";
            this.colPath.OptionsColumn.ReadOnly = true;
            // 
            // colTotalCountries
            // 
            this.colTotalCountries.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalCountries.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalCountries.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalCountries.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalCountries.Caption = "Countries";
            this.colTotalCountries.FieldName = "TotalCountries";
            this.colTotalCountries.Name = "colTotalCountries";
            this.colTotalCountries.OptionsColumn.ReadOnly = true;
            this.colTotalCountries.Visible = true;
            this.colTotalCountries.VisibleIndex = 1;
            // 
            // colTotalSites
            // 
            this.colTotalSites.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalSites.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalSites.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalSites.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalSites.Caption = "Sites";
            this.colTotalSites.FieldName = "TotalSites";
            this.colTotalSites.Name = "colTotalSites";
            this.colTotalSites.OptionsColumn.ReadOnly = true;
            this.colTotalSites.ToolTip = "Available sites for selected Countries";
            this.colTotalSites.Visible = true;
            this.colTotalSites.VisibleIndex = 2;
            // 
            // colTotalAvailableChannels
            // 
            this.colTotalAvailableChannels.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalAvailableChannels.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalAvailableChannels.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalAvailableChannels.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalAvailableChannels.Caption = "Total channels";
            this.colTotalAvailableChannels.FieldName = "TotalAvailableChannels";
            this.colTotalAvailableChannels.Name = "colTotalAvailableChannels";
            this.colTotalAvailableChannels.OptionsColumn.ReadOnly = true;
            this.colTotalAvailableChannels.ToolTip = "Total number of available channels for selected Countries";
            this.colTotalAvailableChannels.Visible = true;
            this.colTotalAvailableChannels.VisibleIndex = 3;
            // 
            // colConfig
            // 
            this.colConfig.FieldName = "Config";
            this.colConfig.Name = "colConfig";
            // 
            // colTotalSelectedChannels
            // 
            this.colTotalSelectedChannels.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalSelectedChannels.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalSelectedChannels.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalSelectedChannels.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colTotalSelectedChannels.Caption = "Selected channels";
            this.colTotalSelectedChannels.FieldName = "TotalSelectedChannels";
            this.colTotalSelectedChannels.Name = "colTotalSelectedChannels";
            this.colTotalSelectedChannels.OptionsColumn.ReadOnly = true;
            this.colTotalSelectedChannels.ToolTip = "Number of channels in WebGrab++.config.xml";
            this.colTotalSelectedChannels.Visible = true;
            this.colTotalSelectedChannels.VisibleIndex = 4;
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.mFile,
            this.barButtonItem2,
            this.iOpen,
            this.iClose,
            this.iNew,
            this.iSave,
            this.iSaveAs,
            this.iExit,
            this.mHelp,
            this.iAbout});
            this.barManager.MainMenu = this.bar2;
            this.barManager.MaxItemId = 12;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.mHelp)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // mFile
            // 
            this.mFile.Caption = "&File";
            this.mFile.Id = 0;
            this.mFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.iClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.iExit)});
            this.mFile.Name = "mFile";
            // 
            // iNew
            // 
            this.iNew.Caption = "&New";
            this.iNew.Id = 6;
            this.iNew.Name = "iNew";
            // 
            // iOpen
            // 
            this.iOpen.Caption = "&Open";
            this.iOpen.Id = 4;
            this.iOpen.Name = "iOpen";
            // 
            // iClose
            // 
            this.iClose.Caption = "&Close";
            this.iClose.Id = 5;
            this.iClose.Name = "iClose";
            // 
            // iSave
            // 
            this.iSave.Caption = "&Save";
            this.iSave.Id = 7;
            this.iSave.Name = "iSave";
            // 
            // iSaveAs
            // 
            this.iSaveAs.Caption = "Save &As";
            this.iSaveAs.Id = 8;
            this.iSaveAs.Name = "iSaveAs";
            // 
            // iExit
            // 
            this.iExit.Caption = "E&xit";
            this.iExit.Id = 9;
            this.iExit.Name = "iExit";
            // 
            // mHelp
            // 
            this.mHelp.Caption = "&Help";
            this.mHelp.Id = 10;
            this.mHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iAbout)});
            this.mHelp.Name = "mHelp";
            // 
            // iAbout
            // 
            this.iAbout.Caption = "&About";
            this.iAbout.Id = 11;
            this.iAbout.Name = "iAbout";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(812, 22);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 563);
            this.barDockControlBottom.Size = new System.Drawing.Size(812, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 541);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(812, 22);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 541);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Open";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // mainLayoutControlGroup
            // 
            this.mainLayoutControlGroup.CustomizationFormText = "Root";
            this.mainLayoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.mainLayoutControlGroup.GroupBordersVisible = false;
            this.mainLayoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.mainLayoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutControlGroup.Name = "Root";
            this.mainLayoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 0, 0, 0);
            this.mainLayoutControlGroup.Size = new System.Drawing.Size(630, 529);
            this.mainLayoutControlGroup.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grid;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(624, 529);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // runAllConfigurationsItem
            // 
            this.runAllConfigurationsItem.Caption = "Run all configurations";
            this.runAllConfigurationsItem.Name = "runAllConfigurationsItem";
            this.runAllConfigurationsItem.SmallImage = ((System.Drawing.Image)(resources.GetObject("runAllConfigurationsItem.SmallImage")));
            this.runAllConfigurationsItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.runAllConfigurationsItem_LinkClicked);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 563);
            this.Controls.Add(this.splitContainerControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebGrab++ Config Editor";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem mFile;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem iOpen;
        private DevExpress.XtraBars.BarButtonItem iClose;
        private DevExpress.XtraBars.BarButtonItem iNew;
        private DevExpress.XtraBars.BarButtonItem iSave;
        private DevExpress.XtraBars.BarButtonItem iSaveAs;
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarSubItem mHelp;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.XtraNavBar.NavBarControl navBarControl;
        private DevExpress.XtraNavBar.NavBarGroup configurationGroup;
        private DevExpress.XtraNavBar.NavBarGroup toolsGroup;
        private DevExpress.XtraNavBar.NavBarItem addItem;
        private DevExpress.XtraNavBar.NavBarItem editConfigItem;
        private DevExpress.XtraNavBar.NavBarItem deleteItem;
        private DevExpress.XtraNavBar.NavBarItem downloadSiteIniItem;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup mainLayoutControlGroup;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalCountries;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSites;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAvailableChannels;
        private DevExpress.XtraGrid.Columns.GridColumn colConfig;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalSelectedChannels;
        private DevExpress.XtraNavBar.NavBarItem updateSiteInisItem;
        private DevExpress.XtraNavBar.NavBarItem editWebgrabConfigItem;
        private DevExpress.XtraNavBar.NavBarItem updateSiteKeys;
        private DevExpress.XtraNavBar.NavBarItem editSelectedWebGrabConfigItem;
        private DevExpress.XtraNavBar.NavBarItem aboutItem;
        private DevExpress.XtraNavBar.NavBarItem refreshItem;
        private DevExpress.XtraNavBar.NavBarItem channelsMapItem;
        private DevExpress.XtraNavBar.NavBarItem configDirectoryItem;
        private DevExpress.XtraNavBar.NavBarItem outputDirectoryItem;
        private DevExpress.XtraNavBar.NavBarItem runConfigurationItem;
        private DevExpress.XtraNavBar.NavBarItem runAllConfigurationsItem;
    }
}
