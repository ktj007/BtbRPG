using UnityEngine;

using SO;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public class InputAction : StateAction
	{
		VariablesHolder varHolder;

		public InputAction(VariablesHolder holder)
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
