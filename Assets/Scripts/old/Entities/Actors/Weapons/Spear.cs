namespace Entities.Actors.Weapons
{
	public class Spear : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Spear;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player equips the Spear
		}
	}
}