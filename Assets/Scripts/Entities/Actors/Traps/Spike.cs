namespace Entities.Actors.Traps
{
	public class Spike : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Spike;
		}
	}
}