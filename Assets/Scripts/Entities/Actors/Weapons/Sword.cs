namespace Entities.Actors.Weapons
{
	public class Sword : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Sword;
		}
	}
}