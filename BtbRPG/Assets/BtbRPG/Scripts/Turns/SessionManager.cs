using UnityEngine;
using System.Collections.Generic;

using btbrpg.characters;
using btbrpg.fsn;
using btbrpg.grid;
using btbrpg.pathfinder;
using btbrpg.so.variables;

namespace btbrpg.turns
{
    public class SessionManager : MonoBehaviour
    {
        private const float LIFT_PATH_VIZ = .05f;
        int turnIndex;
        public Turn[] turns;

        public Transform gridObject;

        public GridManager gridManager;

        private float delta;
        public float Delta
        {
            get
            {
                return delta;
            }
        }

        public VariablesHolder gameVariables;

        bool isInit;
        bool isPathfinding;

        public LineRenderer pathViz;

        #region Init
        private void Start()
        {
            gridManager.Init();
            PlaceUnits();
            InitStateManagers();
            isInit = true;
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

        private void InitStateManagers()
        {
            foreach (Turn t in turns)
            {
                t.player.Init();
            }
        }
        #endregion

        #region pathfinder calls
        public void PathfinderCall(GridCharacter character, Node targetNode)
        {
            if (!isPathfinding)
            {
                isPathfinding = true;

                Node start = character.currentNode;
                Node target = targetNode;

                if (start != null && target != null)
                {
                    PathfinderMaster.singleton.RequestPathfind(character,
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

            pathViz.positionCount = p.Count + 1; // +1 see below: one position added
            List<Vector3> allPositions = new List<Vector3>();
            Vector3 pathVizOffset = Vector3.up * LIFT_PATH_VIZ;

            allPositions.Add(c.currentNode.worldPosition + pathVizOffset); // ...one position added
            for (int i = 0; i < p.Count; i++)
            {
                allPositions.Add(p[i].worldPosition + pathVizOffset);
            }

            c.SetCurrentPath(p);
            pathViz.SetPositions(allPositions.ToArray());
        }

        public void ClearPath(StateManager states)
        {
            pathViz.positionCount = 0;
            if (states.CurrentCharacter != null)
            {
                states.CurrentCharacter.currentPath = null;
            }
        }
        #endregion

        #region turn management
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
        #endregion

        #region Events
        public IntVariable stanceInt;

        public void SetStanceForCurrentPlayer()
        {
            if (turns[turnIndex].player.stateManager.CurrentCharacter == null)
                return;

            switch (stanceInt.value)
            {
                case 0:
                    turns[turnIndex].player.stateManager.CurrentCharacter.SetNormal();
                    break;
                case 1:
                    turns[turnIndex].player.stateManager.CurrentCharacter.SetCrouch();
                    break;
                case 2:
                    turns[turnIndex].player.stateManager.CurrentCharacter.SetRun();
                    break;
                case 3:
                    turns[turnIndex].player.stateManager.CurrentCharacter.SetProne();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}

