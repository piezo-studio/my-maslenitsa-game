using System;
using TMPro;
using UnityEngine;

namespace Actors
{
	public class Actor : MonoBehaviour
	{
		[Header("UI element links")]
		[SerializeField] protected TMP_Text labelName;
		[SerializeField] protected TMP_Text labelHp;
		[SerializeField] protected TMP_Text labelAp;

		/// <summary>
		/// Actor's initial config.
		/// </summary>
		[Header("Actor data")]
		[SerializeField] protected ActorData data;
		
		/// <summary>
		/// Actor's location on the game Field.
		/// </summary>
		protected Vector2Int Location;

		public int Hp { get; private set; }
		public int HpMax { get; private set; }

		public event Action<Actor> OnSpawn; 
		public event Action<Actor, Actor> OnInteraction;
		public event Action<Actor> OnTurn;
		public event Action<Actor, Actor> OnDamageTaken;
		public event Action<Actor, Actor> OnDeath;

		/*
		public UnityEvent onSpawn = new UnityEvent();
		public UnityEvent onInteraction = new UnityEvent();
		public UnityEvent onTurn = new UnityEvent();
		public UnityEvent onDamageTaken = new UnityEvent();
		public UnityEvent onDeath = new UnityEvent();
		*/

		/// <summary>
		/// Initializes the Actor: selects localization, sprite, and generates HP.
		/// </summary>
		private void Awake()
		{
			// -- Initialize visuals
			if (data.type != ActorType.Player) 
				labelAp.gameObject.SetActive(false);

			labelName.text = "a";
			
			// -- Initialize HP
			Hp = data.hp + Ext.Rng.Next(-data.hpDelta, data.hpDelta);
			HpMax = Hp;
			
			// -- Initialize game logic
			foreach (var behaviour in data.behaviours)
			{
				if (!ReferenceEquals(behaviour.OnSpawn, null))
					OnSpawn += behaviour.OnSpawn;
				if (!ReferenceEquals(behaviour.OnInteraction, null))
					OnInteraction += behaviour.OnInteraction;
				if (!ReferenceEquals(behaviour.OnTurn, null))
					OnTurn += behaviour.OnTurn;
				if (!ReferenceEquals(behaviour.OnDamageTaken, null))
					OnDamageTaken += behaviour.OnDamageTaken;
				if (!ReferenceEquals(behaviour.OnDeath, null))
					OnDeath += behaviour.OnDeath;
			}
			
			// -- Invoke OnSpawn game logic
			OnSpawn?.Invoke(this);
		}

		private void OnDestroy()
		{
			// -- Release all events
			foreach (var d in OnSpawn?.GetInvocationList()!)
				OnSpawn -= (Action<Actor>)d;
			foreach (var d in OnInteraction?.GetInvocationList()!)
				OnInteraction -= (Action<Actor, Actor>)d;
			foreach (var d in OnTurn?.GetInvocationList()!)
				OnTurn -= (Action<Actor>)d;
			foreach (var d in OnDamageTaken?.GetInvocationList()!)
				OnDamageTaken -= (Action<Actor, Actor>)d;
			foreach (var d in OnDeath?.GetInvocationList()!)
				OnDeath -= (Action<Actor, Actor>)d;
			
			// -- Other stuff?
			
		}

		public ActorType Who() => data.type;

		public Vector2Int Where() => Location;

		public void AddHp(int change, Actor source)
		{
			Hp += change;
			
			if (change < 0)
				OnDamageTaken?.Invoke(this, source);

			CheckHp(source);
		}

		public void SetHp(int newHp, Actor source)
		{
			if (newHp < Hp)
			{
				Hp = newHp;
				OnDamageTaken?.Invoke(this, source);
			}
			else
				Hp = newHp;
			
			CheckHp(source);
		}

		private void CheckHp(Actor source)
		{
			if (Hp > HpMax)
				Hp = HpMax;
			
			if (Hp > 0) return;
			OnDeath?.Invoke(this, source);
			Die();
		}

		private void Die()
		{
			// Call DestroyAllChildren() on Tile.
			// Tile should inform the Game script that it is dying,
			// so we either move and spawn a new one, or replace this Tile
			// with an empty one.
		}
	}

	/// <summary>
	/// The type of Actor. Relevant for turn and reaction event handling.
	/// </summary>
	public enum ActorType
	{
		Player,		// Players are controlled by the user and act on Player Turns.
		Boss,		// Bosses are like Enemies, except they act on Enemy turns, not just as a reaction.
		Enemy,		// Enemies are hostile to the Player, but can be damaged with Weapons safely.
		Trap,		// Traps sometimes act on Enemy turns, but usually they're just reactionary trouble.
		Pickup,		// These items only have immediate reactions to the Player, can't be damaged by most weapons.
		Equipment,	// These items are immediately equipped by the Player, changing their Movement Type.
		Consumable	// These items are stored in Player's inventory until triggered or activated.
	}
}