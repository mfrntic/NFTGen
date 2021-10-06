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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            statusInfo.Text = "Select main folder which contains folders with trait images...";
            //browse folders
            if (folderBrowse.ShowDialog(this) == DialogResult.OK)
            {
                statusInfo.Text = "Loading trait folders...";

                string fld = folderBrowse.SelectedPath;
                //učitaj sve foldere
                string[] dirs = System.IO.Directory.GetDirectories(fld);
                foreach (var dir in dirs)
                {
                    var nd = treeView1.Nodes.Add(System.IO.Path.GetFileName(dir));
                    nd.Tag = dir;
                }

            }
            statusInfo.Text = "Ready";

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            btnRemoveFolder.Enabled = e.Node != null;
            btnMakeRoot.Enabled = e.Node != null;

        }

        private void NormalizeTreeview()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            //flatten treeview folder structure (remove root)
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    foreach (TreeNode child in item.Nodes)
                    {
                        nodes.Add(child);
                    }
                    item.Nodes.Clear();
                    nodes.Insert(0, item);
                }
            }
            if (nodes.Count > 0)
            {
                treeView1.Nodes.Clear();
                treeView1.Nodes.AddRange(nodes.ToArray());
            }
        }

        private void btnMakeRoot_Click(object sender, EventArgs e)
        {

            var node = treeView1.SelectedNode;
            if (node != null)
            {
                NormalizeTreeview();

                var nodes = new List<TreeNode>();
                foreach (TreeNode item in treeView1.Nodes)
                {
                    if (item != node)
                    {
                        nodes.Add(item);
                    }

                }



                foreach (TreeNode item in nodes)
                {
                    treeView1.Nodes.Remove(item);
                    node.Nodes.Add(item);
                }
                treeView1.ExpandAll();
            }
        }

        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }
    }
}
