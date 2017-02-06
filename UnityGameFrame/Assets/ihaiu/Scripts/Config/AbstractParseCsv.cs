using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.ihaiu
{
    public abstract class AbstractParseCsv : IParseCsv
    {
        public Dictionary<string, int>      headKeyEns      = new Dictionary<string, int>();
        public Dictionary<int, int>         headPropIds     = new Dictionary<int, int>();

        virtual public void ParseHeadKeyCN(string[] csv)
        {
        }

        virtual public void ParseHeadKeyEN(string[] csv)
        {
            string key;
            for(int i = 0; i < csv.Length; i ++)
            {
                key = csv[i];
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.Trim();
                    headKeyEns.Add(key, i);
                }
            }
        }

        virtual public void ParseHeadPropId(string[] csv)
        {
            for(int i = 0; i < csv.Length; i ++)
            {
                if (string.IsNullOrEmpty(csv[i]))
                    continue;

                headPropIds.Add(i, csv.GetInt32(i));
            }
        }

        virtual public void ParseCsv(string[] csv)
        {
        }

        virtual public int GetHeadIndex(string enName)
        {
            if (headKeyEns.ContainsKey(enName))
            {
                return headKeyEns[enName];
            }

			Debug.LogErrorFormat("{0}: headKeyEns[{1}] = -1", this, enName);
            return -1;
        }

        virtual public int GetHeadPropId(int index)
        {
            if (headPropIds.ContainsKey(index))
            {
                return headPropIds[index];
            }
            return -1;
        }
    }
}