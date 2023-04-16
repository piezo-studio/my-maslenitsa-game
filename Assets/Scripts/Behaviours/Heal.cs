using Actors;
using UnityEngine;

namespace Behaviours
{
	[CreateAssetMenu(fileName = "Heal", menuName = "Maslenitsa/Behaviour/Consumable", order = 400)]
	public class Heal : ActorBehaviour
	{
		public bool fullHeal;

		private Heal()
		{
			OnInteraction = HealActor;
		}

		private void HealActor(Actor target, Actor source)
		{
			if (fullHeal)
				target.SetHp(target.HpMax, source);
			else
				target.AddHp(target.HpMax, source);
		}
	}
}