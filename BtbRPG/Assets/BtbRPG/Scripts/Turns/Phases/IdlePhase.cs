using UnityEngine;
using System.Collections;

namespace btbrpg.turns
{
	[CreateAssetMenu(menuName = "BtbRPG/Turns/Phases/Idle Phase")]
	public class IdlePhase : Phase
	{
		public override bool IsComplete(SessionManager sm, Turn turn)
		{
			return false;
		}

		public override void OnStartPhase(SessionManager sm, Turn turn)
		{
			if (isInit)
				return;
			isInit = true;

			Debug.Log("Idle Phase started");
		}

		public override void OnEndPhase(SessionManager sm, Turn turn)
		{

		}
	}
}
