using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFTAG
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            CurrentProject = new Lib.Project();
            txtTotalItems.Text = this.CurrentProject.TotalItems.ToString();
        }

        public Lib.Project CurrentProject { get; set; }

        #region ADD FOLDER TO TREEVIEW

        private void addNode(string fld, ref int numOfGroups, TreeNode parentNode = null)
        {
            string[] dirs = System.IO.Directory.GetDirectories(fld);
            if (dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    TreeNode nd;
                    numOfGroups++;
                    string folderName = System.IO.Path.GetFileName(dir);
                    if (parentNode == null)
                    {
                        nd = treeView1.Nodes.Add(folderName);
                    }
                    else
                    {
                        nd = parentNode.Nodes.Add(folderName);
                    }

                    Lib.ProjectLayer projectGroup = new Lib.ProjectLayer();
                    projectGroup.IsGroup = true;
                    projectGroup.Path = dir;
                    projectGroup.Name = folderName;
                    projectGroup.ID = $"{(char)(65 + (numOfGroups - 1))}";
                    nd.Tag = projectGroup;
                    nd.ImageIndex = 0;
                    nd.SelectedImageIndex = 1;

                    //recursive
                    addNode(dir, ref numOfGroups, nd);

                    //add files
                    string[] fls = System.IO.Directory.GetFiles(dir, "*.png");
                    int j = 0;
                    foreach (var fl in fls)
                    {
                        j++;
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(fl);
                        TreeNode ndFile = nd.Nodes.Add(fileName);
                        ndFile.ImageIndex = 2;
                        ndFile.SelectedImageIndex = 2;

                        Lib.ProjectLayer projectLayer = new Lib.ProjectLayer();
                        projectLayer.IsGroup = false;
                        projectLayer.Path = fl;
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
            //browse folders
            if (folderBrowse.ShowDialog(this) == DialogResult.OK)
            {

                statusInfo.Text = "Loading trait folders...";
                LoadFolder(folderBrowse.SelectedPath);

                //set base project folder
                CurrentProject.BaseFolder = folderBrowse.SelectedPath;

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

        //remove selected item
        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
            btnReloadRarityTable_Click(null, null);
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Odabir foldera u treeview kontroli
            //omogući/onemogući gumbe
            btnAddFile.Enabled = btnUp.Enabled = btnDown.Enabled = btnRemoveFolder.Enabled = e.Node != null;
            //očisti galeriju
            gallery1.Gallery.Groups.Clear();

            Lib.ProjectLayer lay = e.Node.Tag as Lib.ProjectLayer;

            pgProjLay.SelectedObject = lay;

            if (lay.IsGroup) //ako je folder prikaži galeriju
            {
                picPrev.Visible = false;
                gallery1.Visible = true;
                //load images
                GalleryItemGroup group = new GalleryItemGroup();
                group.Caption = lay.Name;

                gallery1.Gallery.Groups.Add(group);

                foreach (TreeNode tn in e.Node.Nodes)
                {
                    Lib.ProjectLayer overlay = tn.Tag as Lib.ProjectLayer;
                    if (!overlay.IsGroup)
                    {
                        var gi = new GalleryItem(Image.FromFile(overlay.Path).GetThumbnailImage(100, 100, null, new IntPtr()), overlay.Name, "");
                        gi.Tag = tn;
                        group.Items.Add(gi);
                    }
                }
            }
            else //ako je file prikaži sliku
            {
                picPrev.Visible = true;
                gallery1.Visible = false;
                Lib.ProjectLayer overlay = e.Node.Tag as Lib.ProjectLayer;
                picPrev.Image = Image.FromFile(overlay.Path);

            }
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            //move item up the tree
            TreeNode sel = treeView1.SelectedNode;
            if (sel != null)
            {
                treeView1.SelectedNode.MoveUp();
                treeView1.SelectedNode = sel;
            }
            treeView1.EndUpdate();
            btnReloadRarityTable_Click(null, null);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            //move item down the tree
            TreeNode sel = treeView1.SelectedNode;
            if (sel != null)
            {
                treeView1.SelectedNode.MoveDown();
                treeView1.SelectedNode = sel;
            }
            treeView1.EndUpdate();
            btnReloadRarityTable_Click(null, null);
        }

        #region RARITY TABLE

        private void gallery1_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            TreeNode tn = e.Item.Tag as TreeNode;
            treeView1.SelectedNode = tn;

        }

        /// <summary>
        /// Load Rarity table from treeview folder structure
        /// </summary>
        private void CreateRarityTableFromFolders(TreeNode tn = null, TreeListNode parent = null)
        {
            var nodes = (tn == null ? treeView1.Nodes : tn.Nodes);
            foreach (TreeNode node in nodes)
            {
                Lib.ProjectLayer lay = node.Tag as Lib.ProjectLayer;

                TreeListNode tln = tlRT.AppendNode(new object[] { lay.Name, lay.ID, lay.Rarity, lay.RarityPerc }, parent);
                tln.Tag = node;

                if (node.Nodes.Count > 0)
                {
                    CreateRarityTableFromFolders(node, tln);
                }
            }
        }

        private void btnReloadRarityTable_Click(object sender, EventArgs e)
        {
            //Osvježi rarity table
            tlRT.BeginUnboundLoad();
            tlRT.Nodes.Clear();
            CreateRarityTableFromFolders();
            //calcPerc();

            tlRT.EndUnboundLoad();
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
                    targetNode.Nodes.Add(draggedNode);
                }

                // If it is a copy operation, clone the dragged node 
                // and add it to the node at the drop location.
                else if (e.Effect == DragDropEffects.Copy)
                {
                    targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                }

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();
                btnReloadRarityTable_Click(null, null);
            }
        }


        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }
        #endregion

        private void mnuSaveProject_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Saving project data...";
            dlgSave.FileName = this.CurrentProject.ProjectName;

            //spremi
            if (string.IsNullOrEmpty(currentFileName) || !System.IO.File.Exists(currentFileName))
            {
                if (dlgSave.ShowDialog(this) == DialogResult.OK)
                {
                    //spremi projekt
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
                this.CurrentProject.ProjectName = System.IO.Path.GetFileName(fileName);
            }

            System.IO.File.WriteAllText(fileName, CurrentProject.ToJSON());

            this.Text = $"NFTGen :: { this.CurrentProject.ProjectName}";

            statusInfo.Text = "Project is saved to disk... Ready...";

        }

        private void mnuSetProjectName_Click(object sender, EventArgs e)
        {
            //set project name
            this.CurrentProject.ProjectName = XtraInputBox.Show("Set project name", "Project Name", "");
            this.Text = $"NFTGen :: { this.CurrentProject.ProjectName}";
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            //exit
            this.Close();
        }

        private void mnuNewProject_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Creating new project...";
            //new project
            this.CurrentProject = new Lib.Project();
            this.Text = $"NFTGen";
            treeView1.Nodes.Clear();
            tlRT.Nodes.Clear();
            txtTotalItems.Text = this.CurrentProject.TotalItems.ToString();
            statusInfo.Text = "New project created...";

        }

        private void txtTotalItems_TextChanged(object sender, EventArgs e)
        {
            this.CurrentProject.TotalItems = int.Parse(txtTotalItems.Text);
        }

        private void txtTotalItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow numbers only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        //private void calcPerc(TreeListNode tln = null)
        //{
        //    var nodes = (tln != null ? tln.Nodes : tlRT.Nodes);
        //    foreach (TreeListNode item in nodes)
        //    {
        //        Lib.ProjectLayer lyCurr = (item.Tag as TreeNode).Tag as Lib.ProjectLayer;
        //        if (!lyCurr.IsGroup)
        //        {
        //            Lib.ProjectLayer lyParent = (item.ParentNode.Tag as TreeNode).Tag as Lib.ProjectLayer;
        //            item.SetValue(3, Math.Round(lyCurr.Rarity * 1.0 / lyParent.Rarity * 100, 2));
        //        }
        //        calcPerc(item);
        //    }
        //}

        private void tlRT_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //promjena vrijednosti u rarity table
            Lib.ProjectLayer lay = ((TreeNode)e.Node.Tag).Tag as Lib.ProjectLayer;
            if (e.Column.FieldName == "Name")
            {
                lay.Name = e.Value.ToString();
            }
            else if (e.Column.FieldName == "Rarity")
            {
                lay.Rarity = int.Parse(e.Value.ToString());
                if (lay.IsGroup)
                {
                    //update svih childova
                    foreach (TreeListNode item in e.Node.Nodes)
                    {
                        Lib.ProjectLayer l = ((TreeNode)item.Tag).Tag as Lib.ProjectLayer;
                        l.RarityPerc = Math.Round(l.Rarity * 1.0 / lay.Rarity * 100, 2);
                        item.SetValue(3, l.RarityPerc);
                    }
                }
                else
                {
                    Lib.ProjectLayer l = ((TreeNode)e.Node.ParentNode.Tag).Tag as Lib.ProjectLayer;
                    lay.RarityPerc = Math.Round(lay.Rarity * 1.0 / l.Rarity * 100, 2);
                    e.Node.SetValue(3, lay.RarityPerc);
                }
                // calcPerc();
            }
        }

        private void FillProjectStructure(TreeNode tn = null)
        {
            var nodes = (tn == null ? treeView1.Nodes : tn.Nodes);
            List<Lib.ProjectLayer> overlays = null;
            if (tn == null)
            {
                CurrentProject.Overlays.Clear();
                overlays = CurrentProject.Overlays;
            }
            else
            {
                var l = tn.Tag as Lib.ProjectLayer;
                l.Overlays.Clear();
                overlays = l.Overlays;

            }

            foreach (TreeNode node in nodes)
            {
                Lib.ProjectLayer lay = node.Tag as Lib.ProjectLayer;
                if (lay.IsGroup)
                {
                    overlays.Add(lay);
                }
                else
                {
                    Lib.ProjectLayer parentLay = node.Parent.Tag as Lib.ProjectLayer;
                    parentLay.Overlays.Add(lay);
                }

                FillProjectStructure(node);
            }
        }

        private void LoadProject(Lib.ProjectLayer layer = null, TreeNode parent = null)
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
            statusInfo.Text = "Loading project from disk...";

            //open project
            if (dlgOpen.ShowDialog(this) == DialogResult.OK)
            {
                mnuNewProject_Click(null, null);
                currentFileName = dlgOpen.FileName;
                CurrentProject = Lib.Project.Load(currentFileName);
                LoadProject();
                btnReloadRarityTable_Click(null, null);
                //calcPerc();

                txtTotalItems.Text = this.CurrentProject.TotalItems.ToString();
                this.Text = $"NFTGen :: { this.CurrentProject.ProjectName}";

                statusInfo.Text = "Project loaded...";
            }
            else
            {
                statusInfo.Text = "Ready...";
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                FillProjectStructure();
                var style = "<style>body{ background-color: 'black'; font-family: 'Courier New'; font-size: '11pt'; color: 'white'; } .key{ color: 'CornflowerBlue'; } .string {color: 'Lime'} .number { color: 'Yellow'; } .boolean { color: 'magenta' } .null { color: 'gray'; }</style>";
                webBrowser1.DocumentText = style + SyntaxHighlightJson(CurrentProject.ToJSON().Replace("\n", "<br>").Replace(" ", "&nbsp;"));
            }
        }

        private string SyntaxHighlightJson(string original)
        {
            return Regex.Replace(
              original,
              @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)".Replace('¤', '"'),
              match =>
              {
                  var cls = "number";
                  if (Regex.IsMatch(match.Value, @"^¤".Replace('¤', '"')))
                  {
                      if (Regex.IsMatch(match.Value, ":$"))
                      {
                          cls = "key";
                      }
                      else
                      {
                          cls = "string";
                      }
                  }
                  else if (Regex.IsMatch(match.Value, "true|false"))
                  {
                      cls = "boolean";
                  }
                  else if (Regex.IsMatch(match.Value, "null"))
                  {
                      cls = "null";
                  }
                  return "<span class=\"" + cls + "\">" + match + "</span>";
              });
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            //add file
            if (dlgAddFile.ShowDialog(this) == DialogResult.OK)
            {
                TreeNode nd = treeView1.SelectedNode;
                if (nd.ImageIndex == 2)
                {
                    nd = nd.Parent;
                }

                string fl = dlgAddFile.FileName;
                string fileName = System.IO.Path.GetFileNameWithoutExtension(fl);


                TreeNode ndFile = nd.Nodes.Add(fileName);
                ndFile.ImageIndex = 2;
                ndFile.SelectedImageIndex = 2;

                Lib.ProjectLayer parentLayer = nd.Tag as Lib.ProjectLayer;

                Lib.ProjectLayer projectLayer = new Lib.ProjectLayer();
                projectLayer.IsGroup = false;
                projectLayer.Path = fl;
                projectLayer.Name = fileName;
                projectLayer.ID = $"{parentLayer.ID}{nd.Nodes.Count}";
                ndFile.Tag = projectLayer;
            }
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            string outputPath = "D:\\CryptoWeb_Processed";

            generatedFiles = new List<System.IO.FileInfo>();
            outputGrid.DataSource = generatedFiles;

            btnGenerate.Enabled = false;
            timerGen.Enabled = true;
            

            if (System.IO.Directory.Exists(outputPath))
            {
                System.IO.Directory.Delete(outputPath, true);
            }
            System.IO.Directory.CreateDirectory(outputPath);

            var files = Lib.NFTCollectionItem.CreateCollection(CurrentProject);

            prg1.Minimum = 0;
            prg1.Maximum = files.Count;
            prg1.Value = 0;
            prg1.Step = 1;
            prg1.Visible = true;
            lblGenProgress.Text = $"{0}/{files.Count} ({Math.Round(0 * 1.0 / files.Count * 100, 2)}%)";

            StringBuilder sb = new StringBuilder();



            await Task.Run(() =>
             {
                 Parallel.ForEach(files, async item =>
                {
                    await item.GenerateImageAsync(outputPath);
                });
             });

            //foreach (var item in files)
            //{
            //    await item.GenerateImageAsync(outputPath);
            //    sb.AppendLine($"> DONE!\t[{item.FileName}]");

            //    //this.Invoke(new Action(() =>
            //    //{
            //    //    sb.AppendLine($"> DONE!\t[{item.FileName}]");
            //    //    prg1.Increment(1);
            //    //    output.Text = sb.ToString();
            //    //    output.SelectionStart = sb.Length;
            //    //    output.ScrollToCaret();
            //    //}));

            //}
          


        }


        public List<System.IO.FileInfo> generatedFiles;

        private void timerGen_Tick(object sender, EventArgs e)
        {
            //generate progress for 
            string outputPath = "D:\\CryptoWeb_Processed";
            //get files
            var processed = System.IO.Directory.GetFiles(outputPath, "*.png");
            prg1.Value = processed.Length;

            if (prg1.Value == 0) return;

           
            foreach (var item in processed)
            {
                if (generatedFiles.Where(a=> a.FullName == item).Count() == 0)
                {
                    generatedFiles.Add(new System.IO.FileInfo(item));
                }
            }
            outputGrid.RefreshDataSource();


            lblGenProgress.Text = $"{processed.Length}/{CurrentProject.TotalItems} ({Math.Round(processed.Length * 1.0 / CurrentProject.TotalItems * 100, 2)}%)";

            if (processed.Length == CurrentProject.TotalItems)
            {
                //GOTOVO!
                prg1.Visible = false;
                btnGenerate.Enabled = true;
                timerGen.Enabled = false;
                lblGenProgress.Text = "";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
            var view = ((DevExpress.XtraGrid.Views.Base.ColumnView)outputGrid.DefaultView);
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                view.ActiveFilterCriteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("Contains([Name], '" + txtSearch.Text + "')");
            }
            else
            {
                view.ActiveFilter.Clear();
            }


        }
 
    }
}
