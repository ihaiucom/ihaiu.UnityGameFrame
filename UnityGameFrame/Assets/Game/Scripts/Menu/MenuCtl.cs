using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Games
{
    public class MenuCtl 
    {
        public enum StateType
        {
            Opened,
            Closed,
            Loading
        }

        public int                  menuId;
        public MenuConfig           config;
        public AbstractModule       module;
        public AbstractWindow       window;

        public StateType            state           = StateType.Closed;
        public float                cacheTime       = 0;
        private bool                isPreinstall    = false;

        public string windowPath
        {
            get
            {
                return config.path;
            }
        }

        public List<string> GetLoadAssets()
        {
            if (module != null)
            {
                return module.GetLoadAssets();
            }

            return null;
        }

        public void Destory()
        {
            if (window != null)
            {
                window.Destory();
            }
        }

        public void OnDestory()
        {
            window = null;
        }


        public void Open(bool isPreinstall = false)
        {
            this.isPreinstall = isPreinstall;
            if (window == null)
            {
                state = StateType.Loading;
                Load();
            }
            else
            {
                if (!isPreinstall)
                {
                    SetWindowShow();
                }
            }
            cacheTime = 0;
        }

        public void Close()
        {
            if (window != null)
            {
                window.Hide();
            }

            state       = StateType.Closed;
            cacheTime   = 0;
        }



        private List<string>    _preloadAssets;
        private bool            _preloadComplete    = false;
        private int             _preloadCount       = 0;
        private int             _preloadNum         = 0;
        public void Load()
        {
            _preloadAssets = GetLoadAssets();
            if (_preloadAssets != null)
            {
                _preloadCount       = _preloadAssets.Count;
                _preloadNum         = 0;
                _preloadComplete    = false;
                LoadAssets();
            }
            else
            {
                LoadWindow();
            }
        }

        // load pre assets
        private void LoadAssets()
        {
            for(int i = 0; i < _preloadCount; i ++)
            {
                Game.asset.Load(_preloadAssets[i], OnLoadAsset);
            }
        }

        private void OnLoadAsset(string filename, object obj)
        {
            _preloadNum++;
            if(_preloadNum >= _preloadCount)
            {
                if (!_preloadComplete)
                {
                    _preloadComplete = true;
                    LoadWindow();
                }
            }
        }

        // load window
        private void LoadWindow()
        {
            Game.asset.Load(windowPath, OnLoadWindow);
        }


        private void OnLoadWindow(string filename, object obj)
        {
            if (obj == null)
            {
                Debug.LogErrorFormat("MenuItem OnLoadWindow obj=null, menuId={0}, menuName={1}, filename={2}", menuId, config.name, filename);
                return;
            }

            GameObject go = GameObject.Instantiate((GameObject)obj);
            window = go.GetComponent<AbstractWindow>();

            if (window != null)
            {
                window.module   = module;
                window.menuCtl  = this;

                if (isPreinstall)
                {
                    window.rectTransform.SetParent(Game.uiLayer.GetLayer(UILayer.Layer.Layer_PreInstance), false);
                    go.SetActive(false);
                }
                else
                {
                    if (state != StateType.Closed)
                    {
                        SetWindowShow();
                    }
                }
            }
        }

        private void SetWindowShow()
        {
            window.rectTransform.SetParent(Game.uiLayer.GetLayer(config.layer), false);
            window.rectTransform.localScale = Vector3.one;

            switch(config.layout)
            {
                case MenuLayout.ScreenSize:
                    window.rectTransform.anchoredPosition = Vector2.zero;
                    break;

                case MenuLayout.PositionZero:
                    window.rectTransform.offsetMin = Vector2.zero;
                    window.rectTransform.offsetMax = Vector2.zero;
                    break;
            }


            window.Show();
            window.rectTransform.SetAsLastSibling();

            CloseOther();
            state = StateType.Opened;
        }

        private void CloseOther()
        {
            if (config.closeOtherType == MenuCloseOtherType.None)
                return;
            
            List<MenuCtl> list = Game.menu.GetMenuCtlList();;

            switch (config.closeOtherType)
            {
                case MenuCloseOtherType.ExceptSelf_All:
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] == this)
                            continue;

                        list[i].Close();
                    }
                    break;


                case MenuCloseOtherType.ExceptSelf_Module:
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] == this || list[i].config.layer != UILayer.Layer.Layer_Module)
                            continue;

                        list[i].Close();
                    }
                    break;


                case MenuCloseOtherType.ExceptSelf_SameLayer:
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] == this || list[i].config.layer == config.layer)
                            continue;

                        list[i].Close();
                    }
                    break;
            }
        }






    }
}
