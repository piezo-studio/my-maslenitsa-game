﻿namespace Entities.Actors.Heals
{
	public class SmallHeal : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.SmallHeal;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player is healed
		}
	}
}