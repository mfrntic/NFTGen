using NFTGenerator.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFTGenerator
{
    public partial class Main : Form
    {
        public Project CurrentProject { get; set; }
        public List<TraitRarityGroupItem> RarityGroupItems { get; set; }

        public Main()
        {
            InitializeComponent();
            CurrentProject = new Project();
            RarityGroupItems = new List<TraitRarityGroupItem>();
            txtTotalItems.Text = this.CurrentProject.TotalItems.ToString();

            // let the listview know that a TraitRarityGroupItem node can expand
            rarityTreeListView.CanExpandGetter = delegate (object rowObject) {
                return (rowObject is TraitRarityGroupItem);
            };

            // retrieving the "TraitRarityItems" from TraitRarityGroupItems
            rarityTreeListView.ChildrenGetter = delegate (object rowObject) {
                TraitRarityGroupItem traitRarityGroupItem = rowObject as TraitRarityGroupItem;
                return traitRarityGroupItem.TraitRarityItems;
            };
        }

        private void LoadGenerated(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var json = File.ReadAllText(path);
                var proj = NFTCollectionProject.FromJSON(json);

                generatedFiles = proj.Tokens;
                outputDataGridView.DataSource = generatedFiles;

            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            //show new project dialog on first load

            // TODO disabled for testing
            // mnuNewProject_Click(null, null);
        }

        #region ADD FOLDER TO TREEVIEW (recursion)

        private void addNode(string fld, ref int numOfGroups, TreeNode parentNode = null)
        {
            string[] dirs = Directory.GetDirectories(fld);
            if (dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    TreeNode nd;
                    numOfGroups++;
                    string folderName = Path.GetFileName(dir);
                    if (parentNode == null)
                    {
                        nd = treeView1.Nodes.Add(folderName);
                    }
                    else
                    {
                        nd = parentNode.Nodes.Add(folderName);
                    }

                    ProjectLayer projectGroup = new ProjectLayer();
                    projectGroup.IsGroup = true;
                    projectGroup.LocalPath = dir;
                    projectGroup.Name = folderName;
                    projectGroup.ID = $"{(char)(65 + (numOfGroups - 1))}";
                    nd.Tag = projectGroup;
                    nd.ImageIndex = 0;
                    nd.SelectedImageIndex = 1;

                    //recursive
                    addNode(dir, ref numOfGroups, nd);

                    //add files
                    string[] fls = Directory.GetFiles(dir, "*.png");
                    int j = 0;
                    foreach (var fl in fls)
                    {
                        j++;
                        string fileName = Path.GetFileNameWithoutExtension(fl);
                        TreeNode ndFile = nd.Nodes.Add(fileName);
                        ndFile.ImageIndex = 2;
                        ndFile.SelectedImageIndex = 2;

                        ProjectLayer projectLayer = new ProjectLayer();
                        projectLayer.IsGroup = false;
                        projectLayer.LocalPath = fl;
                        projectLayer.Name = fileName;
                        projectLayer.ID = $"{(char)(65 + (numOfGroups - 1))}{j}";
                        ndFile.Tag = projectLayer;
                    }
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            statusInfo.Text = "Select main folder which contains folders with trait images...";
            // browse folders
            if (folderBrowse.ShowDialog(this) == DialogResult.OK)
            {
                statusInfo.Text = "Loading trait folders...";
                LoadFolder(folderBrowse.SelectedPath);

                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0];
                }
            }
            statusInfo.Text = "Ready";
        }

        private void LoadFolder(string fn)
        {
            int numOfFolders = 0;
            addNode(fn, ref numOfFolders);
            btnReloadRarityTable_Click(null, null);
        }

        #endregion

        #region MENU ACTIONS

        private void mnuSaveProject_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Saving project data...";
            dlgSave.FileName = this.CurrentProject.ProjectName;
            if (string.IsNullOrEmpty(currentFileName) || !System.IO.File.Exists(currentFileName))
            {
                if (dlgSave.ShowDialog(this) == DialogResult.OK)
                {
                    currentFileName = dlgSave.FileName;
                }
            }
            SaveProject(currentFileName);
        }

        private void SaveProject(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            FillProjectStructure();
            if (string.IsNullOrEmpty(this.CurrentProject.ProjectName))
            {
                this.CurrentProject.ProjectName = Path.GetFileName(fileName);
            }

            File.WriteAllText(fileName, CurrentProject.ToJSON());

            this.Text = $"{ this.CurrentProject.ProjectName}";

            statusInfo.Text = "Project is saved to disk";

        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            //exit
            this.Close();
        }

        private void mnuNewProject_Click(object sender, EventArgs e)
        {
            ProjectForm newProj = new ProjectForm();
            newProj.Text = "New Project";
            newProj.Project = CurrentProject.Copy();
            statusInfo.Text = "Creating new project...";

            if (newProj.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentProject = newProj.Project;
                CreateProject();
            }
            else
            {
                statusInfo.Text = "Ready";
            }
        }

        private void CreateProject()
        {
            this.Text = $"{ this.CurrentProject.ProjectName}";
            treeView1.Nodes.Clear();
            rarityTreeListView.ClearObjects();
            txtTotalItems.Text = this.CurrentProject.TotalItems.ToString();
            statusInfo.Text = "New project is created";
        }

        private void FillProjectStructure(TreeNode tn = null)
        {
            var nodes = (tn == null ? treeView1.Nodes : tn.Nodes);
            List<ProjectLayer> overlays = null;
            if (tn == null)
            {
                CurrentProject.Overlays.Clear();
                overlays = CurrentProject.Overlays;
            }
            else
            {
                var l = tn.Tag as ProjectLayer;
                l.Overlays.Clear();
                overlays = l.Overlays;

            }

            foreach (TreeNode node in nodes)
            {
                ProjectLayer lay = node.Tag as ProjectLayer;
                if (lay.IsGroup)
                {
                    overlays.Add(lay);
                }
                else
                {
                    ProjectLayer parentLay = node.Parent.Tag as ProjectLayer;
                    parentLay.Overlays.Add(lay);
                }

                FillProjectStructure(node);
            }
        }

        private void LoadProject(ProjectLayer layer = null, TreeNode parent = null)
        {
            var overlays = (layer != null ? layer.Overlays : CurrentProject.Overlays);
            var nodes = (parent != null ? parent.Nodes : treeView1.Nodes);

            foreach (var item in overlays)
            {
                TreeNode tn = nodes.Add(item.Name);
                if (item.IsGroup)
                {
                    tn.ImageIndex = 0;
                    tn.SelectedImageIndex = 1;
                }
                else
                {
                    tn.ImageIndex = 2;
                    tn.SelectedImageIndex = 2;
                }
                tn.Tag = item;
                if (item.Overlays.Count > 0)
                {
                    LoadProject(item, tn);
                }
            }


        }

        string currentFileName = "";
        private void mnuOpenProject_Click(object sender, EventArgs e)
        {
            try
            {
                statusInfo.Text = "Loading project from disk...";
                this.Cursor = Cursors.WaitCursor;
                //open project
                if (dlgOpen.ShowDialog(this) == DialogResult.OK)
                {
                    CreateProject();
                    currentFileName = dlgOpen.FileName;
                    CurrentProject = Project.Load(currentFileName);
                    LoadProject();
                    btnReloadRarityTable_Click(null, null);
                    //calcPerc();
                    LoadGenerated(CurrentProject.LastGeneratedJSON);

                    txtTotalItems.Text = this.CurrentProject.TotalItems.ToString();
                    this.Text = $"{ this.CurrentProject.ProjectName}";

                    if (treeView1.Nodes.Count > 0)
                    {
                        treeView1.SelectedNode = treeView1.Nodes[0];
                        treeView1.SelectedNode.Expand();
                        treeView1_AfterSelect(treeView1, new TreeViewEventArgs(treeView1.SelectedNode));
                    }

                    statusInfo.Text = "Project loaded...";
                }
                else
                {
                    statusInfo.Text = "Ready";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("File load error. Check if it is compatible with NFTGen!\n" + ex.Message,"File Corrupted",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuProjectSettings_Click(object sender, EventArgs e)
        {
            // show setting
            ProjectForm sett = new ProjectForm();
            sett.Text = "Project Settings";
            sett.Project = CurrentProject.Copy();
            if (sett.ShowDialog(this) == DialogResult.OK)
            {
                CurrentProject = sett.Project;
            }
        }
        #endregion

        #region TREEVIEW / FOLDER ACTIONS
        // button remove selected item
        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Removing item from tree...";
            treeView1.Nodes.Remove(treeView1.SelectedNode);
            btnReloadRarityTable_Click(null, null);
            statusInfo.Text = "Ready";
        }

        // action after treeview select
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnAddFile.Enabled = btnUp.Enabled = btnDown.Enabled = btnRemoveFolder.Enabled = e.Node != null;
            ProjectLayer lay = e.Node.Tag as ProjectLayer;

            pgProjLay.SelectedObject = lay;

            if (lay.IsGroup)
            {
                /*GalleryItemGroup group = new GalleryItemGroup();
                group.Caption = lay.Name;

                foreach (TreeNode tn in e.Node.Nodes)
                {
                    Lib.ProjectLayer overlay = tn.Tag as Lib.ProjectLayer;
                    if (!overlay.IsGroup)
                    {
                        if (File.Exists(overlay.LocalPath))
                        {
                            var gi = new GalleryItem(Image.FromFile(overlay.LocalPath).GetThumbnailImage(100, 100, null, new IntPtr()), overlay.Name, "");
                            gi.Tag = tn;
                            group.Items.Add(gi);
                        }
                        else
                        {
                            MessageBox.Show($"Selected [{overlay.LocalPath}] image does not anymore exist.", "Image does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }*/
            }
            else
            {
                ProjectLayer overlay = e.Node.Tag as ProjectLayer;
                if (File.Exists(overlay.LocalPath))
                {
                    pictureBox.Image = Image.FromFile(overlay.LocalPath);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    MessageBox.Show($"Selected [{overlay.LocalPath}] image does not anymore exist.", "Image does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            statusInfo.Text = "Ready";

        }

        // button move item up the tree
        private void btnUp_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Moving item up the tree...";
            treeView1.BeginUpdate();
            TreeNode sel = treeView1.SelectedNode;
            if (sel != null)
            {
                treeView1.SelectedNode.MoveUp();
                treeView1.SelectedNode = sel;
            }
            treeView1.EndUpdate();
            btnReloadRarityTable_Click(null, null);
            statusInfo.Text = "Ready";
        }

        // button move item down the tree
        private void btnDown_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Moving item down the tree...";
            treeView1.BeginUpdate();
            TreeNode sel = treeView1.SelectedNode;
            if (sel != null)
            {
                treeView1.SelectedNode.MoveDown();
                treeView1.SelectedNode = sel;
            }
            treeView1.EndUpdate();
            btnReloadRarityTable_Click(null, null);
            statusInfo.Text = "Ready";
        }


        // button add file
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (dlgAddFile.ShowDialog(this) == DialogResult.OK)
            {
                statusInfo.Text = "Adding file to tree structure...";
                TreeNode nd = treeView1.SelectedNode;
                if (nd.ImageIndex == 2)
                {
                    nd = nd.Parent;
                }

                string fl = dlgAddFile.FileName;
                string fileName = Path.GetFileNameWithoutExtension(fl);


                TreeNode ndFile = nd.Nodes.Add(fileName);
                ndFile.ImageIndex = 2;
                ndFile.SelectedImageIndex = 2;

                ProjectLayer parentLayer = nd.Tag as ProjectLayer;

                ProjectLayer projectLayer = new ProjectLayer();
                projectLayer.IsGroup = false;
                projectLayer.LocalPath = fl;
                projectLayer.Name = fileName;
                projectLayer.ID = $"{parentLayer.ID}{nd.Nodes.Count}";
                ndFile.Tag = projectLayer;
                statusInfo.Text = "Ready";

            }
        }
        #endregion

        #region TREEVIEW (FOLDERS) DRAG & DROP

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Move the dragged node when the left mouse button is used.
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // Copy the dragged node when the right mouse button is used.
            else if (e.Button == MouseButtons.Right)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        // Set the target drop effect to the effect 
        // specified in the ItemDrag event handler.
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        // Select the node under the mouse pointer to indicate the 
        // expected drop location.
        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            treeView1.SelectedNode = treeView1.GetNodeAt(targetPoint);
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeView1.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current 
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();
                    if (targetNode != null)
                    {
                        targetNode.Nodes.Add(draggedNode);
                    }
                    else
                    {
                        treeView1.Nodes.Add(draggedNode);
                    }
                }

                // If it is a copy operation, clone the dragged node 
                // and add it to the node at the drop location.
                else if (e.Effect == DragDropEffects.Copy)
                {
                    if (targetNode != null)
                    {
                        targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                    }
                    else
                    {
                        treeView1.Nodes.Add((TreeNode)draggedNode.Clone());
                    }
                }

                // Expand the node at the location 
                // to show the dropped node.
                if (targetNode != null)
                {
                    targetNode.Expand();
                }
                btnReloadRarityTable_Click(null, null);
            }
        }


        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2 == null || node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }
        #endregion

        #region RARITY TABLE

        private void btnReloadRarityTable_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Loading rarity table...";

            var nodes = treeView1.Nodes;
            foreach (TreeNode node in nodes)
            {
                ProjectLayer layGroup = node.Tag as ProjectLayer;
                var traitRarityGroupItem = new TraitRarityGroupItem
                {
                    TraitName = layGroup.Name,
                    TraitId = layGroup.ID,
                    NumberOfOccurences = layGroup.Rarity,
                    RarityPercentage = layGroup.RarityPerc,
                    TraitRarityItems = new List<TraitRarityItem>()
                };

                RarityGroupItems.Add(traitRarityGroupItem);

                foreach(TreeNode n in node.Nodes)
                {
                    ProjectLayer layChild = n.Tag as ProjectLayer;
                    traitRarityGroupItem.TraitRarityItems.Add(new TraitRarityItem
                    {
                        TraitName = layChild.Name,
                        TraitId = layChild.ID,
                        NumberOfOccurences = layChild.Rarity,
                        RarityPercentage = layChild.RarityPerc,
                    });
                }                  
            }
            rarityTreeListView.AddObjects(RarityGroupItems);
            statusInfo.Text = "Ready";
        }        

        private void rarityTreeListView_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            var allNodes = treeView1.Descendants();
            statusInfo.Text = "Computing rarity percentage...";

            if (e.RowObject is TraitRarityGroupItem)
            { 
                var traitRarityGroupItemObject = e.RowObject as TraitRarityGroupItem;
                var treeViewGroupNode = allNodes.FirstOrDefault(node => (node.Tag as ProjectLayer).ID == traitRarityGroupItemObject.TraitId);

                if(treeViewGroupNode != null)
                {
                    ProjectLayer currentLayer = treeViewGroupNode.Tag as ProjectLayer;
                    currentLayer.Rarity = int.Parse(e.NewValue.ToString());

                    // update all children
                    foreach (TraitRarityItem traitRarityItem in traitRarityGroupItemObject.TraitRarityItems)
                    {
                        var treeViewChildNode = allNodes.FirstOrDefault(node => (node.Tag as ProjectLayer).ID == traitRarityItem.TraitId);
                        if (treeViewChildNode != null)
                        {
                            ProjectLayer childLayer = treeViewChildNode.Tag as ProjectLayer;
                            childLayer.RarityPerc = Math.Round(childLayer.Rarity * 1.0 / currentLayer.Rarity * 100, 2);
                            traitRarityItem.RarityPercentage = childLayer.RarityPerc;
                        }                                   
                    }
                }
            }
            else if(e.RowObject is TraitRarityItem)
            {
                var traitRarityItemObject = e.RowObject as TraitRarityItem;

                var treeViewChildNode = allNodes.FirstOrDefault(node => (node.Tag as ProjectLayer).ID == traitRarityItemObject.TraitId);
                if (treeViewChildNode != null)
                {
                    ProjectLayer currentLayer = treeViewChildNode.Tag as ProjectLayer;
                    currentLayer.Rarity = int.Parse(e.NewValue.ToString());

                    ProjectLayer parentLayer = treeViewChildNode.Parent.Tag as ProjectLayer;
                    currentLayer.RarityPerc = Math.Round(currentLayer.Rarity * 1.0 / parentLayer.Rarity * 100, 2);
                    traitRarityItemObject.RarityPercentage = currentLayer.RarityPerc;
                }
            }

            statusInfo.Text = "Ready";
        }

        // set total number of tokens
        private void txtTotalItems_TextChanged(object sender, EventArgs e)
        {
            this.CurrentProject.TotalItems = int.Parse(txtTotalItems.Text);
        }

        private void txtTotalItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow numbers only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region GENERATE IMAGES

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = false;
            string outputPath = CurrentProject.Settings.GetOutputPath(CurrentProject);
            statusInfo.Text = "Prepare for image generation...";
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            else
            {
                if (Directory.GetFileSystemEntries(outputPath).Length > 0)
                {
                    if (MessageBox.Show("Continuation of work will delete the contents of the entire output folder.\nDo you want to continue?", "The output folder is not empty", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        btnGenerate.Enabled = true;
                        return;
                    }
                }

                await Task.Run(() =>
                {
                    statusInfo.Text = "Deleting existing files in the output folder...";

                    var delFiles = Directory.GetFileSystemEntries(outputPath);

                    // delete from output folder
                    this.Invoke(new Action(() =>
                    {
                        prg1.Visible = true;
                        prg1.Minimum = 0;
                        prg1.Maximum = delFiles.Length;
                        prg1.Value = 0;
                    }));
                    foreach (var delfl in delFiles)
                    {
                        var attr = File.GetAttributes(delfl);
                        if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            // dir
                            Directory.Delete(delfl, true);
                        }
                        else
                        {
                            try
                            {
                                // file
                                File.Delete(delfl);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.InnerException);
                            }
                        }
                        this.Invoke(new Action(() =>
                        {
                            prg1.Increment(1);
                        }));
                    }
                    delFiles = Directory.GetFileSystemEntries(outputPath);
                });
            }

            statusInfo.Text = "Generating images...";

            generatedFiles = new List<NFTCollectionItem>();
            outputDataGridView.DataSource = generatedFiles;
            btnGenerateCancel.Enabled = true;

            // create collection with empty nft items
            allFiles = NFTCollectionItem.CreateCollection(CurrentProject, int.Parse(txtStartTokenID.Text));

            metaList = new List<NFTMetaCollectionItem>();
            await Task.Run(() =>
            {
                foreach (NFTCollectionItem item in allFiles)
                {
                    NFTMetaCollectionItem t = new NFTMetaCollectionItem
                    {
                        tokenId = item.TokenID,
                        name = item.TokenID.ToString(),
                        description = "description todo",
                        image = "ipfs://base/" + item.TokenID.ToString() + ".png",
                        attributes = new List<Trait>()
                    };

                    foreach (var trait in item.Traits)
                    {
                        t.attributes.Add(new Trait
                        {
                            trait_type = trait.Key,
                            value = trait.Value.Name
                        });
                    }

                    metaList.Add(t);
                }
            });

            prg1.Minimum = 0;
            prg1.Maximum = allFiles.Count;
            prg1.Value = 0;
            timerGen.Enabled = true;
            prg1.Visible = true;
            lblGenProgress.Text = $"{0}/{allFiles.Count} ({Math.Round(0 * 1.0 / allFiles.Count * 100, 2)}%)";

            await Task.Run(() =>
            {
                cts = new CancellationTokenSource();
                ParallelOptions po = new ParallelOptions();
                po.CancellationToken = cts.Token;
                po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
                Parallel.ForEach(allFiles, po,
                    async (item, state) =>
                    {
                        try
                        {
                            po.CancellationToken.ThrowIfCancellationRequested();
                            await item.GenerateImageAsync(CurrentProject, cts.Token);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                        //if (po.CancellationToken.IsCancellationRequested)
                        //{
                        //    state.Break();
                        //}
                        //else
                        //{
                        //    try
                        //    {
                        //        await item.GenerateImageAsync(CurrentProject, cts.Token);

                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //    }
                        //}
                    }
                );
            });
        }

        public List<NFTCollectionItem> allFiles;
        public List<NFTCollectionItem> generatedFiles;
        public List<NFTMetaCollectionItem> metaList;
        CancellationTokenSource cts = null;

        private async void timerGen_Tick(object sender, EventArgs e)
        {
            string outputPath = CurrentProject.Settings.GetOutputPath(CurrentProject);
            var processed = Directory.GetFiles(outputPath, "*.png");

            if (prg1.Maximum >= processed.Length)
            {
                prg1.Value = processed.Length;
            }
            else
            {
                prg1.Value = prg1.Maximum;
            }

            if (prg1.Value == 0) return;

            foreach (var item in processed)
            {
                if (generatedFiles.Where(a => a.LocalPath == item).Count() == 0)
                {
                    var af = allFiles.Where(x => x.LocalPath == item).Single();
                    if (af != null)
                    {
                        generatedFiles.Add(af);
                    }
                }
            }

            outputDataGridView.DataSource = generatedFiles;

            lblGenProgress.Text = $"{processed.Length}/{CurrentProject.TotalItems} ({Math.Round(processed.Length * 1.0 / CurrentProject.TotalItems * 100, 2)}%)";

            if (processed.Length == CurrentProject.TotalItems)
            {
                prg1.Visible = false;
                btnGenerate.Enabled = true;
                btnGenerateCancel.Enabled = false;
                timerGen.Enabled = false;
                lblGenProgress.Text = "";

                // save final json
                var finalJSONFile = Path.Combine(outputPath, $"{CurrentProject.ProjectName}_db.json");
                statusInfo.Text = $"Saving final JSON file [{finalJSONFile}]...";

                NFTCollectionProject nftProject = new NFTCollectionProject(CurrentProject.ProjectName);
                nftProject.Tokens = generatedFiles;

                var json = nftProject.ToJSON();

                CurrentProject.LastGeneratedJSON = finalJSONFile;

                await Task.Run(() =>
                {
                    File.WriteAllText(finalJSONFile, json);
                    foreach (var tokenMeta in metaList)
                    {
                        var tokenMetaJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(tokenMeta, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(Path.Combine(outputPath, $"{tokenMeta.tokenId}.json"), tokenMetaJsonString);
                    }
                });

                outputDataGridView.DataSource = null;
                outputDataGridView.DataSource = generatedFiles;
                statusInfo.Text = "Ready";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //search in grid
            //var view = ((DevExpress.XtraGrid.Views.Base.ColumnView)outputGrid.DefaultView);
            //if (!string.IsNullOrEmpty(txtSearch.Text))
            //{
            //    view.ActiveFilterCriteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("Contains([FileName], '" + txtSearch.Text + "')");
            //}
            //else
            //{
            //    view.ActiveFilter.Clear();
            //}
        }

        private void btnGenerateCancel_Click(object sender, EventArgs e)
        {
            // cancel build
            cts.Cancel();
            prg1.Visible = false;
            btnGenerate.Enabled = true;
            btnGenerateCancel.Enabled = false;
            timerGen.Enabled = false;
            lblGenProgress.Text = "";
        }

        #endregion        
    }
}
