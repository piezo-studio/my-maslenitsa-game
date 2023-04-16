namespace Entities.Actors.Bosses
{
	public class Boss3 : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Boss3;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}