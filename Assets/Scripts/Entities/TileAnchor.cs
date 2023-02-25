using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Entities
{
	public class TileAnchor : MonoBehaviour
	{
		[SerializeField] public Vector2Int coordinates;

		private void AttachedSpawnedTile(Tile tile)
		{
			
		}
	}
}