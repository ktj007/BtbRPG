using UnityEngine;

using btbrpg.grid;
using btbrpg.turns;
using btbrpg.characters;

namespace btbrpg.fsn
{
	public class HandleMouseAction : StateAction
	{
        private const int MAX_DISTANCE = 1000;

        GridCharacter prevCharacter;

        public override void Execute(StateManager states, SessionManager sm, Turn t)
        {
            bool mouseClick = Input.GetMouseButtonDown(0);

            if (prevCharacter != null)
                prevCharacter.OnDeHighlight(states.playerHolder);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, MAX_DISTANCE))
            {
                Node n = sm.gridManager.GetNode(hit.point);
                IDetectable detectable = hit.transform.GetComponent<IDetectable>();
                if (detectable != null)
                {
                    n = detectable.OnDetect();
                }

                if (n != null)
                {
                    if (n.character != null)
                    {
                        // highlight own character
                        if (n.character.owner == states.playerHolder)
                        {
                            n.character.OnHighlight(states.playerHolder);
                            prevCharacter = n.character;
                        }
                        else // highlight an enemy character
                        {
                            // TODO: Attack!
                        }
                    }

                    if (states.CurrentCharacter != null && n.character == null)
                    {
                        PathDetection(states, sm, n);
                    }
                    else // no character selected
                    {
                        if (mouseClick)
                        {
                            if (n.character.owner == states.playerHolder)
                            {
                                n.character.OnSelect(states.playerHolder);
                                states.prevNode = null;
                                sm.ClearPath();
                            }
                        }
                    }
                }
            }
        }

        void PathDetection(StateManager states, SessionManager sm, Node n)
        {
            states.currentNode = n;

            if (states.currentNode != null)
            {
                if (states.currentNode != states.prevNode || states.prevNode == null)
                {
                    states.prevNode = states.currentNode;
                    sm.PathfinderCall(states.CurrentCharacter, states.currentNode);
                }
            }
        }
    }
}
