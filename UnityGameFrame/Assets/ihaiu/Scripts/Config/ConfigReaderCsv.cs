using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

namespace com.ihaiu
{
    public class ConfigReader<T> : AbstractParseCsv, IConfigReader
    {
        public Dictionary<int, T> configs = new Dictionary<int, T>();
        private ConfigCsvAttribute arrtr;

        virtual public void Load()
        {
			Type t = this.GetType();
            arrtr = t.GetCustomAttributes(typeof(ConfigCsvAttribute), false)[0] as ConfigCsvAttribute;
            ConfigSetting.Load(arrtr.assetName, ParseAsset);
        }

        virtual public void ParseAsset(string path, object obj)
        {
			if(obj == null)
			{
				Debug.LogErrorFormat("{0}: obj={1}, path={2}", this, obj, path);
				return;
			}
            string txt = (string) obj;
            StringReader sr = new StringReader(txt);

            string      line;
            string[]    csv;
            line = sr.ReadLine();
            csv = line.Split(';');
            ParseHeadKeyCN(csv);

            line = sr.ReadLine();
            csv = line.Split(';');
            ParseHeadKeyEN(csv);

            if (arrtr.hasHeadPropId)
            {
                line = sr.ReadLine();
                csv = line.Split(';');
                ParseHeadPropId(csv);
            }

            while (true)
            {
                line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }

                csv = line.Split(';');
                if (csv.Length != 0 && !string.IsNullOrEmpty(csv[0]))
                {
                    ParseCsv(csv);
                }
            }
        }

        virtual public void Reload()
        {
            configs.Clear();
            Load();
        }

		virtual public void OnGameConfigLoaded()
		{
		}

        public T GetConfig(int id)
        {
            if (configs.ContainsKey(id))
            {
                return configs[id];
            }

            return default(T);
        }

    }
}
