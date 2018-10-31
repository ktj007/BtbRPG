using UnityEngine;

using btbrpg.grid;
using btbrpg.holders;

namespace btbrpg.characters
{
	public class GridCharacter : MonoBehaviour
	{
        public PlayerHolder owner;
        public Node currentNode;

        public void Init()
        {
            owner.RegisterCharacter(this);
        }
    }
}
