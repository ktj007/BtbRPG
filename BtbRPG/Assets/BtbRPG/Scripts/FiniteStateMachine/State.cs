using System.Collections.Generic;

using btbrpg.turns;

namespace btbrpg.fsn
{
	public class State 
	{
		public List<StateAction> actions = new List<StateAction>();

		public void Tick(StateManager states, SessionManager sm, Turn t)
		{
			if (states.forceExit)
				return;

			for (int i = 0; i < actions.Count; i++)
			{
				actions[i].Execute(states, sm, t);
			}
		}
	}
}
