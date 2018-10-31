using UnityEngine;
using System.Collections;

namespace btbrpg.turns
{
	public abstract class Phase : ScriptableObject
	{
		public string phaseName;

		//[System.NonSerialized]
		public bool forceExit;

        [System.NonSerialized] protected bool isInit;

        public abstract bool IsComplete(SessionManager sm, Turn turn);

		public abstract void OnStartPhase(SessionManager sm, Turn turn);

		public virtual void OnEndPhase(SessionManager sm, Turn turn)
		{
			isInit = false;
			forceExit = false;
		}
	}
}
