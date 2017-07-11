using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SgtComponentPool))]
public class SgtComponentPool_Editor : SgtEditor<SgtComponentPool>
{
	protected override void OnInspector()
	{
		BeginDisabled();
			EditorGUILayout.TextField("Type", Target.TypeName);
			EditorGUILayout.IntField("Count", Target.Elements.Count);
		EndDisabled();
		EditorGUILayout.HelpBox("SgtComponentPool are not saved to your scene, so don't worry if you see it in edit mode.", MessageType.Info);
	}
}
#endif

[ExecuteInEditMode]
[AddComponentMenu("")]
public class SgtComponentPool : MonoBehaviour
{
	public static List<SgtComponentPool> AllComponentPools = new List<SgtComponentPool>();

	public string TypeName;

	public List<Component> Elements = new List<Component>();
	
	protected virtual void OnEnable()
	{
		AllComponentPools.Add(this);
	}

	protected virtual void OnDisable()
	{
		AllComponentPools.Remove(this);
	}
	
	protected virtual void OnDestroy()
	{
		for (var i = Elements.Count - 1; i >= 0; i--)
		{
			var element = Elements[i];
			
			if (element != null)
			{
				Object.DestroyImmediate(element.gameObject);
			}
		}
	}

#if UNITY_EDITOR
	protected virtual void Update()
	{
		if (Application.isPlaying == false)
		{
			SgtHelper.Destroy(gameObject);
		}
	}
#endif

#if UNITY_EDITOR
	protected virtual void OnDrawGizmos()
	{
		if (Application.isPlaying == false)
		{
			SgtHelper.Destroy(gameObject);
		}
	}
#endif
}

public static class SgtComponentPool<T>
	where T : Component
{
	private static SgtComponentPool pool;
	
	public static T Add(T entry)
	{
		return Add(entry, null);
	}

	public static T Add(T element, System.Action<T> onAdd)
	{
		if (element != null)
		{
			if (onAdd != null)
			{
				onAdd(element);
			}

			UpdateComponent(true);
#if UNITY_EDITOR
			element.gameObject.hideFlags = HideFlags.DontSave;
#endif
			element.transform.SetParent(pool.transform, false);

			element.gameObject.SetActive(false);

			pool.Elements.Add(element);
		}

		return null;
	}
	
	public static T Pop(Transform parent, string name, int layer)
	{
		var element = Pop();

		if (element != null)
		{
			element.name = name;

			element.transform.SetParent(parent, false);

			element.gameObject.layer = layer;
		}
		else
		{
			var gameObject = new GameObject(name);

			gameObject.layer = layer;

			gameObject.transform.SetParent(parent, false);

			element = gameObject.AddComponent<T>();
		}

		return element;
	}

	public static T Pop()
	{
		return Pop(null);
	}

	public static T Pop(System.Predicate<T> match)
	{
		UpdateComponent(false);

		if (pool != null)
		{
			var elements = pool.Elements;
			var index    = elements.Count - 1;

			if (match != null)
			{
				for (var i = index; i >= 0; i--)
				{
					var element = elements[i];

					if (match((T)element) == true)
					{
						index = i; break;
					}
				}
			}

			if (index >= 0)
			{
				var element = (T)elements[index];

				elements.RemoveAt(index);

				if (element != null)
				{
#if UNITY_EDITOR
					element.gameObject.hideFlags = HideFlags.None;
#endif
					element.gameObject.SetActive(true);
				}
				
				return element;
			}
		}

		return null;
	}
	
	private static void UpdateComponent(bool allowCreation)
	{
		if (pool == null)
		{
			var typeName = typeof(T).Name;

			pool = SgtComponentPool.AllComponentPools.Find(p => p.TypeName == typeName);

			if (pool == null && allowCreation == true)
			{
				pool = new GameObject("SgtComponentPool<" + typeName + ">").AddComponent<SgtComponentPool>();
				
				if (Application.isPlaying == true)
				{
					Object.DontDestroyOnLoad(pool);
				}
#if UNITY_EDITOR
				pool.gameObject.hideFlags = HideFlags.DontSave;
#endif
			}
		}
	}
}