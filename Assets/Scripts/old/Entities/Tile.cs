using Entities;
using Entities.Actors;
using UnityEngine;
using UnityEngine.EventSystems;

namespace old.Entities
{
	public class Tile : MonoBehaviour, IPointerClickHandler
	{
		/// <summary>
		/// Ссылка на якорь поля.
		/// </summary>
		private TileAnchor _anchor;

		/// <summary>
		/// Ссылка на скрипт игры.
		/// </summary>
		public Play controller;

		/// <summary>
		/// Ссылка на существо на тайле.
		/// </summary>
		public Actor actor;

		public void OnPointerClick(PointerEventData eventData)
		{
			controller.OnTileClicked(this);
		}

		public Actor OnSpawn(Play script, TileAnchor anchor, ActorData actorData)
		{
			controller = script;
			_anchor = anchor;

			// Спавним актёра
			Debug.Log($"Soawning Actor {actorData.type} with value = {actorData.hp}");
			actor = Instantiate(actorData.prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Actor>();
			actor.SetValue(actorData.hp, actorData.hpDelta, controller.Rnd);

			Debug.Log($"Tile spawned @ {Where()} with {Who()}.");

			transform.localPosition = Vector3.zero;
			
			if (Who() == ActorType.Player)
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("tile_bg2");

			_anchor.actor = actor;
			
			return actor;
		}

		public void OnSwapped(TileAnchor newAnchor)
		{
			_anchor = newAnchor;
			
			// Здесь мы двигаемся на новое место
			// TODO: Animation!!
			transform.localPosition = Vector3.zero;
		}

		public void Suicide()
		{
			_anchor.ForgetTile();
		}

		public ActorType Who() => actor.Who();

		public Vector2Int Where() => _anchor.coordinates;

		public Config GetConfig() => controller.cfg;
	}
}