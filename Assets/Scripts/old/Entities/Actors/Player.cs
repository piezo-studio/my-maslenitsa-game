using TMPro;
using UnityEngine;

namespace Entities.Actors
{
	public class Player : Actor
	{
		/// <summary>
		/// Количество валюты (янтарей) у игрока.
		/// </summary>
		public int consumables = 0;
		
		/// <summary>
		/// Режим реакции игрока. Зависит от оружия.
		/// </summary>
		public PlayerActionMode mode = PlayerActionMode.Regular;
		
		/// <summary>
		/// Значение на оружии.
		/// </summary>
		public int valueStr;
		
		/// <summary>
		/// Текст значения оружия.
		/// </summary>
		private TextMeshProUGUI _secondaryLabel;

		protected override void OnSpawn()
		{
			Type = ActorType.Player;
			ValueMax = value;
			valueStr = 0;
			_secondaryLabel = GameObject.Find("secondary").GetComponent<TextMeshProUGUI>();
			_secondaryLabel.gameObject.SetActive(false);
		}

		public override void OnInteraction(Actor interactor)
		{
			throw new System.ArgumentException("Player's reaction can't be expected here;" + 
			                                   " seek implementation in Play");
		}
	}
	
	public enum PlayerActionMode
	{
		Regular = 0,
		MeleeWeapon = 1,
		RangedWeapon = 2,
		Spell = 3,
		LongWeapon = 4
	}
}