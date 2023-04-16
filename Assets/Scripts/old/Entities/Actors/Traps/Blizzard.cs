namespace Entities.Actors.Traps
{
	public class Blizzard : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Blizzard;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}