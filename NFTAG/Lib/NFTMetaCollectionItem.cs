using System.Collections.Generic;

namespace NFTGen.Lib
{
    public class NFTMetaCollectionItem
    {
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public int tokenId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public List<Trait> attributes { get; set; }
    }
}