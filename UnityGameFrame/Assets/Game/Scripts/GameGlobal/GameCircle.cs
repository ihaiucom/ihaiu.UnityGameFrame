using UnityEngine;
using System.Collections;

public class GameCircle 
{
    private GameObject      go;


    private bool            _visiable = false;
    private bool Visiable
    {
        get
        {
            return _visiable;
        }

        set
        {
            _visiable = value;
            if (go != null)
            {
                go.SetActive(value);
            }
        }
    }

    private bool isInit = false;
    private void Init()
    {
        if (isInit)
            return;
        isInit = true;
        Game.asset.Load("Modules/GameCirclePanel", OnLoad);
    }

    void OnLoad(string filename, object obj)
    {
		Debug.Log(filename + "  obj=" + obj);
        go = GameObject.Instantiate((GameObject) obj);
        go.transform.SetParent(Game.uiLayer.loader);
		RectTransform rectTransform = (RectTransform) go.transform;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
        rectTransform.localScale = Vector3.one;
		rectTransform.anchoredPosition3D = Vector3.zero;

        go.SetActive(Visiable);
    }

    public void Show()
    {
        Init();

        Visiable = true;
    }

    public void Hide()
    {
        Visiable = false;
    }
}
