namespace Entities.Actors.Traps
{
	public class Mush : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Mush;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}