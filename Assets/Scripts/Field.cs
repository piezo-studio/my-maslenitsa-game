using System.Collections.Generic;
using System.ComponentModel;
using Actors;
using UI;
using UnityEngine;

/// <summary>
/// The Field class defines the necessary parameters of the playing Field, holds information about all Actors on itself,
/// handles clicking and invocation of all the necessary game events.
/// </summary>
[RequireComponent(typeof(FlexibleGridLayout))]
public class Field : MonoBehaviour
{

	[EditorBrowsable] private GameObject _tilePrefab;
	
	[SerializeField] private Level level;
	
	/// <summary>
    /// A dictionary used for conversion of world position vectors into local grid coordinates.
    /// </summary>
    private Dictionary<Vector2Int, Vector3> _worldAnchors;

	private void Awake()
	{
		GenerateWorldAncors();
		
		// This line exists only in case we set up reference field objects.
		gameObject.DestroyAllChildren();

		
	}

	/// <summary>
	/// Generates new Wolrd Anchors for animations.
	/// </summary>
	private void GenerateWorldAncors()
	{
		// Initialize required values.
		var grid = GetComponent<FlexibleGridLayout>(); // Grid reference to pull necessary values from reference setup.

		_worldAnchors = new Dictionary<Vector2Int, Vector3>(grid.columns * grid.rows);
		
		for (var x = 0; x < grid.columns; x++)
		for (var y = 0; y < grid.rows; y++)
		{
			var worldAnchor = new Vector3();
			_worldAnchors.Add(new Vector2Int(x, y), worldAnchor);
		}
		
		// Disable the Grid, as we no longer have a use for it.
		grid.enabled = false;
	}

	private void GenerateTile()
	{
		
	}

	private void PlaceTile(Vector2Int coordinates) => 
		Instantiate(_tilePrefab, _worldAnchors[coordinates], Quaternion.identity);

	private void PlaceActor(Vector2Int coordinates, ActorData actorData)
	{
		
	}

	public void OnTileClicked(Tile clickedTile)
	{
		
		if (clickedTile.Actor is Player)
		{
			// TODO: Something happens when a Player is clicked.
			return;
		}
		
	}
}