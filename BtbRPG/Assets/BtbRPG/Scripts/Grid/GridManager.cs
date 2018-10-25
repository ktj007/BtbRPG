using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace btbrpg.grid
{
	public class GridManager : MonoBehaviour
	{

		#region Variables
		private Node[,,] grid;

        [SerializeField] private float xzScale = 1.5f;
		[SerializeField] private float yScale = 2;

        private Vector3 minPos;

        private int maxX;
        private int maxZ;
        private int maxY;

        private int dimensionX;
        private int dimensionZ;
        private int dimensionY;


        List<Vector3> nodeVisualization = new List<Vector3>();
		Vector3 nodeExtents = new Vector3(.8f, .8f, .8f);

		#endregion

		private void Start()
		{
			ReadLevel();
            CreateGrid();
        }

		void ReadLevel()
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

		void CreateGrid()
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

                        Vector3 tp = minPos;
                        tp.x += x * xzScale;// + .5f;
                        tp.z += z * xzScale;// + .5f;
                        tp.y += y * yScale;

                        n.worldPosition = tp;

                        Collider[] overlapNode = Physics.OverlapBox(tp, nodeExtents / 2, Quaternion.identity);

                        if (overlapNode.Length > 0)
                        {
                            bool isWalkable = false;

                            for (int i = 0; i < overlapNode.Length; i++)
                            {
                                GridObject obj = overlapNode[i].transform.GetComponentInChildren<GridObject>();
                                if (obj != null)
                                {
                                    if (obj.isWalkable && n.obstacle == null)
                                    {
                                        isWalkable = true;
                                    }
                                    else
                                    {
                                        isWalkable = false;
                                        n.obstacle = obj;
                                    }
                                }
                            }

                            n.isWalkable = isWalkable;
                        }

                        if (n.isWalkable) //  && y > 0) this would only visualize floors above floor zero
                        {
                            nodeVisualization.Add(n.worldPosition);
                        }

                        grid[x, y, z] = n;
                    }
                }
            }
        }

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			for (int i = 0; i < nodeVisualization.Count; i++)
			{
				Gizmos.DrawWireCube(nodeVisualization[i], nodeExtents);
			}
		}

	}
}
