using UnityEngine;

namespace btbrpg.turns
{
    [CreateAssetMenu(menuName = "BtbRPG/Turns/Phases/States Tick Phase")]
    public class StatesTickPhase : Phase
	{
		public override bool IsComplete(SessionManager sm, Turn turn)
		{
			turn.player.stateManager.Tick(sm, turn);
			return false;
		}

		public override void OnStartPhase(SessionManager sm, Turn turn)
		{
		    // do nothing
		}
	}
}
