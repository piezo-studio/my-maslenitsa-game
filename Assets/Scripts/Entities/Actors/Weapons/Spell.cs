namespace Entities.Actors.Weapons
{
	public class Spell : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.Spell;
		}
	}
}