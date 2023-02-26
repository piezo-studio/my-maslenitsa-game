namespace Entities.Actors
{
	public class Amber : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Amber;
		}

		public override void OnInteraction(Actor interactor)
		{
			if (interactor is Player player)
				player.consumables += value;
		}
	}
}