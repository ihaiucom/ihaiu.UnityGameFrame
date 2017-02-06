using UnityEngine;
using System.IO;

namespace Games
{
	public abstract class AbstractSetting<T> 
	{
		virtual public void Set()
		{
		}

		virtual public void Save(string path)
		{
			string json = JsonUtility.ToJson(this);
			File.WriteAllText(path, json);
		}

		virtual public T Load(string path)
		{
			if(!File.Exists(path))
			{
				return default(T);
			}
			string json = File.ReadAllText(path);
			return JsonUtility.FromJson<T>(json);
		}
	}
}