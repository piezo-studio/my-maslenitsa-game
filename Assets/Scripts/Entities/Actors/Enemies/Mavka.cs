namespace Entities.Actors.Enemies
{
	public class Mavka : Wolf
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Mavka;
		}
	}
}