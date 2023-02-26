using Entities.Actors;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Entities
{
	public class Tile : MonoBehaviour, IPointerClickHandler
	{
		/// <summary>
		/// Координаты тайла на поле.
		/// </summary>
		public Vector2Int coordinates;

		/// <summary>
		/// Ссылка на якорь поля.
		/// </summary>
		private TileAnchor _anchor;

		/// <summary>
		/// Ссылка на скрипт игры.
		/// </summary>
		private Play _controller;

		/// <summary>
		/// Ссылка на существо на тайле.
		/// </summary>
		public Actor actor;

		public void OnPointerClick(PointerEventData eventData)
		{
			_controller.OnTileClicked(this);
		}

		public Actor OnSpawn(Play script, TileAnchor anchor, ActorData actorData)
		{
			_controller = script;
			_anchor = anchor;
			coordinates = _anchor.coordinates;
			
			// Спавним актёра
			actor = Instantiate(actorData.prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<Actor>();
			actor.ChangeValue(actorData.hp, actorData.hpDelta, _controller.Rnd);

			Debug.Log($"Tile spawned @ {coordinates} with {Who()}.");

			transform.localPosition = Vector3.zero;
			
			if (Who() == ActorType.Player)
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("tile_bg2");

			_anchor.actor = actor;
			
			return actor;
		}

		public ActorType Who() => actor.Who();

		public Config GetConfig() => _controller.cfg;
	}
}