using UnityEngine;
using System.Collections;
using System;

namespace com.ihaiu
{
    public class ConfigCsvAttribute : Attribute
    {
        public string       assetName;
        public bool         hasHeadPropId;
		public ConfigCsvAttribute(string path, bool hasHeadPropId)
        {
            this.assetName = path;
            this.hasHeadPropId = hasHeadPropId;
        }

    }
}