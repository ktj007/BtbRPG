﻿using UnityEngine;

using btbrpg.characters;
using btbrpg.grid;


namespace btbrpg.turns
{
    public class SessionManager : MonoBehaviour
    {
        int turnIndex;
        public Turn[] turns;

        public Transform gridObject;

        public GridManager gridManager;
        public float delta;

        bool isInit;

        private void Start()
        {
            gridManager.Init();
            InitStateManagers();
            PlaceUnits();
            isInit = true;
        }

        private void InitStateManagers()
        {
            foreach (Turn t in turns)
            {
                t.player.Init();
            }
        }

        private void PlaceUnits()
        {
            GridCharacter[] units = GameObject.FindObjectsOfType<GridCharacter>();
            foreach (GridCharacter u in units)
            {
                Node n = gridManager.GetNode(u.transform.position);
                if (n != null)
                {
                    u.transform.position = n.worldPosition;
                    n.character = u;
                    u.currentNode = n;
                }

                u.Init();
            }
        }

        private void Update()
        {
            if (!isInit)
                return;

            delta = Time.deltaTime;

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

