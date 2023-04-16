using System;
using Entities.Actors;
using old;
using UnityEngine;

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
		
		public float GetWeightAtLevel(LevelOld level)
		{
			return level switch
			{
				LevelOld.Tutorial => level0,
				LevelOld.Level1 => level1,
				LevelOld.Boss1 => boss1,
				LevelOld.Level2 => level2,
				LevelOld.Boss2 => boss2,
				LevelOld.Level3 => level3,
				LevelOld.Boss3 => boss3,
				LevelOld.Level4 => level4,
				LevelOld.Boss4 => boss4,
				_ => 0f
			};
		}
	}
}