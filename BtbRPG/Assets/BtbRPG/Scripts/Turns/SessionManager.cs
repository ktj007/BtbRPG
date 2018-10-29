using UnityEngine;

using btbrpg.grid;

namespace btbrpg.turns
{
    public class SessionManager : MonoBehaviour
    {
        int turnIndex;
        public Turn[] turns;

        public Transform gridObject;

        public GridManager gridManager;

        bool isInit;

        private void Start()
        {
            gridManager.Init();
            isInit = true;
        }

        private void Update()
        {
            if (!isInit)
                return;

            if (turns[turnIndex].Execute(this))
            {
                turnIndex++;
                if (turnIndex > turns.Length - 1)
                {
                    turnIndex = 0;
                }
            }
        }
    }
}

