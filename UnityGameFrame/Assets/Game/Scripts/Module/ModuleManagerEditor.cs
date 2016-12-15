#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System;
using com.ihaiu;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Games
{
	public class ModuleManagerEditor 
	{

		public static void Generate()
		{
			Dictionary<int, string> menuFieldNames = new Dictionary<int, string>();
			Type menuIdType = typeof(MenuId);


			FieldInfo[] fields = menuIdType.GetFields(BindingFlags.Static | BindingFlags.Public);
			for(int i = 0; i < fields.Length; i ++)
			{
				FieldInfo fieldInfo = fields[i];
				if(menuFieldNames.ContainsKey((int)fieldInfo.GetRawConstantValue()))
				{
					Debug.LogErrorFormat("class MenuId 存在多个值为 {0} 的常量,  Menu.{1}={0}", fieldInfo.GetRawConstantValue(), fieldInfo.Name);
					continue;
				}
				menuFieldNames.Add((int)fieldInfo.GetRawConstantValue(), fieldInfo.Name);
			}


			List<Type> list = new List<Type>();

			Type 			tie 		= typeof(AbstractModule);
			List<Type> 		ignore 		= new List<Type>{
				typeof(AbstractModule)
			};


			Assembly[] ass = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (var oas in ass) 
			{
				Type[] t = oas.GetTypes();
				foreach (var typ in t)
				{
					if (tie.IsAssignableFrom(typ))
					{
						if(ignore.Contains(typ)) continue;
						list.Add(typ);
					}
				}
			}

			list.Sort(SortHanlde);



			//generate s file
			var nsc = list.Select(t => t.Namespace).Distinct();


			using(StreamWriter sw = new StreamWriter(Application.dataPath + "/Game/Scripts/Module/ModuleManager_List.cs",false))
			{
				sw.WriteLine("using System;");
				sw.WriteLine("using System.Collections;");
				sw.WriteLine("using System.Collections.Generic;");
				foreach (var ns in nsc)
				{
					sw.WriteLine("using " + ns + ";");


					sw.WriteLine("namespace Games\n{");

					sw.WriteLine("\tpublic partial class ModuleManager\n\t{");

					sw.WriteLine("");


					StringWriter _l = new StringWriter();
					foreach(Type type in list)
					{
						string className = type.Name;
						string filedName = className.Replace("Module", "").FirstLower();

						int menuId = -1;
						ModuleAttribute[] moduleAttributes = type.GetCustomAttributes(typeof(ModuleAttribute), false) as ModuleAttribute[];
						if(moduleAttributes == null || moduleAttributes.Length == 0)
						{
							Debug.LogFormat("{0}  没有添加 [ModuleAttribute]",className);
						}
						else
						{
							menuId = moduleAttributes[0].menuId;
						}

						sw.WriteLine("\t\tpublic {0}	{1};", className, filedName);
						_l.WriteLine("\t\t\t{0} = new {1} ();", filedName, className);
						if(menuId > 0) _l.WriteLine("\t\t\t{0}.menuId = MenuId.{1};", filedName, menuFieldNames[menuId]);
						_l.WriteLine("\t\t\tlist.Add(" + filedName + ");");
						_l.WriteLine("\t\t\tdict.Add("+ filedName +".menuId, " + filedName + ");");
						_l.WriteLine("");
						_l.WriteLine("");
					}


					sw.WriteLine("");
					sw.WriteLine("");
					sw.WriteLine("\t\tpublic void CreateModules()\n\t\t{\n");
					sw.WriteLine(_l.ToString());
					sw.WriteLine("\t\t}");

					// class
					sw.WriteLine("\t}");

					// namespace
					sw.WriteLine("}");
				}
			}

		}

		static int SortHanlde(Type a, Type b)
		{
			SortAttribute[] attsA = a.GetCustomAttributes(typeof(SortAttribute), false) as SortAttribute[];
			SortAttribute[] attsB = b.GetCustomAttributes(typeof(SortAttribute), false) as SortAttribute[];

			int valA = attsA != null && attsA.Length > 0 ? attsA[0].val : 0;
			int valB = attsB != null && attsB.Length > 0 ? attsB[0].val : 0;

			return valA - valB;
		}
	}
}
#endif