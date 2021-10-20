using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string ID { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string Path { get; set; }
        public bool IsGroup { get; set; }

        private List<ProjectLayer> overlays = null;
        [Browsable(false)]
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
