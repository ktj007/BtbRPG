using System.Collections.Generic;
using UnityEngine;

using btbrpg.characters;
using btbrpg.grid;
using btbrpg.holders;
using btbrpg.turns;
using System;

namespace btbrpg.fsn
{
	public abstract class StateManager : MonoBehaviour
	{
		public State currentState;
        public State startingState;

		public bool forceExit;

		public Node currentNode;
        public Node prevNode;
        public float delta;

        public PlayerHolder playerHolder;

        public GridCharacter currentCharacter;
        public GridCharacter CurrentCharacter
        {
            get
            {
                return currentCharacter;
            }

            set
            {
                if(currentCharacter != null)
                {
                    currentCharacter.OnDeselect(playerHolder);
                }

                currentCharacter = value;
            }
        }

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

		public State GetState(string id)
		{
			State result = null;
			allStates.TryGetValue(id, out result);
			return result;
		}

        public void SetStartingState()
        {
            currentState = startingState;
        }
    }
}
