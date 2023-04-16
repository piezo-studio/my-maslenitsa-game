namespace Entities.Actors.Heals
{
	public class BigHeal : SmallHeal
	{
		protected override void OnSpawn()
		{
			Type = ActorType.BigHeal;
		}
	}
}