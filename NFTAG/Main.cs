using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFTAG
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region ADD FOLDER TO TREEVIEW
        private void addNode(string fld, TreeNode parentNode = null)
        {
            string[] dirs = System.IO.Directory.GetDirectories(fld);
            if (dirs.Length > 0)
            {
                foreach (var dir in dirs)
                {
                    TreeNode nd;
                    string folderName = System.IO.Path.GetFileName(dir);
                    if (parentNode == null)
                    {
                        nd = treeView1.Nodes.Add(folderName);
                    }
                    else
                    {
                        nd = parentNode.Nodes.Add(folderName);
                    }
                    nd.Tag = dir;
                    nd.ImageIndex = 0;
                    nd.SelectedImageIndex = 1;

                    //recursive
                    addNode(dir, nd);

                    //add files
                    string[] fls = System.IO.Directory.GetFiles(dir, "*.png");
                    foreach (var fl in fls)
                    {
                        string fileName = System.IO.Path.GetFileNameWithoutExtension(fl);
                        TreeNode ndFile = nd.Nodes.Add(fileName);
                        ndFile.ImageIndex = 2;
                        ndFile.SelectedImageIndex = 2;
                        ndFile.Tag = fl;
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
                addNode(folderBrowse.SelectedPath);

                btnReloadRarityTable_Click(null, null);

            }
            statusInfo.Text = "Ready";
        }

        #endregion

        //remove selected item
        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        //Odabir foldera u treeview kontroli

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            btnUp.Enabled = btnDown.Enabled = btnRemoveFolder.Enabled = e.Node != null;

            gallery1.Gallery.Groups.Clear();

            if (e.Node.ImageIndex < 2) //ako je folder
            {
                picPrev.Visible = false;
                gallery1.Visible = true;
                //load images
                GalleryItemGroup group = new GalleryItemGroup();
                group.Caption = e.Node.Text;

                gallery1.Gallery.Groups.Add(group);

                foreach (TreeNode tn in e.Node.Nodes)
                {
                    if (tn.ImageIndex == 2)
                    {
                        string fl = tn.Tag.ToString();

                        var gi = new GalleryItem(Image.FromFile(fl).GetThumbnailImage(100, 100, null, new IntPtr()), tn.Text, "");
                        gi.Tag = tn;
                        group.Items.Add(gi);
                    }
                }
            }
            else
            {
                picPrev.Visible = true;
                gallery1.Visible = false;
                string fl = e.Node.Tag.ToString();
                picPrev.Image = Image.FromFile(fl);

            }
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            //move item up the tree
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //move item down the tree
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
        private void LoadRarityTableFromFolders(TreeNode tn = null, TreeListNode parent = null)
        {
            var nodes = (tn == null ? treeView1.Nodes : tn.Nodes);
            
            foreach (TreeNode node in nodes)
            {
                TreeListNode tln = tlRT.AppendNode(new object[] { node.Text }, parent);
                if (node.Nodes.Count > 0)
                {
                    LoadRarityTableFromFolders(node, tln);
                }
            }
        }


        private void btnReloadRarityTable_Click(object sender, EventArgs e)
        {
            //Osvježi rarity table
            tlRT.BeginUnboundLoad();
            tlRT.Nodes.Clear();
            LoadRarityTableFromFolders();
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

    }
}
