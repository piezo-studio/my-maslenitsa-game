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
		public int value; // hp ± hp_delta
		protected TextMeshProUGUI ValueLabel;
		
		protected void Awake()
		{
			_tile = transform.parent.GetComponent<Tile>();
			ValueLabel = GameObject.Find("value").GetComponent<TextMeshProUGUI>();
			OnSpawn();
		}

		protected abstract void OnSpawn();

		public int ChangeValue(int newValue)
		{
			value = newValue;
			ValueLabel.text = value.ToString();
			return value;
		}
		
		public int ChangeValue(int newValue, int valueDelta, Random rnd)
		{
			value = newValue + rnd.Next(-valueDelta, valueDelta);
			ValueLabel.text = value.ToString();
			return value;
		}

		public Vector2Int Where()
		{
			return _tile.coordinates;
		}

		public ActorType Who()
		{
			return Type;
		}
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