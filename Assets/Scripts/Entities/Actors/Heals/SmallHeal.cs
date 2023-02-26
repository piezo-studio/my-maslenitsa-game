namespace Entities.Actors.Heals
{
	public class SmallHeal : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.SmallHeal;
		}
	}
}