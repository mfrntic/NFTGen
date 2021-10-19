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

    }


}
