using UnityEngine;

using btbrpg.turns;

using btbrpg.so.variables;

namespace btbrpg.fsn
{
	public class MoveCameraAction : StateAction
	{
		TransformVariable cameraTransform;
		FloatVariable horizontal;
		FloatVariable vertical;

		VariablesHolder varHolder;

		public MoveCameraAction(VariablesHolder holder)
		{
			varHolder = holder;
			cameraTransform = varHolder.cameraTransform;
			horizontal = varHolder.horizontalInput;
			vertical = varHolder.verticalInput;
		}

		public override void Execute(StateManager states, SessionManager sm, Turn t)
		{
			Vector3 tp = cameraTransform.value.forward * (vertical.value * varHolder.cameraMoveSpeed);
			tp += cameraTransform.value.right * (horizontal.value * varHolder.cameraMoveSpeed);

			cameraTransform.value.position += tp * states.delta;
		}
	}
}
