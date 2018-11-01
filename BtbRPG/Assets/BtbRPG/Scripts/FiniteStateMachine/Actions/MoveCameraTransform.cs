using UnityEngine;

using btbrpg.so;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public class MoveCameraTransform : StateAction
	{
		TransformVariable cameraTransform;
		FloatVariable horizontal;
		FloatVariable vertical;

		VariablesHolder varHolder;

		public MoveCameraTransform(VariablesHolder holder)
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
