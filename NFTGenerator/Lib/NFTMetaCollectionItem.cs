﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace NFTGenerator.Lib
{
    public class NFTMetaCollectionItem
    {
        [JsonIgnore]
        public int tokenId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public List<Trait> attributes { get; set; }
    }
}