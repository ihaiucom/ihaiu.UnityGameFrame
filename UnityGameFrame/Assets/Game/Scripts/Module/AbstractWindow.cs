using UnityEngine;
using System.Collections;


namespace Games
{
    public class AbstractWindow : MonoBehaviour
    {
        public AbstractModule   module;
        public MenuCtl          menuCtl;

        private bool            _isStart;
        private bool            _visiable;
        private RectTransform   _rectTransform;

        public bool visiable
        {
            get
            {
                return _visiable;
            }
        }


        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = gameObject.GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        virtual protected void  Start ()
        {
            _isStart = true;
            OnInit();

            _visiable = true;
            OnShow();
    	}
    	

        virtual protected void OnInit()
        {
            
        }


        virtual public void Show()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            if (_isStart)
            {
                _visiable = true;
                OnShow();
            }
        }


        virtual protected void OnShow()
        {
        }

        virtual public void Hide()
        {
            _visiable = false;
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                OnHide();
            }
        }

        virtual protected void OnHide()
        {
        }

        virtual public void Destory()
        {
            Hide();
            GameObject.Destroy(gameObject);
        }

        virtual protected void OnDestory()
        {
            if (menuCtl != null)
            {
                menuCtl.OnDestory();
            }
            module = null;
        }






    }
}
