using System;
using UnityEngine;

public static class Calc
{
	public static int IntDistance(Vector2Int a, Vector2Int b) =>
		Math.Abs(a.x - b.x + a.y - b.y);
	
	public static int IntDistanceDiagonal(Vector2Int a, Vector2Int b) =>
		Math.Max(a.x - b.x, a.y - b.y);
}