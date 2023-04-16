namespace Entities.Actors.Traps
{
	public class Spike : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Spike;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage if on spikes
		}
	}
}