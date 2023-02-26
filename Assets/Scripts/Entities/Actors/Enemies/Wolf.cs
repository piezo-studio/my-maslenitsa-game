namespace Entities.Actors.Enemies
{
	public class Wolf : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Wolf;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player takes damage
		}
	}
}