using System;
using UnityEngine;

namespace Entities
{
	public class TileAnchor : MonoBehaviour
	{
		[SerializeField] public Vector2Int coordinates;
		public Tile tile;

		public void SaveTile(Tile newTile)
		{
			if (tile != null)
				throw new IndexOutOfRangeException($@"Attempted to tie {newTile} to coordinates {coordinates}");
			tile = newTile;
		}

		public void ForgetTile()
		{
			tile = null;
		}

		public void SwapTile(Tile newTile)
		{
			ForgetTile();
			SaveTile(newTile);
		}
	}
}