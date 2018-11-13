using UnityEngine;
using btbrpg.grid;
using btbrpg.characters;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public class MoveCharacterOnPathAction : StateAction
	{
		private bool isInit;

        private float t;
        private float rotT;
        private float speed = 5;

        private Node startNode;
        private Node targetNode;
        private Quaternion targetRot;
        private Quaternion startRot;

        private int index;

		public override void Execute(StateManager states, SessionManager sm, Turn turn)
		{
			GridCharacter gridCharacter = states.CurrentCharacter;

			if (!isInit)
			{
				if (gridCharacter == null || index > gridCharacter.currentPath.Count - 1)
				{
					states.SetStartingState();
					return;
				}

				isInit = true;
				startNode = gridCharacter.currentNode;
				targetNode = gridCharacter.currentPath[index];
				float t_ = t - 1;
				t_ = Mathf.Clamp01(t_);
				t = t_;
				float distance = Vector3.Distance(startNode.worldPosition, targetNode.worldPosition);
				speed = gridCharacter.moveSpeed / distance;

				Vector3 direction = targetNode.worldPosition - startNode.worldPosition;
				targetRot = Quaternion.LookRotation(direction);
				startRot = gridCharacter.transform.rotation;

                gridCharacter.PlayAnimation("Movement");
			}

			t += states.delta * speed;
			rotT += states.delta * gridCharacter.moveSpeed * 2;

			if (rotT > 1)
			{
				rotT = 1;
			}

			gridCharacter.transform.rotation = Quaternion.Slerp(startRot, targetRot, rotT);

			if (t > 1)
			{
				isInit = false;
				gridCharacter.currentNode.character = null;
				gridCharacter.currentNode = targetNode;
				gridCharacter.currentNode.character = gridCharacter;

				index++;
				if (index > states.CurrentCharacter.currentPath.Count - 1)
				{
					//We moved on to our path
					t = 1;
					index = 0;

					states.SetStartingState();
                    gridCharacter.PlayAnimation("Idle");
                }
			}

			Vector3 tp = Vector3.Lerp(startNode.worldPosition, targetNode.worldPosition, t);
			gridCharacter.transform.position = tp;
		}
	}
}
