namespace Entities.Actors.Weapons
{
	public class Bow : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Bow;
		}
	}
}