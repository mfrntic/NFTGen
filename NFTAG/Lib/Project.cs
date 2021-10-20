using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTAG.Lib
{
    public class Project
    {
        public Project()
        {
            overlays = new List<ProjectLayer>();
        }

        public string ProjectName { get; set; }
        //public string ProjectDecription { get; set; }
        public string BaseFolder { get; set; }
        /// <summary>
        /// Total expected number of items in the collection
        /// </summary>
        public int TotalItems { get; set; }

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
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
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
