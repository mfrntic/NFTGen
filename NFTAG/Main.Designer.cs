
namespace NFTAG
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, null, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetProjectName = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imlFolders = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddFolder = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUp = new System.Windows.Forms.ToolStripButton();
            this.btnDown = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gallery1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tlRT = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnReloadRarityTable = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTotalItems = new System.Windows.Forms.ToolStripTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.folderBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.picPrev = new DevExpress.XtraEditors.PictureEdit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gallery1)).BeginInit();
            this.gallery1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlRT)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPrev.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.projectToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1189, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewProject,
            this.mnuSaveProject,
            this.mnuOpenProject,
            this.toolStripMenuItem1,
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuNewProject
            // 
            this.mnuNewProject.Name = "mnuNewProject";
            this.mnuNewProject.Size = new System.Drawing.Size(195, 22);
            this.mnuNewProject.Text = "&New Project...";
            this.mnuNewProject.Click += new System.EventHandler(this.mnuNewProject_Click);
            // 
            // mnuSaveProject
            // 
            this.mnuSaveProject.Name = "mnuSaveProject";
            this.mnuSaveProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSaveProject.Size = new System.Drawing.Size(195, 22);
            this.mnuSaveProject.Text = "&Save Project...";
            this.mnuSaveProject.Click += new System.EventHandler(this.mnuSaveProject_Click);
            // 
            // mnuOpenProject
            // 
            this.mnuOpenProject.Name = "mnuOpenProject";
            this.mnuOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenProject.Size = new System.Drawing.Size(195, 22);
            this.mnuOpenProject.Text = "&Open Project...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(195, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetProjectName});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "&Project";
            // 
            // mnuSetProjectName
            // 
            this.mnuSetProjectName.Name = "mnuSetProjectName";
            this.mnuSetProjectName.Size = new System.Drawing.Size(174, 22);
            this.mnuSetProjectName.Text = "Set Project &Name...";
            this.mnuSetProjectName.Click += new System.EventHandler(this.mnuSetProjectName_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1189, 726);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imlFolders;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(291, 701);
            this.treeView1.TabIndex = 1;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
            // 
            // imlFolders
            // 
            this.imlFolders.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlFolders.ImageStream")));
            this.imlFolders.TransparentColor = System.Drawing.Color.Transparent;
            this.imlFolders.Images.SetKeyName(0, "Folder-icon.png");
            this.imlFolders.Images.SetKeyName(1, "Folder-Open-icon.png");
            this.imlFolders.Images.SetKeyName(2, "Layer-Backward-icon.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddFolder,
            this.btnRemoveFolder,
            this.toolStripSeparator1,
            this.btnUp,
            this.btnDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(291, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFolder.Image")));
            this.btnAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(23, 22);
            this.btnAddFolder.Text = "Add Folders";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // btnRemoveFolder
            // 
            this.btnRemoveFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveFolder.Enabled = false;
            this.btnRemoveFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveFolder.Image")));
            this.btnRemoveFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveFolder.Name = "btnRemoveFolder";
            this.btnRemoveFolder.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveFolder.Text = "Remove Folder";
            this.btnRemoveFolder.Click += new System.EventHandler(this.btnRemoveFolder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUp
            // 
            this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUp.Enabled = false;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(23, 22);
            this.btnUp.Text = "Move Up";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDown.Enabled = false;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(23, 22);
            this.btnDown.Text = "Move Down";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(894, 726);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.picPrev);
            this.tabPage1.Controls.Add(this.gallery1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(886, 700);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Preview";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gallery1
            // 
            this.gallery1.Controls.Add(this.galleryControlClient1);
            this.gallery1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.gallery1.Gallery.ImageSize = new System.Drawing.Size(100, 100);
            this.gallery1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.gallery1.Gallery.ItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.gallery1_Gallery_ItemClick);
            this.gallery1.Location = new System.Drawing.Point(3, 3);
            this.gallery1.Name = "gallery1";
            this.gallery1.Size = new System.Drawing.Size(880, 694);
            this.gallery1.TabIndex = 0;
            this.gallery1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.gallery1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(859, 690);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tlRT);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(886, 700);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Rarity Table";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tlRT
            // 
            this.tlRT.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4});
            this.tlRT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlRT.Location = new System.Drawing.Point(3, 28);
            this.tlRT.Name = "tlRT";
            this.tlRT.Size = new System.Drawing.Size(880, 669);
            this.tlRT.TabIndex = 0;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Trait name";
            this.treeListColumn1.FieldName = "Trait name";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Trait ID";
            this.treeListColumn2.FieldName = "Trait ID";
            this.treeListColumn2.MaxWidth = 100;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Rarity";
            this.treeListColumn3.FieldName = "Rarity";
            this.treeListColumn3.MaxWidth = 100;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 2;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Rarity %";
            this.treeListColumn4.FieldName = "Rarity %";
            this.treeListColumn4.MaxWidth = 100;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.btnReloadRarityTable,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.txtTotalItems});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(880, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnReloadRarityTable
            // 
            this.btnReloadRarityTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReloadRarityTable.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadRarityTable.Image")));
            this.btnReloadRarityTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReloadRarityTable.Name = "btnReloadRarityTable";
            this.btnReloadRarityTable.Size = new System.Drawing.Size(23, 22);
            this.btnReloadRarityTable.Text = "toolStripButton3";
            this.btnReloadRarityTable.Click += new System.EventHandler(this.btnReloadRarityTable_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(98, 22);
            this.toolStripLabel1.Text = "Total Item Count:";
            // 
            // txtTotalItems
            // 
            this.txtTotalItems.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTotalItems.Name = "txtTotalItems";
            this.txtTotalItems.Size = new System.Drawing.Size(100, 25);
            this.txtTotalItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalItems_KeyPress);
            this.txtTotalItems.TextChanged += new System.EventHandler(this.txtTotalItems_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(886, 700);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 750);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1189, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusInfo
            // 
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(1174, 17);
            this.statusInfo.Spring = true;
            this.statusInfo.Text = "...";
            this.statusInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "NFT Collection|*.nftc|All Files|*.*";
            this.dlgSave.Title = "Save Project";
            // 
            // picPrev
            // 
            this.picPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPrev.Location = new System.Drawing.Point(3, 3);
            this.picPrev.Name = "picPrev";
            this.picPrev.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picPrev.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picPrev.Size = new System.Drawing.Size(880, 694);
            this.picPrev.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 772);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NFTGen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gallery1)).EndInit();
            this.gallery1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlRT)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPrev.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddFolder;
        private System.Windows.Forms.ToolStripButton btnRemoveFolder;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowse;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusInfo;
        private System.Windows.Forms.ImageList imlFolders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUp;
        private System.Windows.Forms.ToolStripButton btnDown;
        private DevExpress.XtraBars.Ribbon.GalleryControl gallery1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraTreeList.TreeList tlRT;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnReloadRarityTable;
        private System.Windows.Forms.ToolStripMenuItem mnuNewProject;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveProject;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSetProjectName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtTotalItems;
        private DevExpress.XtraEditors.PictureEdit picPrev;
    }
}