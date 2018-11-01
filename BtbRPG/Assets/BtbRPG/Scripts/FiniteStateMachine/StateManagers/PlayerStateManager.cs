using UnityEngine;

using btbrpg.so;

namespace btbrpg.fsn
{
	public class PlayerStateManager : StateManager
	{
		public override void Init()
		{
            VariablesHolder gameVars = Resources.Load("GameVariables") as VariablesHolder;

            State interactions = new State();
            interactions.actions.Add(new InputAction(gameVars));
            interactions.actions.Add(new DetectMousePositionAction());
            interactions.actions.Add(new MoveCameraAction(gameVars));
            

            State wait = new State();
			currentState = interactions;

			allStates.Add("interactions", interactions);
			allStates.Add("wait", wait);
		}
	}
}
