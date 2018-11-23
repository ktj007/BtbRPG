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

        public LineRenderer reachablePathViz;
        public LineRenderer unreachablePathViz;

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

        void PathfinderCallback(List<Node> pathfinderPath, GridCharacter c)
        {
            isPathfinding = false;
            if (pathfinderPath == null)
            {
                return;
            }

            List<Node> currentPath = new List<Node>();
            List<Vector3> reachablePositions = new List<Vector3>();
            List<Vector3> unreachablePositions = new List<Vector3>();
            Vector3 offset = Vector3.up * .1f;

            if (c.actionPoints > 0)
            {
                reachablePositions.Add(c.currentNode.worldPosition + offset);
            }

            if (pathfinderPath.Count > c.actionPoints)
            {
                if (c.actionPoints == 0)
                {
                    unreachablePositions.Add(c.currentNode.worldPosition + offset);
                }
                else
                {
                    unreachablePositions.Add(pathfinderPath[c.actionPoints - 1].worldPosition + offset);
                }
            }

            for (int i = 0; i < pathfinderPath.Count; i++)
            {
                if (i <= c.actionPoints - 1)
                {
                    currentPath.Add(pathfinderPath[i]);
                    reachablePositions.Add(pathfinderPath[i].worldPosition + offset);
                }
                else
                {
                    unreachablePositions.Add(pathfinderPath[i].worldPosition + offset);
                }
            }

            reachablePathViz.positionCount = currentPath.Count + 1;
            reachablePathViz.SetPositions(reachablePositions.ToArray());
            unreachablePathViz.positionCount = unreachablePositions.Count;
            unreachablePathViz.SetPositions(unreachablePositions.ToArray());


            c.SetCurrentPath(currentPath);
        }

        public void ClearPath(StateManager states)
        {
            reachablePathViz.positionCount = 0;
            unreachablePathViz.positionCount = 0;

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

        public void EndTurn()
		{
			turns[turnIndex].EndCurrentPhase();
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

