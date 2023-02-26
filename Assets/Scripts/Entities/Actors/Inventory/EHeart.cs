namespace Entities.Actors.Inventory
{
	public class EHeart : Actor
	{
		protected override void OnSpawn()
		{
			Type = ActorType.EHeart;
		}
		
		public override void OnInteraction(Actor interactor)
		{
			// Player equips the Heart
		}
	}
}