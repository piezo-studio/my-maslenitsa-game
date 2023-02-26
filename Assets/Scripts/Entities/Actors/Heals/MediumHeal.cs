namespace Entities.Actors.Heals
{
	public class MediumHeal : SmallHeal
	{
		protected override void OnSpawn()
		{
			Type = ActorType.MediumHeal;
		}
	}
}