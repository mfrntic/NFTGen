using NFTGenerator.Lib;
using System.Windows.Forms;

namespace NFTGenerator
{
    public partial class ProjectForm : Form
    {
        public ProjectForm()
        {
            InitializeComponent();
        }

        private Project project;
        public Project Project
        {
            get
            {
                if (project != null)
                {
                    project.ProjectName = txtProjectName.Text;
                    project.TotalItems = (int)nudTotalItems.Value;
                    project.Settings = (ProjectSettings)pgSettings.SelectedObject;
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
                    pgSettings.SelectedObject = new ProjectSettings();
                }
            }
        }
    }

}
