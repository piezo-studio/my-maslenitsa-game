namespace Entities.Actors.Bosses
{
	public class Boss2 : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Boss2;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}