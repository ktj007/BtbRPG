using UnityEngine;

using btbrpg.characters;
using btbrpg.grid;
using btbrpg.turns;


namespace SA
{
	[CreateAssetMenu(menuName = "BtbRPG/Turns/Phases/Place Units Phase")]
	public class PlaceUnitsPhase : Phase
	{
		public override bool IsComplete(SessionManager sm)
		{
			return true;
		}

		public override void OnStartPhase(SessionManager sm)
		{
			if (isInit)
				return;
			isInit = true;

			PlaceUnitsOnGrid(sm);
		}

		public override void OnEndPhase(SessionManager sm)
		{

		}

		void PlaceUnitsOnGrid(SessionManager sm)
		{
			GridCharacter[] units = sm.gridObject.GetComponentsInChildren<GridCharacter>();

			foreach (GridCharacter u in units)
			{
				Node n = sm.gridManager.GetNode(u.transform.position);
				if (n != null)
				{
					u.transform.position = n.worldPosition;
				}
			}
		}
	}
}
