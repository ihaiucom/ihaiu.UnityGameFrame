using UnityEngine;
using System.Collections;

namespace Games
{
    public class LoadPanel : MonoBehaviour
    {
        private RectTransform   _rectTransform;

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

        virtual public void Set(float progress, string state)
        {
            
        }

        virtual public void Set(float progress)
        {
            
        }
       
    }
}
