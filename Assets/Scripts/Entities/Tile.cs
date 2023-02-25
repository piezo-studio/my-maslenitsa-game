using System;
using Entities.Actors;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Entities
{
	public class Tile : MonoBehaviour, IPointerClickHandler
	{
		/// <summary>
		/// Ссылка на скрипт игры.
		/// </summary>
		private Play _controller;
		/// <summary>
		/// Ссылка на якорь поля.
		/// </summary>
		private TileAnchor _anchor;
		/// <summary>
		/// Координаты тайла на поле.
		/// </summary>
		public Vector2Int coordinates;
		/// <summary>
		/// Ссылка на существо на тайле.
		/// </summary>
		public IActor Actor;
		/// <summary>
		/// Тип актёра на этом тайле.
		/// </summary>
		public ActorType ActorType;

		public IActor OnSpawn(Play script, TileAnchor anchor)
		{
			_controller = script;
			_anchor = anchor;
			coordinates = _anchor.coordinates;
			Actor = GetComponentInChildren<IActor>();
			ActorType = Actor.GetActorType();
			return Actor;
		}

		public Vector2Int GetGridPosition() => coordinates;

		public void OnPointerClick(PointerEventData eventData) => _controller.OnTileClicked(this);
	}
}