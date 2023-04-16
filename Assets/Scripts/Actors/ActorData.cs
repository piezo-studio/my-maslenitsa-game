using Behaviours;
using UnityEngine;

namespace Actors
{
	[CreateAssetMenu(fileName = "ActorData", menuName = "Maslenitsa/ActorData", order = 0)]
	public class ActorData : ScriptableObject
	{
		[Header("Base descriptors")]
		public ActorType type;
		public Sprite sprite;
		public string localizationKey;
		[Header("HP")]
		public int hp;
		public int hpDelta;
		[Header("Behaviours")] 
		public System.Collections.Generic.List<ActorBehaviour> behaviours;
	}
}