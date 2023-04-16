using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class Ext
{
	public static Random Rng = new Random();
	
	/// <summary>
	/// Calculates distance in a cartesian coordinate system between two vectors.
	/// </summary>
	/// <param name="a">The origin vector.</param>
	/// <param name="b">The target vector.</param>
	/// <returns></returns>
	public static int IntDistance(this Vector2Int a, Vector2Int b) =>
		Math.Abs(a.x - b.x + a.y - b.y);
	
	/// <summary>
	/// Calculates distance in a cartesian coordinate system between two vectors, where diagonal movement is allowed.
	/// </summary>
	/// <param name="a">The origin vector.</param>
	/// <param name="b">The target vector.</param>
	/// <returns></returns>
	public static int IntDistanceDiagonal(this Vector2Int a, Vector2Int b) =>
		Math.Max(a.x - b.x, a.y - b.y);
	
	/// <summary>
	/// Destroys children of this GameObject.
	/// </summary>
	/// <param name="t">This GameObject.</param>
	public static void DestroyChildren(this GameObject t) {
		t.transform.Cast<Transform>().ToList().ForEach(c => Object.Destroy(c.gameObject));
	}
	
	/// <summary>
	/// Destroys all children of this GameObject. Recursive.
	/// </summary>
	/// <param name="t">This GameObject.</param>
	public static void DestroyAllChildren(this GameObject t) {
		for (var i = 0; i < t.transform.childCount; i++)
		{
			var child = t.transform.GetChild(i).gameObject;
			DestroyAllChildren(child);
			Object.Destroy(child);
		}
	}

	/// <summary>
	/// Finds the child GameObject that matches by <c>name</c>.
	/// </summary>
	/// <param name="parent">The parent GameObject.</param>
	/// <param name="name">String name of the GameObject to be found.</param>
	/// <returns>The child GameObject or <c>null</c> if finds nothing.</returns>
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
	
	/// <summary>
	/// Finds the child GameObject that matches by <c>name</c>. Casts it to <c>T</c>.
	/// </summary>
	/// <param name="parent">The parent GameObject.</param>
	/// <param name="name">String name of the GameObject to be found.</param>
	/// <typeparam name="T">The type of object to return.</typeparam>
	/// <returns>The child GameObject or <c>null</c> if finds nothing.</returns>
	public static T FindChildByName<T>(this GameObject parent, string name) where T : class
	{
		var gameObject = FindChildByName(parent, name);
		try
		{
			return gameObject as T;
		}
		catch (Exception e)
		{
			Debug.LogError($"Tried to find child of {parent.name} with name {name} of type {typeof(T)} but failed.");
			return null;
		}
	}

	/// <summary>
	/// Calculates a list of coordinates that are aligned in a ray from an origin coordinate.
	/// The ray will calculate until it hits the boundary defined by min and max vectors.
	/// The origin coordiante is not included in the list.
	/// </summary>
	/// <param name="origin">The location to calculate from.</param>
	/// <param name="direction">The direction of the ray.</param>
	/// <param name="min">Combination of lowest <c>x</c> and <c>y</c> values.</param>
	/// <param name="max">Combination of highest <c>x</c> and <c>y</c> values.</param>
	/// <returns>The ordered list of coordinates, starting with one next to the origin.</returns>
	public static List<Vector2Int> GetRelativeLine(this Vector2Int origin, Vector2Int direction, Vector2Int min, Vector2Int max)
	{
		var result = new List<Vector2Int>();
		switch (direction)
		{
			case { x: 1, y: 0 }:
				for (var i = origin.x + 1; i <= max.x; i++)
					result.Add(new Vector2Int(i, origin.y));
				break;
			case { x: 0, y: 1 }:
				for (var i = origin.y + 1; i <= max.y; i++)
					result.Add(new Vector2Int(origin.x, i));
				break;
			case { x: 0, y: -1 }:
				for (var i = origin.y - 1; i >= min.y; i--)
					result.Add(new Vector2Int(origin.x, i));
				break;
			case { x: -1, y: 0 }:
				for (var i = origin.x - 1; i >= min.x; i--)
					result.Add(new Vector2Int(i, origin.y));
				break;
		}
		return result;
	}
	
	/// <summary>
	/// Calculates a list of coordinates that are aligned in a ray from an origin coordinate.
	/// The ray will calculate for the defined range.
	/// The origin coordiante is not included in the list.
	/// </summary>
	/// <param name="origin">The location to calculate from.</param>
	/// <param name="direction">The direction of the ray.</param>
	/// <param name="range">The range of the ray.</param>
	/// <returns>The ordered list of coordinates, starting with one next to the origin.</returns>
	/// <exception cref="ArgumentOutOfRangeException">if <c>range</c> is or less than 0.</exception>
	public static List<Vector2Int> GetRelativeLine(this Vector2Int origin, Vector2Int direction, int range)
	{
		if (range <= 0)
			throw new ArgumentOutOfRangeException($"Attempted to calculate a ray of range {range} fromm {origin}");
		
		return GetRelativeLine(
			origin, 
			direction, 
			new Vector2Int(origin.x - range, origin.y - range),
			new Vector2Int(origin.x + range, origin.y + range)
			);
	}
}