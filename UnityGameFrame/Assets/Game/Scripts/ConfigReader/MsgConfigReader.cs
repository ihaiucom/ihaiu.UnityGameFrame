using UnityEngine;
using System.Collections;
using com.ihaiu;

namespace Games
{
	[Sort(-1)]
	[ConfigCsv("Config/msg", false)]
	public class MsgConfigReader : ConfigReader<MsgConfig>
	{
		public override void ParseCsv (string[] csv)
		{
			MsgConfig config = new MsgConfig();
			config.id	= csv.GetInt32(GetHeadIndex("id"));
			config.type	= csv.GetInt32(GetHeadIndex("type"));
			config.msg	= csv.GetString(GetHeadIndex("msg"));

			configs.Add(config.id, config);
		}
	}
}
