﻿namespace Entities.Actors
{
	public class Empty : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Empty;
			ValueLabel.gameObject.SetActive(false);
		}
	}
}