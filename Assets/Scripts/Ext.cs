using System;
using System.Collections.Generic;
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

	public static List<Vector2Int> GetRelativeLine(Vector2Int origin, Vector2Int direction, Vector2Int min, Vector2Int max)
	{
		var result = new List<Vector2Int>();
		if (direction.x == 1 && direction.y == 0)
		{
			for (var i = origin.x + 1; i <= max.x; i++)
				result.Add(new Vector2Int(i, origin.y));
		}
		if (direction.x == 0 && direction.y == 1)
		{
			for (var i = origin.y + 1; i <= max.y; i++)
				result.Add(new Vector2Int(origin.x, i));
		}
		if (direction.x == -1 && direction.y == 0)
		{
			for (var i = origin.x - 1; i >= min.x; i--)
				result.Add(new Vector2Int(i, origin.y));
		}
		if (direction.x == 0 && direction.y == -1)
		{
			for (var i = origin.y - 1; i >= min.y; i--)
				result.Add(new Vector2Int(origin.x, i));
		}

		return result;
	}
}