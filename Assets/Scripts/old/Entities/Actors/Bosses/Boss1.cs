namespace Entities.Actors.Bosses
{
	public class Boss1 : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Boss1;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}