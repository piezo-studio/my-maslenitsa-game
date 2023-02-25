using UnityEngine;

namespace Entities.Actors
{
	public abstract class Actor : IActor
	{
		private readonly ActorType _actorType;
		public Tile OwnedTile;
		public PlayerActionMode Mode;

		

		public Vector2Int GetGridPosition() => OwnedTile.coordinates;
		public ActorType GetActorType() => _actorType;
	}
}