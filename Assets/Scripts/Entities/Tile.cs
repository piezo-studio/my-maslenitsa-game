using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Entities
{
	public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
	{
		[SerializeField] public GameObject anchor;
		public IFieldEntity Entity;

		private void Awake()
		{
			
		}

		public Vector2Int GetGridPosition() => this.transform.parent.GetComponent<TileAnchor>().coordinates;

		public void OnTileClick()
		{
			Debug.Log($@"Yo you did it, congrats. You tapped on {GetGridPosition()}");
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			throw new NotImplementedException();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			throw new NotImplementedException();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			throw new NotImplementedException();
		}
	}
}