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
	/// Текущее состояние игры: ход игрока или скрипта.
	/// </summary>
	public PlayState gameState = PlayState.NpcTurn;
	
	/// <summary>
	/// Генератор рандомных чисел.
	/// </summary>
	public readonly Random Rnd = new Random();

	/// <summary>
	/// Текущий уровнь (сложности).
	/// </summary>
	[SerializeField] private Level _gameLevel;

	/// <summary>
	/// Список якорей поля. По ним мы можем найти тайлы.
	/// </summary>
	private TileAnchor[,] _anchors;

	/// <summary>
	/// Координаты всех актёров.
	/// </summary>
	private Dictionary<Vector2Int, Actor> _actors;

	/// <summary>
	/// Ссылка на игрока.
	/// </summary>
	private Player _player;

	private void Start()
	{
		// Если список не существует, создаём его.
		_anchors ??= new TileAnchor[5, 5];
		_actors ??= new Dictionary<Vector2Int, Actor>(25);

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

		gameState = PlayState.PlayerTurn;
	}

	private void OnDestroy()
	{
		gameState = PlayState.GameEnd;
	}

	private void GenerateTile(Vector2Int coordinates)
	{
		if (_gameLevel == 0)
		{
			// TODO: Tutorial.
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
				//Debug.Log(
				//$"Weight for {(ActorType)i} on {coordinates} is {cfgActorWeights[i]}
				//(fieldActorWeights: {fieldActorWeights[i]}; maxWeight: {maxWeight})");
			}

			// Генерим случайное число (NextDouble() создаёт от 0f до 1f)
			var randomNum = Rnd.NextDouble() * maxWeight;

			//Debug.Log($"Rolled {randomNum}");
			
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
		//Debug.Log($"Spawning a tile with actor {type}…");
		var anchor = _anchors[coordinates.x, coordinates.y];
		var tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity, anchor.transform);
		
		var actor = tile.OnSpawn(this, anchor, cfg.tilesConfig[(int)type]);
		
		anchor.SaveTile(tile);
		_actors.Add(coordinates, actor);

		return actor;
	}

	public void OnTileClicked(Tile tile)
	{
		if (gameState != PlayState.PlayerTurn)
			return;
		
		if (tile.actor is Player)
		{
			// TODO: What happens when we tap the player?
			Debug.Log("Yep, that's you, good job.");
		}
		else
			switch (_player.mode)
			{
				case PlayerActionMode.Regular:
					if (tile.Where().IntDistance(_player.Where()) == 1)
						PlayerMove(_actors[tile.Where()]);
					// TODO: What happens when we click a non-actionable tile?
					break;
				case PlayerActionMode.MeleeWeapon:
					if (tile.Where().IntDistance(_player.Where()) == 1)
						PlayerDealDamage(_actors[tile.Where()]);
					// TODO: What happens when we click a non-actionable tile?
					break;
				case PlayerActionMode.RangedWeapon:
					if (tile.Where().IntDistanceDiagonal(_player.Where()) == 1)
						PlayerDealDamage(_actors[tile.Where()]);
					// TODO: What happens when we click a non-actionable tile?
					break;
				case PlayerActionMode.Spell:
					if (tile.Where().x == _player.Where().x || tile.Where().y == _player.Where().y)
						PlayerDealDamageLine(_actors[tile.Where()]);
					// TODO: What happens when we click a non-actionable tile?
					break;
				case PlayerActionMode.LongWeapon:
					if (tile.Where().IntDistance(_player.Where()) == 1)
						PlayerMove(_actors[tile.Where()]);
					else
					{
						if (tile.Where().IntDistanceDiagonal(_player.Where()) != 1
						    && tile.Where().IntDistance(_player.Where()) == 2)
							PlayerDealDamage(_actors[tile.Where()]);
						// TODO: What happens when we click a non-actionable tile?
					}
					break;
				default:
					throw new IndexOutOfRangeException(
						$"Attempted to move using unrecognized player mode {_player.mode}");
			}
	}

	private void PlayerDealDamageLine(Actor target)
	{
		throw new NotImplementedException();
	}

	private void PlayerDealDamage(Actor target)
	{
		throw new NotImplementedException();
	}

	private void PlayerMove(Actor target)
	{
		gameState = PlayState.PlayerMove;
		
		// Реакция тайла на игрока.
		target.OnInteraction(_player);

		// Убиваем цель, запоминаем где
		var moveFrom = _player.Where();
		var moveTo = target.Where();
		target.SetValue(0);
		
		// Двигаем игрока
		MoveTile(_player.Where(), moveTo);
		
		// Двигаем остальные тайлы позади него
		var direction = moveFrom - moveTo;
		var orderedTiles = Ext.GetRelativeLine(moveTo, direction, new Vector2Int(0, 0), new Vector2Int(4, 4));

		for (var i = 0; i < orderedTiles.Count() - 1; i++)
			MoveTile(orderedTiles[i] + direction, orderedTiles[i]);

		// Спавним новый тайл в конце
		GenerateTile(orderedTiles.Last());
		
		gameState = PlayState.PlayerTurn;
	}

	public void MoveTile(Vector2Int source, Vector2Int target)
	{
		if (source.IntDistance(target) != 1)
			throw new ArgumentOutOfRangeException($"Attempted to move tile @ {target} to {source}");
		if (_actors.ContainsKey(target))
			throw new ArgumentException(
				$"Attempted to move tile @ {target} to {source}, where there is {_actors[target]}");

		var sourceAnchor = _anchors[source.x, source.y];
		var targetAnchor = _anchors[target.x, target.y];
		
		sourceAnchor.SwapTile(targetAnchor);

		_actors.Remove(source);
		_actors.Add(target, targetAnchor.actor);
	}

	public void OnActorDeath(Vector2Int where)
	{
		if (gameState == PlayState.GameEnd)
			return;
		
		// Забываем актёра.
		_actors.Remove(where);
		
		// Если игрок сейчас в движении, ничего не спавним
		if (gameState == PlayState.PlayerMove)
			return;
		
		// Спавним на пустую клетку пустого актёра
		SpawnTile(where, ActorType.Empty);
	}
}

public enum PlayState
{
	PlayerTurn,
	PlayerMove,
	NpcTurn,
	NpcMove,
	GameEnd
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