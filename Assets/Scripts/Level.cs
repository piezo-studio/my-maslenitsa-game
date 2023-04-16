using System;
using System.Collections.Generic;
using Actors;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Maslenitsa/Level", order = 10)]
public class Level : ScriptableObject
{
	public List<ActorLevelData> actorWeights;
}

[Serializable]
public struct ActorLevelData
{
	public ActorData actor;
	[Range(0f, 1f)] public float weight;
}