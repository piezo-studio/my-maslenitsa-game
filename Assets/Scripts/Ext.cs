using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Ext
{
	public static int IntDistance(this Vector2Int a, Vector2Int b) =>
		Math.Abs(a.x - b.x + a.y - b.y);
	
	public static int IntDistanceDiagonal(this Vector2Int a, Vector2Int b) =>
		Math.Max(a.x - b.x, a.y - b.y);
	public static void DestroyChildren(this GameObject t) {
		t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
	}

	public static GameObject FindChildByName(this GameObject parent, string name)
	{
		for (var i = 0; i < parent.transform.childCount; i++)
		{
			var child = parent.transform.GetChild(i).gameObject;
			if (child.name == name)
				return child;

			var newParent = FindChildByName(child, name);

			if (newParent != null)
				return newParent;
		}

		return null;
	}
}