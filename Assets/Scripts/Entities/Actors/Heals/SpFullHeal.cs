namespace Entities.Actors.Heals
{
	public class SpFullHeal : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.SpFullHeal;
		}
	}
}