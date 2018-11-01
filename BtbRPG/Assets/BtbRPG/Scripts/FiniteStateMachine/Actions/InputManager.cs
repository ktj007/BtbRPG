using UnityEngine;

using btbrpg.so;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public class InputManager : StateAction
	{
		VariablesHolder varHolder;

		public InputManager(VariablesHolder holder)
		{
			varHolder = holder;
		}

		public override void Execute(StateManager states, SessionManager sm, Turn t)
		{
			varHolder.horizontalInput.value = Input.GetAxis("Horizontal");
			varHolder.verticalInput.value = Input.GetAxis("Vertical");
		}
	}
}
