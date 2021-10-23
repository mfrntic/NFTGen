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
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();
        }

        private Lib.Project project;
        public Lib.Project Project
        {
            get
            {
                if (project != null)
                {
                    project.ProjectName = txtProjectName.Text;
                    project.TotalItems = (int)nudTotalItems.Value;
                    project.Settings = (Lib.ProjectSettings)pgSettings.SelectedObject;
                }
                return project;
            }
            set
            {
                project = value;
                if (project != null)
                {
                    txtProjectName.Text = project.ProjectName;
                    nudTotalItems.Value = project.TotalItems;
                    pgSettings.SelectedObject = project.Settings;
                }
                else
                {
                    txtProjectName.Text = "";
                    nudTotalItems.Value = 0;
                    pgSettings.SelectedObject = new Lib.ProjectSettings();
                }
            }
        }
    }

}
