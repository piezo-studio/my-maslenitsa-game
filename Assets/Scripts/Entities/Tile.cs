using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
	public class Tile : MonoBehaviour
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
	}
}