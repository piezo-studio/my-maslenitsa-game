using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Actors
{
	public class Player : MonoBehaviour, IActor
	{
		public Tile ownedTile;
		private readonly ActorType _actorType;
		public PlayerActionMode mode;

		/// <summary>
		/// Определяем с самого начала дескрипторы нашего 
		/// </summary>
		public Player()
		{
			_actorType = ActorType.Player;
			mode = PlayerActionMode.Regular;
		}

		void Awake()
		{
			ownedTile = transform.parent.GetComponent<Tile>();
		}
		
		public Vector2Int GetGridPosition() => ownedTile.GetGridPosition();
		public ActorType GetActorType() => _actorType;
	}
}