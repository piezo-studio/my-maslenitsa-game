namespace Entities.Actors.Bosses
{
	public class Boss4 : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Boss4;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}