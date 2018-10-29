using UnityEngine;

using btbrpg.characters;

namespace btbrpg.grid
{
	public class Node 
	{
		public int x;
		public int y;
		public int z;

		public bool isWalkable;
		public Vector3 worldPosition;

		public GridObject obstacle;
        public GameObject tileVisualization;

        public GridCharacter character;
    }
}
