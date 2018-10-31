using btbrpg.turns;

namespace btbrpg.fsn
{
	public abstract class StateAction
	{
		public abstract void Execute(StateManager states, SessionManager sm, Turn t);
	}
}
