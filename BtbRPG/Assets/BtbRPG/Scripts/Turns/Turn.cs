using System;
using UnityEngine;

using btbrpg.holders;

namespace btbrpg.turns
{
    [CreateAssetMenu(menuName = "BtbRPG/Turns/Turn")]
    public class Turn : ScriptableObject
    {
        public PlayerHolder player;

        [NonSerialized] private int index;
        private Phase[] phases;

        public bool Execute(SessionManager sm)
        {
            bool result = false;

            phases[index].OnStartPhase(sm, this);

            if (phases[index].IsComplete(sm, this))
            {
                phases[index].OnEndPhase(sm, this);
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

