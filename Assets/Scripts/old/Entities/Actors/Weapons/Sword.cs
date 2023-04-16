namespace Entities.Actors.Weapons
{
	public class Sword : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Sword;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player equips the Sword
		}
	}
}