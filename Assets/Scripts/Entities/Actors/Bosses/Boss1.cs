namespace Entities.Actors.Bosses
{
	public class Boss1 : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Boss1;
		}
	}
}