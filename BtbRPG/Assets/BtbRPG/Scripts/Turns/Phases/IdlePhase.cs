using UnityEngine;
using System.Collections;

namespace btbrpg.turns
{
	[CreateAssetMenu(menuName = "BtbRPG/Turns/Phases/Idle Phase")]
	public class IdlePhase : Phase
	{
		public override bool IsComplete(SessionManager sm)
		{
			return false;
		}

		public override void OnStartPhase(SessionManager sm)
		{
			if (isInit)
				return;
			isInit = true;

			Debug.Log("Idle Phase started");
		}
		public override void OnEndPhase(SessionManager sm)
		{

		}
	}
}
