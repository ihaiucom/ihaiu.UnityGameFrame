using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games
{
    public class LoadManager 
    {
        private Dictionary<int, LoadCtl> dict = new Dictionary<int, LoadCtl>();


        private LoadCtl GetLoadCtl(int loadId)
        {
            if (dict.ContainsKey(loadId))
            {
                return dict[loadId];
            }

            return null;
        }

        public void Open(int loadId)
        {
            LoadCtl loadCtl = GetLoadCtl(loadId);
            if (loadCtl == null)
            {
                LoadConfig loadConfig = Game.config.load.GetConfig(loadId);
                if (loadConfig == null)
                {
                    Debug.LogErrorFormat("LoadManager Open loadConfig=null loadId={0}", loadId);
                    return;
                }

                loadCtl = new LoadCtl();
                loadCtl.loadId = loadId;
                loadCtl.loadConfig = loadConfig;
                dict.Add(loadCtl.loadId, loadCtl);
            }

            loadCtl.Open();
        }


        public void Close(int loadId)
        {
            LoadCtl loadCtl = GetLoadCtl(loadId);
            if (loadCtl == null)
            {
                loadCtl.Close();
            }
        }

        public void CloseAll()
        {
            foreach(var kvp in dict)
            {
                if (kvp.Value.isOpen)
                {
                    kvp.Value.Close();
                }
            }
        }
    }
}