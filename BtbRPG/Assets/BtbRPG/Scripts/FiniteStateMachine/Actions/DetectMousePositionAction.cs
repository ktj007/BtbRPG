using UnityEngine;

using btbrpg.grid;
using btbrpg.turns;

namespace btbrpg.fsn
{
	public class DetectMousePositionAction : StateAction
	{
		public override void Execute(StateManager states, SessionManager sm, Turn t)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000))
			{
				Node n = sm.gridManager.GetNode(hit.point);
				states.currentNode = n;
				if (n != null)
				{
					Debug.Log("node found");
				}
				
			}
		}
	}
}
