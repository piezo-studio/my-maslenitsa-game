using System;
using Entities.Actors;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
	[Serializable]
	public record ActorData
	{
		[SerializeField] public GameObject prefab;
		[SerializeField] public ActorType type; 
		[SerializeField] public int hp; 
		[SerializeField] public int hpDelta;
		[SerializeField] public int lootID; 
		[SerializeField] public int lootHpMod;
		[SerializeField] public int lootHpModDelta; 
		[SerializeField] public float level0;
		[SerializeField] public float level1; 
		[SerializeField] public float boss1; 
		[SerializeField] public float level2; 
		[SerializeField] public float boss2; 
		[SerializeField] public float level3; 
		[SerializeField] public float boss3; 
		[SerializeField] public float level4; 
		[SerializeField] public float boss4;
		
		public float GetWeightAtLevel(Level level)
		{
			return level switch
			{
				Level.Tutorial => level0,
				Level.Level1 => level1,
				Level.Boss1 => boss1,
				Level.Level2 => level2,
				Level.Boss2 => boss2,
				Level.Level3 => level3,
				Level.Boss3 => boss3,
				Level.Level4 => level4,
				Level.Boss4 => boss4,
				_ => 0f
			};
		}
	}
}