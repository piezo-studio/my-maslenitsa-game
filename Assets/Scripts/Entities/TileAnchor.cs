using System;
using Entities.Actors;
using UnityEngine;

namespace Entities
{
	public class TileAnchor : MonoBehaviour
	{
		[SerializeField] public Vector2Int coordinates;
		public Tile tile;
		public Actor actor;

		public void SaveTile(Tile newTile)
		{
			if (tile != null)
				throw new IndexOutOfRangeException($@"Attempted to tie {newTile} to coordinates {coordinates}");
			tile = newTile;
		}

		public void ForgetTile()
		{
			tile = null;
			gameObject.DestroyChildren();
		}

		public void SwapTile(TileAnchor target)
		{
			var newTile = target.tile;
			
			// Отдаём свой тайл
			tile.transform.parent = target.gameObject.transform;
			target.tile = tile;
			
			// Забираем чужой тайл
			newTile.transform.parent = this.gameObject.transform;
			this.tile = newTile;
		}
	}
}