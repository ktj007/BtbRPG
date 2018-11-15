using UnityEngine;

using SO;

namespace btbrpg.fsn
{
	public class PlayerStateManager : StateManager
	{
		public override void Init()
		{
            VariablesHolder gameVars = Resources.Load("GameVariables") as VariablesHolder;

            State interactionsState = new State();
            interactionsState.actions.Add(new InputAction(gameVars));
            interactionsState.actions.Add(new HandleMouseAction());
            interactionsState.actions.Add(new MoveCameraAction(gameVars));
            
            State waitState = new State();
            // Empty State. No StateActions added here so far...

            State moveOnPathState = new State();
            moveOnPathState.actions.Add(new MoveCharacterOnPathAction());

            startingState = interactionsState;
			currentState = interactionsState;

            allStates.Add("moveOnPath", moveOnPathState);
            allStates.Add("interactions", interactionsState);
			allStates.Add("wait", waitState);
            
        }
	}
}
