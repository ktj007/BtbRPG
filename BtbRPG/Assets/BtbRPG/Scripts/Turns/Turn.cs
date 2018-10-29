using UnityEngine;

using btbrpg.holders;

namespace btbrpg.turns
{
    [CreateAssetMenu(menuName = "BtbRPG/Turns/Turn")]
    public class Turn : ScriptableObject
    {
        public PlayerHolder player;
        int index;
        public Phase[] phases;

        public bool Execute(SessionManager sm)
        {
            bool result = false;

            phases[index].OnStartPhase(sm);

            if (phases[index].IsComplete(sm))
            {
                phases[index].OnEndPhase(sm);
                index++;
                if (index > phases.Length - 1)
                {
                    index = 0;
                    result = true;
                }
            }

            return result;
        }

        public void EndCurrentPhase()
        {
            phases[index].forceExit = true;
        }

    }
}

