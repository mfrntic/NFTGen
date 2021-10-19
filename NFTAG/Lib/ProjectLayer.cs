using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTAG.Lib
{
    public class ProjectLayer
    {
        public ProjectLayer()
        {
            overlays = new List<ProjectLayer>();
        }

        public string Name { get; set; }
        public int Rarity { get; set; }

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
