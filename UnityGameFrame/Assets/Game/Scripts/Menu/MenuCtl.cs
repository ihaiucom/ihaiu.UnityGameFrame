using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Games
{
    public abstract class MenuCtl 
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

        public StateType            state           = StateType.Closed;
        public float                cacheTime       = 0;

		virtual public List<string> GetLoadAssets()
        {
            if (module != null)
            {
                return module.GetLoadAssets();
            }

            return null;
        }

		virtual public void Destory()
        {
           
        }

		virtual public void OnDestory()
        {
        }


		virtual public void Open(bool isPreinstall = false)
        {
        }

		virtual public void Close()
        {
        }



		private List<string>    _preloadAssets;
		private bool            _preloadComplete    = false;
		private int             _preloadCount       = 0;
		private int             _preloadNum         = 0;
		public void Load()
		{
			state = StateType.Loading;
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
				OnLoadAssetsComplete();
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
					OnLoadAssetsComplete();
				}
			}
		}

		virtual protected void OnLoadAssetsComplete()
		{
			
		}


		protected void CloseOther()
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
