using UnityEngine;
using System.Collections;

namespace Games
{
    public class DontDestroy : MonoBehaviour
    {
    	void Start ()
        {
    		GameObject.DontDestroyOnLoad(gameObject);
    	}

    }
}
