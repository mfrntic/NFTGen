using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTAG.Lib
{
    public class ProjectSettings : ICloneable
    {
        public ProjectSettings()
        {
            OutputDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NFTGen_Processed");
            OutputSize = new Size(500, 500);
            ShuffleSeed = 0; //if 0 then uses timer!
            ResizeAlgorithm = ImageMagick.FilterType.Point; //Point is nearest neighbor
        }

        [Description("Path to output directory where images will be generated"), DisplayName("Output Folder"), Category("Output")]
        [EditorAttribute(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string OutputDirectory { get; set; }
        [Description("Specifies whether project folder will be created in output directory"), DisplayName("Folderize Project"), Category("Output")]
        public bool CreateProjectFolderInOutputDirectory { get; set; }

        public string GetOutputPath(Project proj)
        {
            var pth = System.IO.Path.Combine(proj.Settings.OutputDirectory);
            if (proj.Settings.CreateProjectFolderInOutputDirectory)
            {
                pth = System.IO.Path.Combine(proj.Settings.OutputDirectory, proj.ProjectName);
            }
            return pth;
        }

        [Description("Size of output images in pixels"), DisplayName("Image Size"), Category("Output")]
        public Size OutputSize { get; set; }
        [Description("Determines seed for shuffling new image collection. Can be used for predictible shuffle. If value is >0 then shuffle order will be same every time. If value is 0 (default) then timer value is used (every time order is different and unpredictable)."), DisplayName("Shuffle Seed"), Category("Output")]
        public int ShuffleSeed { get; set; }
        [Description("Resize algorithm applied when resizing images to output size. Default is Point which is Nearest Neighbor. More can be found here https://legacy.imagemagick.org/Usage/filter/"), DisplayName("Image Size"), Category("Output")]
        public ImageMagick.FilterType ResizeAlgorithm { get; set; }
        //public string InitialFolder { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
