using UnityEngine;
using System.Collections;

namespace btbrpg.fsn
{
	public class PlayerStateManager : StateManager
	{
		public override void Init()
		{
			State interactions = new State();
			interactions.actions.Add(new DetectMousePositionAction());

			State wait = new State();
			currentState = interactions;

			allStates.Add("interactions", interactions);
			allStates.Add("wait", wait);
		}
	}
}
