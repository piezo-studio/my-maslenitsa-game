using System;
using Actors;
using UnityEngine;

namespace Behaviours
{
	public abstract class ActorBehaviour : ScriptableObject
	{
		public Action<Actor> OnSpawn; 
		public Action<Actor, Actor> OnInteraction;
		public Action<Actor> OnTurn;
		public Action<Actor, Actor> OnDamageTaken;
		public Action<Actor, Actor> OnDeath;
	}
}