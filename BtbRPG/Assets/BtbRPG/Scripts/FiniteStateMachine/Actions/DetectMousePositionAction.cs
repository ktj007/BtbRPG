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

                Node n = sm.gridManager.GetNode(hit.point);
                if (n != null)
                {
                    states.currentNode = n;

                    if (states.currentNode != null)
                    {
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
}
