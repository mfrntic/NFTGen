using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTGenerator.Lib
{
    public class NFTCollectionProject
    {
        public NFTCollectionProject()
        {
            Tokens = new List<NFTCollectionItem>();
        }

        public NFTCollectionProject(string projectName) : this()
        {
            this.ProjectName = projectName;
        }

        public string ProjectName { get; set; }

        public int TotalItems
        {
            get
            {
                return Tokens.Count();
            }
        }

        private string provenanceHash = "";
        public string ProvenanceHash
        {
            get
            {
                if (string.IsNullOrEmpty(provenanceHash))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var token in Tokens)
                    {
                        sb.Append(token.Hash);
                    }

                    var sha = new System.Security.Cryptography.SHA256Managed();
                    byte[] checksum = sha.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
                    provenanceHash = BitConverter.ToString(checksum).Replace("-", String.Empty);

                }
                return provenanceHash;
            }
        }

        public List<NFTCollectionItem> Tokens { get; set; }

        public string ToJSON()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            return json;
        }

        public static NFTCollectionProject FromJSON(string json)
        {
            NFTCollectionProject proj = Newtonsoft.Json.JsonConvert.DeserializeObject<NFTCollectionProject>(json);
            return proj;
        }
    }
}
