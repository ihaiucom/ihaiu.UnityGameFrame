using UnityEngine;
using System.Collections;
using com.ihaiu;

namespace Games
{
	[ConfigCsv("Config/load", false)]
    public class LoadConfigReader : ConfigReader<LoadConfig>
	{
		public override void ParseCsv (string[] csv)
		{
            LoadConfig config = new LoadConfig();
			config.id	= csv.GetInt32(GetHeadIndex("id"));
            config.name	= csv.GetString(GetHeadIndex("name"));
            config.path = csv.GetString(GetHeadIndex("path"));

			configs.Add(config.id, config);
		}
	}
}
