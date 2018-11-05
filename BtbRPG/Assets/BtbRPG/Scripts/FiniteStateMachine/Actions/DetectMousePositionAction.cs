using UnityEngine;

using btbrpg.grid;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public class DetectMousePositionAction : StateAction
	{
        private const int MAX_DISTANCE = 1000;

        public override void Execute(StateManager states, SessionManager sm, Turn t)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, MAX_DISTANCE))
            {
                states.currentNode = sm.gridManager.GetNode(hit.point);
                

                if (states.currentNode != null)
                {
                    Debug.Log("current node is node: " + states.currentNode.x + ", " + states.currentNode.z);

                    if (states.currentNode != states.prevNode)
                    {
                        states.prevNode = states.currentNode;
                        sm.PathfinderCall(states.currentNode);
                    }
                }

            }
        }
	}
}
