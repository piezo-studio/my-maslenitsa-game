using System;
using System.Collections.Generic;
using Entities;
using Entities.Actors;
using UnityEngine;
using UnityEngine.Serialization;

public class Play : MonoBehaviour
{ 
	private List<Tile> _tiles;
	private Player _player;

	public void SubscribeTile(Tile tile)
	{
		_tiles ??= new List<Tile>();

		_tiles.Add(tile);
	}

	public void OnTileClicked(Tile tile)
	{
		if (tile.Actor is Player)
		{
			// TODO: What happens when we tap the player?
		}
		else
		{
			switch (_player.mode)
			{
				case PlayerActionMode.Regular:
					
					break;
				case PlayerActionMode.MeleeWeapon:
					
					break;
				case PlayerActionMode.RangedWeapon:
					
					break;
				case PlayerActionMode.Spell:
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	private bool AreTilesAdjacent(Tile tile1, Tile tile2)
	{
		
		return false;
	}
}