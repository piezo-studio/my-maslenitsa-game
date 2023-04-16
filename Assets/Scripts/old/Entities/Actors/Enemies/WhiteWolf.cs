namespace Entities.Actors.Enemies
{
	public class WhiteWolf : Wolf
	{
		protected override void OnSpawn()
		{
			Type = ActorType.WhiteWolf;
		}
	}
}