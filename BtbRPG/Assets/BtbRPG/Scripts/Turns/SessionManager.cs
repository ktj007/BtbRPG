using UnityEngine;
using System.Collections.Generic;

using btbrpg.characters;
using btbrpg.grid;
using btbrpg.pathfinder;


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
        bool isPathfinding;

        public LineRenderer pathViz;


        public void PathfinderCall(Node targetNode)
        {
            if (!isPathfinding)
            {
                isPathfinding = true;

                Node start = new Node(); // turns[0].player.characters[0].currentNode;
                start.x = 3;
                start.z = 3;

                Node target = targetNode;

                Debug.Log("Start is: " + start + ", target is: " + target);
                if (start != null && target != null)
                {
                    PathfinderMaster.singleton.RequestPathfind(turns[0].player.characters[0],
                        start, target, PathfinderCallback, gridManager);
                }
                else
                {
                    isPathfinding = false;
                }
            }
        }

        void PathfinderCallback(List<Node> p, GridCharacter c)
        {
            isPathfinding = false;
            if (p == null)
            {
                return;
            }

            pathViz.positionCount = p.Count;
            List<Vector3> allPositions = new List<Vector3>();
            for (int i = 0; i < p.Count; i++)
            {
                allPositions.Add(p[i].worldPosition + Vector3.up * .1f);
            }

            pathViz.SetPositions(allPositions.ToArray());
        }

        private void Start()
        {
            gridManager.Init();
            PlaceUnits();
            InitStateManagers();
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
                u.Init();

                Node n = gridManager.GetNode(u.transform.position);
                if (n != null)
                {
                    u.transform.position = n.worldPosition;
                    n.character = u;
                    u.currentNode = n;
                }
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

