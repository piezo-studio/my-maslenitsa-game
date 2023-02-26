using UnityEngine;

namespace Entities.Actors
{
	public interface IActor
	{
		public Vector2Int Where();
		public ActorType Who();
	}
}