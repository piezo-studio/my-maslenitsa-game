using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Actors;
using UnityEngine;
using Random = System.Random;

public class Play : MonoBehaviour
{
	/// <summary>
	/// Префаб тайла. Его игра и спавнит всякий раз.
	/// </summary>
	[SerializeField] private Tile tilePrefab;

	/// <summary>
	/// Ссылка на конфиг.
	/// </summary>
	[SerializeField] public Config cfg;
	
	/// <summary>
	/// Генератор рандомных чисел.
	/// </summary>
	public Random Rnd = new Random();
	
	/// <summary>
	/// Текущее состояние игры: ход игрока или скрипта.
	/// </summary>
	private PlayState _gameState = PlayState.PlayerMove;

	/// <summary>
	/// Текущий уровнь (сложности).
	/// </summary>
	[SerializeField] private Level _gameLevel;
	
	/// <summary>
	/// Список со всеми актёрами.
	/// </summary>
	private List<Actor> _actors;

	/// <summary>
	/// Список якорей поля. По ним мы можем найти тайлы.
	/// </summary>
	private TileAnchor[,] _anchors;

	private Player _player;
	
	/// <summary>
	/// Количество валюты (янтарей) у игрока.
	/// </summary>
	private int _consumables = 0;

	private void Start()
	{
		// Если список не существует, создаём его.
		_anchors ??= new TileAnchor[5, 5];

		// Ищем все якори.
		for (var x = 0; x < 5; x++)
		for (var y = 0; y < 5; y++)
		{
			var anchor = transform.Find($"TileAnchor_X{x}Y{y}").GetComponent<TileAnchor>();
			_anchors[x, y] = anchor;
			// Debug.Log($"Found {_anchors[x, y]}");
		}
		
		// Спавним все штуки.
		for (var x = 0; x < 5; x++)
		for (var y = 0; y < 5; y++)
		{
			var anchor = _anchors[x, y];
			if (x == 2 && y == 2)
			{
				_player = SpawnTile(anchor.coordinates, ActorType.Player) as Player;
			}
			else
			{
				GenerateTile(anchor.coordinates);
			}
		}
	}

	private void GenerateTile(Vector2Int coordinates)
	{
		if (_gameLevel == 0)
		{
			//TODO: Tutorial.
		}
		else
		{
			// Собираем всё в конфиге в словарь
			var cfgActorWeights = cfg.tilesConfig.Select(data => 
				data.GetWeightAtLevel(_gameLevel)).ToList();

			// Теперь смотрим поле
			var fieldActorWeights = new List<float>(new float[cfgActorWeights.Count]);
			for (var x = 0; x < 5; x++)
			for (var y = 0; y < 5; y++)
			{
				var anchor = _anchors[x, y];
				// Если к клетке не привязан тайл, считаем, что там пусто
				if (anchor.tile == null)
					fieldActorWeights[0] += 4f;
				else
					fieldActorWeights[(int)anchor.tile.Who()] += 4f;
			}

			// Умножаем вес конфига на себя и делим на то, что на поле
			var maxWeight = 0f;
			for (var i = 0; i < cfgActorWeights.Count; i++)
			{
				if (fieldActorWeights[i] == 0f)
					fieldActorWeights[i] = 1f;
				cfgActorWeights[i] = cfgActorWeights[i] * cfgActorWeights[i] / fieldActorWeights[i] + maxWeight;
				maxWeight = cfgActorWeights[i];
				Debug.Log($"Weight for {(ActorType)i} on {coordinates} is {cfgActorWeights[i]} (fieldActorWeights: {fieldActorWeights[i]}; maxWeight: {maxWeight})");
			}

			// Генерим случайное число (NextDouble() создаёт от 0f до 1f)
			var randomNum = Rnd.NextDouble() * maxWeight;

			Debug.Log($"Rolled {randomNum}");
			
			// Выбираем тип объекта
			for (var i = 0; i < cfgActorWeights.Count; i++)
				if (randomNum <= cfgActorWeights[i])
				{
					SpawnTile(coordinates, (ActorType) i);
					break;
				}
		}
	}

	private Actor SpawnTile(Vector2Int coordinates, ActorType type)
	{
		Debug.Log($"Spawning a tile with actor {type}…");
		var anchor = _anchors[coordinates.x, coordinates.y];
		var tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity, anchor.transform);

		_actors ??= new List<Actor>();
		var actor = tile.OnSpawn(this, anchor, cfg.tilesConfig[(int)type]);
		
		anchor.SaveTile(tile);
		_actors.Add(actor);

		return actor;
	}

	public void OnTileClicked(Tile tile)
	{
		if (_gameState != PlayState.PlayerMove)
			return;
		if (tile.actor is Player)
			// TODO: What happens when we tap the player?
			Debug.Log("Yep, that's you, good job.");
		else
			switch (_player.mode)
			{
				case PlayerActionMode.Regular:
					if (Math.Abs(Vector2Int.Distance(_player.Where(), tile.coordinates) - 1f) < 0.1)
						MovePlayer();
					break;
				case PlayerActionMode.MeleeWeapon:

					break;
				case PlayerActionMode.RangedWeapon:

					break;
				case PlayerActionMode.Spell:

					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
	}

	private void MovePlayer()
	{
		throw new NotImplementedException();
	}
}

public enum PlayState
{
	PlayerMove,
	NpcMove
}

public enum Level
{
	Tutorial,
	Level1,
	Boss1,
	Level2,
	Boss2,
	Level3,
	Boss3,
	Level4,
	Boss4
}