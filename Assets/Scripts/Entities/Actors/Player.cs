using System.Linq;
using UnityEngine;

namespace Entities.Actors
{
	public class Player : Actor
	{
		public PlayerActionMode mode;
		public int value_str;

		protected override void OnSpawn()
		{
			Type = ActorType.Player;
			mode = PlayerActionMode.Regular;
			value_str = 0;
		}
	}
	
	public enum PlayerActionMode
	{
		Regular = 0,
		MeleeWeapon = 1,
		RangedWeapon = 2,
		Spell = 3,
		LongWeapon
	}
}