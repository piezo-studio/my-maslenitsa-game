using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Entities.Actors
{
	public abstract class Actor : MonoBehaviour, IActor
	{
		private Tile _tile;
		protected ActorType Loot;
		protected int LootMod; // loot ± loot_delta
		protected ActorType Type;
		/// <summary>
		/// Макс. ХП.
		/// </summary>
		protected int ValueMax;
		public int value; // hp ± hp_delta
		protected TextMeshProUGUI ValueLabel;
		
		protected void Awake()
		{
			_tile = transform.parent.GetComponent<Tile>();
			ValueLabel = gameObject.FindChildByName("value").GetComponent<TextMeshProUGUI>();
			OnSpawn();
		}

		protected void OnDestroy()
		{
			gameObject.DestroyChildren();
		}

		/// <summary>
		/// Метод, вызываемый, когда тайл появляется. Каждый тип Актёра по-своему его обрабатывает.
		/// </summary>
		protected abstract void OnSpawn();

		/// <summary>
		/// Скрипт, производящий действия актёра в его ход.
		/// </summary>
		public void OnNPCTurn()
		{
			
		}

		/// <summary>
		/// Что происходит, если ударить (или убить) актёра. 
		/// </summary>
		protected void OnDamageTaken(bool isFatal)
		{
			if (isFatal)
			{
				_tile.controller.OnActorDeath(Where());
				_tile.Suicide();
			}
			else
			{
				
			}
		}

		/// <summary>
		/// Что происходит, если другой актёр взаимодействует с этим актёром.
		/// По умолчанию, просто даёт игроку зайти на своё место.
		/// </summary>
		/// <param name="interactor">Инициатор взаимодействия</param>
		public abstract void OnInteraction(Actor interactor);

		public int SetValue(int newValue)
		{
			ValueLabel.text = newValue.ToString();
			if (value > newValue && _tile.controller.gameState == PlayState.PlayerMove)
				OnDamageTaken(newValue <= 0);
			value = newValue;

			Debug.Log($"Set value to {value}");
			return value;
		}
		
		public int SetValue(int newValue, int valueDelta, Random rnd)
		{
			if (valueDelta != 0)
				newValue += rnd.Next(-valueDelta, valueDelta);
			return SetValue(newValue);
		}

		public Vector2Int Where() => _tile.Where();

		public ActorType Who() => Type;
	}
	
	public enum ActorType
	{
		Empty = 0,
		Player = 1,
		Wolf = 2,
		Amber = 3,
		Boss1 = 4,
		Boss2 = 5,
		Boss3 = 6,
		Boss4 = 7,
		Sword = 8,
		Drekavak = 9,
		Mara = 10,
		Mavka = 11,
		Spell = 12,
		Skeleton = 13,
		WhiteWolf = 14,
		SmallHeal = 15,
		MediumHeal = 16,
		BigHeal = 17,
		SpFullHeal = 18,
		EHeart = 19,
		Mush = 20,
		Blizzard = 21,
		Spear = 22,
		Bow = 23,
		Spike = 24
	}
}