namespace Entities.Actors.Enemies
{
	public class Wolf : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Wolf;
		}
	}
}