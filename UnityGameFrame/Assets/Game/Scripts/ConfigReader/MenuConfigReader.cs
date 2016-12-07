using UnityEngine;
using System.Collections;
using com.ihaiu;

namespace Games
{
	[ConfigCsv("Config/menu", false)]
	public class MenuConfigReader : ConfigReader<MenuConfig>
	{
		public override void ParseCsv (string[] csv)
		{
			MenuConfig config = new MenuConfig();
			config.id	= csv.GetInt32(GetHeadIndex("id"));
			config.name	= csv.GetString(GetHeadIndex("name"));

			configs.Add(config.id, config);
		}
	}
}
