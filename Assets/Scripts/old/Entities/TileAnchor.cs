using System;
using Entities.Actors;
using UnityEngine;

namespace Entities
{
	public class TileAnchor : MonoBehaviour
	{
		[SerializeField] public Vector2Int coordinates;
		public old.Entities.Tile tile;
		public Actor actor;

		public void SaveTile(old.Entities.Tile newTile)
		{
			if (tile != null)
				throw new IndexOutOfRangeException($@"Attempted to tie {newTile} to coordinates {coordinates}");
			tile = newTile;
		}

		public void ForgetTile()
		{
			tile = null;
			gameObject.DestroyAllChildren();
		}

		public void SwapTile(TileAnchor target)
		{
			var newTile = target.tile;
			
			// Отдаём свой тайл
			tile.transform.parent = target.gameObject.transform;
			target.tile = tile;
			target.tile.OnSwapped(target);
			
			// Забираем чужой тайл
			if (newTile is not null)
			{
				newTile.transform.parent = this.gameObject.transform;
				this.tile = newTile;
				this.tile.OnSwapped(this);
			}
			else
			{
				tile = null;
			}
		}
	}
}