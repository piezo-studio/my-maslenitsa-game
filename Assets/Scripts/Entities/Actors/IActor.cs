using UnityEngine;

namespace Entities.Actors
{
	public interface IActor
	{
		public Vector2Int GetGridPosition();
		public ActorType GetActorType();
	}
}