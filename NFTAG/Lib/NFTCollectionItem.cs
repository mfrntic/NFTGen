using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NFTAG.Lib
{
    public class NFTCollectionItem
    {
        public NFTCollectionItem()
        {
            Traits = new Dictionary<string, ProjectLayer>();
        }

        public int TokenID { get; set; }
        //public Project Project { get; set; }
        public string FileName
        {
            get
            {
                string fn = $"{TokenID.ToString("0000")}_";
                foreach (KeyValuePair<string, ProjectLayer> item in Traits)
                {
                    fn += $"{item.Value.ID}_";
                }
                return fn.TrimEnd('_');
            }
        }

        public string HashName
        {
            get
            {
                return FileName.HashString();
            }
        }

        public DateTime GeneratedTimestamp { get; private set; }

        public string LocalPath { get; set; }

        public Dictionary<string, ProjectLayer> Traits { get; set; }


        public static List<NFTCollectionItem> CreateCollection(Project proj)
        {
            List<NFTCollectionItem> files = new List<NFTCollectionItem>();

            //first layer is base layer
            ProjectLayer baseLayer = proj.Overlays.Where(a => a.IsGroup && a.Overlays.Count > 0).FirstOrDefault();

            if (baseLayer == null)
                return files;

            int id = 0;

            //create base collection without traits
            foreach (var layer in baseLayer.Overlays)
            {
                if (!layer.IsGroup) //ako je overlay
                {
                    for (int i = 0; i < layer.Rarity; i++)
                    {
                        id++;
                        NFTCollectionItem item = new NFTCollectionItem() { TokenID = id };
                        files.Add(item);
                    }
                }
            }

            //traverse all trait groups
            foreach (var group in proj.Overlays.Where(a => a.IsGroup))
            {

                //fill overlays for group
                foreach (var layer in group.Overlays.Where(a => !a.IsGroup))
                {
                    Random rng = new Random();
                    if (proj.Settings.PredictableShuffle)
                    {
                        rng = new Random(proj.Settings.ShuffleSeed);
                    }

                    var temp_fls = files
                        .Where(x => !x.Traits.ContainsKey(group.ID))
                        .OrderBy(a => rng.Next()).ToList(); //shuffle

                    for (int j = 0; j < layer.Rarity; j++)
                    {
                        temp_fls[j].Traits.Add(group.ID, layer);
                    }
                }
            }

            return files;
        }


        public void GenerateImage(Project proj)
        {
            ImageMagick.MagickImageCollection images = new ImageMagick.MagickImageCollection();
            foreach (var trait in this.Traits)
            {
                images.Add(trait.Value.LocalPath);
            }

            using (var res = images.Merge())
            {
                res.VirtualPixelMethod = ImageMagick.VirtualPixelMethod.Transparent;
                res.FilterType = proj.Settings.GetMagickResizeAlgorithm();
                res.Resize(proj.Settings.OutputSize.Width, proj.Settings.OutputSize.Height);

                //path to generated image
                this.LocalPath = System.IO.Path.Combine(proj.Settings.GetOutputPath(proj), this.FileName + ".png");
                this.GeneratedTimestamp = DateTime.Now;

                res.Write(this.LocalPath);
            }

        }

        public async Task GenerateImageAsync(Project proj, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                try
                {
                    ImageMagick.MagickImageCollection images = new ImageMagick.MagickImageCollection();
                    foreach (var trait in this.Traits)
                    {
                        images.Add(trait.Value.LocalPath);
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    using (var res = images.Merge())
                    {
                        res.VirtualPixelMethod = ImageMagick.VirtualPixelMethod.Transparent;
                        res.FilterType = proj.Settings.GetMagickResizeAlgorithm();
                        res.Resize(proj.Settings.OutputSize.Width, proj.Settings.OutputSize.Height);

                        //path to generated image
                        this.LocalPath = System.IO.Path.Combine(proj.Settings.GetOutputPath(proj), this.FileName + ".png");
                        this.GeneratedTimestamp = DateTime.Now;

                        res.Write(this.LocalPath);
                    }

                }
                catch
                {

                }
            });
        }

        //public async static Task GenerateImages(List<NFTCollectionItem> items, string outputPath)
        //{
        //    if (System.IO.Directory.Exists(outputPath))
        //    {
        //        System.IO.Directory.Delete(outputPath, true);
        //    }
        //    System.IO.Directory.CreateDirectory(outputPath);

        //    await Task.Run(() =>
        //    {
        //        foreach (var item in items)
        //        {
        //            ImageMagick.MagickImageCollection images = new ImageMagick.MagickImageCollection();
        //            foreach (var trait in item.Traits)
        //            {
        //                images.Add(trait.Value.Path);
        //            }

        //            using (var res = images.Merge())
        //            {
        //                res.Resize(500, 500);
        //                res.Write(System.IO.Path.Combine(outputPath, item.FileName + ".png"));
        //            }
        //        }
        //    });

        //}

        //private void genFileName(ref string fn, ProjectLayer pl = null)
        //{
        //    if (pl == null)
        //    {
        //        fn = $"{TokenID.ToString("0000")}_";
        //    }

        //    var overlays = pl == null ? Project.Overlays : pl.Overlays;

        //    foreach (ProjectLayer layer in overlays)
        //    {
        //        if (!layer.IsGroup)
        //        {
        //            fn += layer.ID + "_";
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
    }
}
