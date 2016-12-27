using UnityEngine;
using System.Collections;

namespace Games
{
    public class LoadCtl 
    {
        public int          loadId;
        public LoadConfig   loadConfig;
        public LoadPanel    loadPanel;
        public bool         isOpen = false;




        public void Open()
        {
            isOpen = true;
            if (!string.IsNullOrEmpty(loadConfig.path))
            {
                if (loadPanel == null)
                {
                    LoadPanelAsset();
                }
                else
                {
                    SetLoadPanelShow();
                }
            }
            else if(loadId == LoadId.Circle)
            {
                Game.cricle.Show();
            }
        }

        public void Close()
        {
            isOpen = false;
            if (loadPanel != null)
            {
                loadPanel.gameObject.SetActive(false);
            }

            if(loadId == LoadId.Circle)
            {
                Game.cricle.Hide();
            }
        }

        void LoadPanelAsset()
        {
            if (loadConfig.isShowCircle)
                Game.cricle.Show();
            
            Game.asset.Load(loadConfig.path, OnLoadPanelAsset);
        }

        void OnLoadPanelAsset(string filename, object obj)
        {
            GameObject go = GameObject.Instantiate((GameObject)obj);
            loadPanel = go.GetComponent<LoadPanel>();
            SetLoadPanelShow();
        }

        private void SetLoadPanelShow()
        {
            if (loadConfig.isShowCircle)
                Game.cricle.Hide();
            
            loadPanel.rectTransform.SetParent(Game.uiLayer.loader, false);
            loadPanel.rectTransform.localScale = Vector3.one;
            loadPanel.rectTransform.anchoredPosition = Vector2.zero;
            loadPanel.rectTransform.SetAsLastSibling();
            loadPanel.gameObject.SetActive(true);
        }


    }
}