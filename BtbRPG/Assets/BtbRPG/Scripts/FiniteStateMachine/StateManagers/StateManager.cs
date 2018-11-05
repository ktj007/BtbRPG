using System.Collections.Generic;
using UnityEngine;

using btbrpg.grid;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public abstract class StateManager : MonoBehaviour
	{
		public State currentState;
		public bool forceExit;

		public Node currentNode;
        public Node prevNode;
        public float delta;

		protected Dictionary<string, State> allStates = new Dictionary<string, State>();

		private void Start()
		{
			Init();
		}

		public abstract void Init();
		
        // Do not use Update() here, as it is a turn-based game. Like this we can control, when the Tick() happens 
		public void Tick(SessionManager sm, Turn turn)
		{
            delta = sm.delta;

			if (currentState != null)
			{
				currentState.Tick(this, sm, turn);
			}

			forceExit = false;
		}

		public void SetState(string id)
		{
			State targetState = GetState(id);
			if (targetState == null)
			{
				Debug.LogError("State with id : " + id + " cannot be found! Check your states and ids!");
			}

			currentState = targetState;
		}

		State GetState(string id)
		{
			State result = null;
			allStates.TryGetValue(id, out result);
			return result;
		}
	}
}
