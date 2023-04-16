namespace Entities.Actors.Enemies
{
	public class Skeleton : Wolf
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Skeleton;
		}
	}
}