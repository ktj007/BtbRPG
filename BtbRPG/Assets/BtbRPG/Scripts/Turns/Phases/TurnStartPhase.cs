using UnityEngine;

using btbrpg.characters;

namespace btbrpg.turns
{
    [CreateAssetMenu(menuName = "BtbRPG/Turns/Phases/Turn Start Phase")]
    public class TurnStartPhase : Phase
	{
		public override bool IsComplete(SessionManager sm, Turn turn)
		{
			return true;
		}

		public override void OnStartPhase(SessionManager sm, Turn turn)
		{
			foreach (GridCharacter c in turn.player.characters)
			{
				c.OnStartTurn();

			    if (turn.player.stateManager.CurrentCharacter == c)
			    {
                    sm.gameVariables.UpdateActionPoints(c.actionPoints);
			    } 
			}
		}
	}
}
