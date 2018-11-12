using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace btbrpg.grid
{
    public class GridManager : MonoBehaviour
    {

        #region Variables
        private Node[,,] grid;
        private GameObject tileContainer;

        [Header("Grid Scaling")]
        [SerializeField] private float xzScale = 1.5f;
        [SerializeField] private float yScale = 2;

        // Grid dimensions
        private Vector3 minPos; // starting point
        private int dimensionX; // dimension x
        private int dimensionZ; // dimension z
        private int dimensionY; // dimension y

        [Header("Node / Collider Visualisation")]
        [SerializeField] private bool visualizeCollisions;
        [SerializeField] private GameObject tileVisualization;
        private List<Vector3> nodeVisualization = new List<Vector3>();


        // As vertical grid collider (see collisonDetectionExtentY) may be less than 1f use raycast to detect floor
        [Header("Grid Detection Raycast Attributes")]
        [SerializeField] float rayDownOrigin = .7f; // start of ray, added to node's y world position
        [SerializeField] float rayDownDist = 1.3f; // distance of ray
        

        // Size of the collider boxes which will (on collision with GridObject's collider) determine, 
        // whether there is to be a grid node. 
        [Header("Obstacle Detection Collider Extents")]
        [Range(.2f, 1f)] [SerializeField] private float collisonExtentX = .8f;
        [Range(.2f, 1f)] [SerializeField] private float collisonExtentY = .8f;
        [Range(.2f, 1f)] [SerializeField] private float collisonExtentZ = .8f;
        [SerializeField] float collisionOffset;

        Vector3 collisionDetectionExtents;
 
        // public GameObject unit;
        #endregion


        public void Init()
        {
            tileContainer = new GameObject();
            tileContainer.name = "Tile Container";

            // needs to be global for usage in OnDrawGizmos()
            collisionDetectionExtents = new Vector3(collisonExtentX, collisonExtentY, collisonExtentZ);

            ReadLevelDimensions();
            CreateGrid();
        }

        void ReadLevelDimensions()
        {
            GridPosition[] gp = GameObject.FindObjectsOfType<GridPosition>();

            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minZ = minX;
            float maxZ = maxX;
            float minY = minX;
            float maxY = maxX;

            for (int i = 0; i < gp.Length; i++)
            {
                Transform t = gp[i].transform;

                #region Read Positions
                if (t.position.x < minX)
                {
                    minX = t.position.x;
                }

                if (t.position.x > maxX)
                {
                    maxX = t.position.x;
                }

                if (t.position.z < minZ)
                {
                    minZ = t.position.z;
                }

                if (t.position.z > maxZ)
                {
                    maxZ = t.position.z;
                }

                if (t.position.y < minY)
                {
                    minY = t.position.y;
                }

                if (t.position.y > maxY)
                {
                    maxY = t.position.y;
                }
                #endregion
            }


            dimensionX = Mathf.FloorToInt((maxX - minX) / xzScale);
            dimensionZ = Mathf.FloorToInt((maxZ - minZ) / xzScale);
            dimensionY = Mathf.FloorToInt((maxY - minY) / yScale);

            if (dimensionY == 0)
            {
                dimensionY = 1;
            }

            minPos = Vector3.zero;
            minPos.x = minX;
            minPos.z = minZ;
            minPos.y = minY;

        }

        private void CreateGrid()
        {
            grid = new Node[dimensionX, dimensionY, dimensionZ];

            for (int y = 0; y < dimensionY; y++)
            {
                for (int x = 0; x < dimensionX; x++)
                {
                    for (int z = 0; z < dimensionZ; z++)
                    {
                        Node n = new Node();
                        n.x = x;
                        n.z = z;
                        n.y = y;

                        Vector3 nodeWorldPosition = minPos;
                        nodeWorldPosition.x += x * xzScale; // + .5f;
                        nodeWorldPosition.z += z * xzScale; // + .5f;
                        nodeWorldPosition.y += y * yScale;

                        n.worldPosition = nodeWorldPosition;
                        DetectWalkableNodesByRaycast(n);

                        Vector3 collisionPosition = DetectObstaclesByCollision(n);
                        BuildWalkableNodeVisualisation(n, collisionPosition);

                        grid[x, y, z] = n;
                    }
                }
            }
        }

        private void DetectWalkableNodesByRaycast(Node n)
        {
            RaycastHit hit;
            Vector3 origin = n.worldPosition;
            origin.y += rayDownOrigin;

            Debug.DrawRay(origin, Vector3.down * rayDownDist, Color.red, 3);
            if (Physics.Raycast(origin, Vector3.down, out hit, rayDownDist))
            {
                GridObject gridObject = hit.transform.GetComponentInParent<GridObject>();
                if (gridObject != null)
                {
                    if (gridObject.isWalkable)
                    {
                        n.isWalkable = true;
                    }
                }

                n.worldPosition = hit.point;
            }
        }

        private Vector3 DetectObstaclesByCollision(Node n)
        {
            Vector3 collisionPosition = n.worldPosition;
            collisionPosition.y += collisionOffset;

            Collider[] overlapNode = Physics.OverlapBox(collisionPosition, collisionDetectionExtents, Quaternion.identity);
            if (overlapNode.Length > 0)
            {
                for (int i = 0; i < overlapNode.Length; i++)
                {
                    GridObject obj = overlapNode[i].transform.GetComponentInChildren<GridObject>();
                    if (obj != null)
                    {
                        if (obj.isWalkable && n.obstacle == null)
                        {
                            
                        }
                        else
                        {   // this will make nodes unwalkable where there is an obstacle below the node
                            // depending on the collider's extents
                            n.isWalkable = false;
                            n.obstacle = obj;
                        }
                    }
                }
            }

            return collisionPosition;
        }

        private void BuildWalkableNodeVisualisation(Node n, Vector3 collisionPosition)
        {
            if (n.isWalkable)
            {
                GameObject go = Instantiate(tileVisualization, n.worldPosition + Vector3.one * .1f, Quaternion.identity) as GameObject;
                n.tileVisualization = go;
                go.transform.parent = tileContainer.transform;
                go.SetActive(true);
            } 
            else
            {
                // node is not walkable, but there is also no obstacle
                if(n.obstacle == null)
                {
                    n.isAir = true;
                }
            }

            nodeVisualization.Add(collisionPosition);
        }

        public Node GetNode(Vector3 wp)
        {
            Vector3 p = wp - minPos;
            int x = Mathf.RoundToInt(p.x / xzScale);
            int y = Mathf.RoundToInt(p.y / yScale);
            int z = Mathf.RoundToInt(p.z / xzScale);

            return GetNode(x, y, z);
        }

        public Node GetNode(int x, int y, int z)
        {
            if (x < 0 || x > dimensionX - 1 || y < 0 || y > dimensionY - 1 || z < 0 || z > dimensionZ - 1)
            {
                return null;
            }

            return grid[x, y, z];
        }

        private void OnDrawGizmos()
        {
            if (visualizeCollisions)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < nodeVisualization.Count; i++)
                {
                    Gizmos.DrawWireCube(nodeVisualization[i], collisionDetectionExtents);
                }
            }
        }

    }
}
