namespace Entities.Actors.Enemies
{
	public class Drekavak : Wolf
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Drekavak;
		}
	}
}