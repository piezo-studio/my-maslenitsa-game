using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Actors
{
	public class Player : Actor
	{
		/// <summary>
		/// Режим реакции игрока. Зависит от оружия.
		/// </summary>
		public PlayerActionMode mode;
		
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
			mode = PlayerActionMode.Regular;
			ValueMax = value;
			valueStr = 0;
			_secondaryLabel = GameObject.Find("secondary").GetComponent<TextMeshProUGUI>();
			_secondaryLabel.gameObject.SetActive(false);
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