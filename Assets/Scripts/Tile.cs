using System;
using Actors;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
	private Field _field;
	public Actor Actor { get; private set; }
	public Vector2Int coordinates;

	private void Awake()
	{
		_field = transform.Find("GameField").GetComponent<Field>();
		GetActor();
	}

	private void GetActor()
	{
		Actor = gameObject.FindChildByName("Actor").GetComponent<Actor>();
		if (Actor.Equals(null))
			throw new NullReferenceException($"Tile @{coordinates} did not find an Actor!");
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_field.OnTileClicked(this);
	}
}