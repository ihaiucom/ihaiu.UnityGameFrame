using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Games
{
	[ExecuteInEditMode]
	public class RenderLayer : MonoBehaviour 
	{
	   

		private static Dictionary<GameSortingLayer, string> layerNames = new Dictionary<GameSortingLayer, string>(); 
	    public static string GetLayerName(GameSortingLayer layer)
	    {
	        if(!layerNames.ContainsKey(layer))
	            layerNames.Add (layer, Enum.GetName(typeof(GameSortingLayer), layer));
	        return layerNames[layer];
	    }

	    public bool hasChildren = true;
		public GameSortingLayer sortingLayer = GameSortingLayer.Default;
	    public int sortingOrder = 0;
		[HideInInspector]
		[SerializeField]
		public string sortingLayerName = Enum.GetName(typeof(GameSortingLayer), GameSortingLayer.Default);

		private GameSortingLayer _sortingLayer;
	    private int _sortingOrder;

	    void Start () 
	    {
	        Set();
	    }

	    void Update () 
	    {
	        if(_sortingLayer != sortingLayer || _sortingOrder != sortingOrder)
	        {
	            Set();
	        }
	    }

	    [ContextMenu("Set")]
	    public void Set()
	    {
			sortingLayerName = Enum.GetName(typeof(GameSortingLayer), sortingLayer);
	        _sortingLayer = sortingLayer;
	        _sortingOrder = sortingOrder;

	        if(hasChildren)
	        {
	            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
	            foreach(Renderer renderer in renderers)
	            {
	                renderer.sortingLayerName = sortingLayerName;
	                renderer.sortingOrder = sortingOrder;
	            }
	        }
	        else
	        {
	            Renderer renderer = GetComponent<Renderer>();
	            if(renderer != null)
	            {
	                renderer.sortingLayerName = sortingLayerName;
	                renderer.sortingOrder = sortingOrder;
	            }
	        }
	    }

	    [ContextMenu("Log")]
	    public void Log()
	    {
	        Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
	        foreach(Renderer renderer in renderers)
	        {
	            Debug.Log(string.Format("{0}, sortingLayerID={1}, sortingLayerName={2}, sortingOrder={3}", renderer.gameObject, renderer.sortingLayerID, renderer.sortingLayerName, renderer.sortingOrder));
	        }
	    }
	}
}