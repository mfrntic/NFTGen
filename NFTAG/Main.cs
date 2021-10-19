using DevExpress.XtraBars.Ribbon;
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
            statusInfo.Text = "Select main folder which contains folders with trait images...";
            //browse folders
            if (folderBrowse.ShowDialog(this) == DialogResult.OK)
            {
                statusInfo.Text = "Loading trait folders...";
                addNode(folderBrowse.SelectedPath);

            }
            statusInfo.Text = "Ready";
        }

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

        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {

        }

        private void gallery1_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            TreeNode tn = e.Item.Tag as TreeNode;
            treeView1.SelectedNode = tn;
 
        }
    }
}
