using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTGen.Lib
{
    public class Project
    {
        public Project()
        {
            ProjectName = "New Project";
            Settings = new ProjectSettings();
            overlays = new List<ProjectLayer>();
        }

        public string ProjectName { get; set; }
        //public string ProjectDecription { get; set; }

        /// <summary>
        /// Total expected number of items in the collection
        /// </summary>
        public int TotalItems { get; set; }
        public string LastGeneratedJSON {get;set;}
        public ProjectSettings Settings
        {
            get; set;
        }


        private List<ProjectLayer> overlays = null;
        public List<ProjectLayer> Overlays
        {
            get
            {
                return overlays;
            }
            set
            {
                overlays = value;
            }
        }


        public string ToJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static Project FromJSON(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Lib.Project>(json);
        }

        public static Project Load(string fileName)
        {
            string json = System.IO.File.ReadAllText(fileName);
            return Project.FromJSON(json);
        }
    }


}
