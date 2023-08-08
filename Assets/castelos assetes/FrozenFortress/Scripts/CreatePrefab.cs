using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class CreatePrefab : MonoBehaviour 
{
	#if UNITY_EDITOR
	[MenuItem ("GameObject/Construct Prefab From Parts")]
	static void DoSomething()
	{
		GameObject[] objects = Selection.gameObjects;

		if (objects.Length < 1 ) 
		{
			Debug.Log ("No objects selected..");

			return;
		}

		for (int i = 0; i < objects.Length; i++) 
		{
			Object obj = objects[i];

			Debug.Log (obj);
		}

		float x = 0f;
		float y = 0f;
		float z = 0f;

		foreach (GameObject currentObj in objects ) 
		{
			x += currentObj.transform.position.x;
			y += currentObj.transform.position.y;
			z += currentObj.transform.position.z;
		}

		GameObject go = new GameObject ("Empty");

		int num = objects.Length;

		Selection.activeGameObject = go;
		go.transform.position = new Vector3 (x/num, y/num, z/num);

		foreach (GameObject obj in objects) 
		{
			obj.transform.parent = go.transform;
		}
	}
	#endif
}
