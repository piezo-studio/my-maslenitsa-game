using UnityEngine;

namespace Actors
{
	public class Player : Actor
	{
		/// <summary>
		/// <para>
		/// Defines types of Interaction Modes for the Player.
		/// </para>
		/// <para>
		///	<c>Movement</c> — The Player stumbles upon tiles, always invoking their <c>OnPlayerInteraction()</c> method.
		/// </para>
		///	<para>
		///	<c>Weapon</c> — The player deals damage to enemies.
		/// </para>
		/// <para>
		///	<c>Teleport</c> — The Player swaps places with the target Tile.
		/// </para>
		/// </summary>
		public enum PlayerInteractionMode
		{
			Movement,
			Weapon,
			Teleport
		}
		
		[HideInInspector] public PlayerInteractionMode interactionMode;
		
	}
}