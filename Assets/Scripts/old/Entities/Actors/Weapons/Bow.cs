namespace Entities.Actors.Weapons
{
	public class Bow : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Bow;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player equips the Bow
		}
	}
}