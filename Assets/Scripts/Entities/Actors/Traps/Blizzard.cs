namespace Entities.Actors.Traps
{
	public class Blizzard : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Blizzard;
		}
	}
}