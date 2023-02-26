namespace Entities.Actors.Heals
{
	public class SpFullHeal : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.SpFullHeal;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player is healed fully
		}
	}
}