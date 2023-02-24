using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
	public class TileAnchor : MonoBehaviour
	{
		[SerializeField] public Vector2Int coordinates;
	}
}