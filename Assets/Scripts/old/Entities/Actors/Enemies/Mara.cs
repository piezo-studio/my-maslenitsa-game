namespace Entities.Actors.Enemies
{
	public class Mara : Wolf
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Mara;
		}
	}
}