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
                Node node = sm.gridManager.GetNode(hit.point);
                IDetectable detectable = hit.transform.GetComponent<IDetectable>();
                if (detectable != null)
                {
                    node = detectable.OnDetect();
                }

                if (node != null)
                {
                    if (node.character != null)
                    {
                        //you highlighted your own unit
                        if (node.character.owner == states.playerHolder)
                        {
                            node.character.OnHighlight(states.playerHolder);
                            prevCharacter = node.character;
                            sm.ClearPath(states);
                            sm.gameVariables.UpdateActionPoints(node.character.actionPoints);
                        }
                        else //you highlighted an enemy unit
                        {

                        }
                    }

                    if (states.CurrentCharacter != null && node.character == null)
                    {
                        if (mouseClick)
                        {
                            if (states.CurrentCharacter.currentPath != null || states.CurrentCharacter.currentPath.Count > 0)
                            {
                                states.SetState("moveOnPath");
                            }
                        }
                        else
                        {
                            PathDetection(states, sm, node);
                        }
                    }
                    else //No character selected
                    {
                        if (mouseClick)
                        {
                            if (node.character != null)
                            {
                                if (node.character.owner == states.playerHolder)
                                {
                                    node.character.OnSelect(states.playerHolder);
                                    states.prevNode = null;
                                    sm.ClearPath(states);

                                    sm.gameVariables.UpdateActionPoints(node.character.actionPoints);
                                }
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
