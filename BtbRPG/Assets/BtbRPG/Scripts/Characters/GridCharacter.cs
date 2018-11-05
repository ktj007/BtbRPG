using System.Collections.Generic;
using UnityEngine;

using btbrpg.grid;
using btbrpg.holders;


namespace btbrpg.characters
{
	public class GridCharacter : MonoBehaviour, ISelectable
	{
        public PlayerHolder owner;
        public Node currentNode;

        public GameObject highlighter;
        private bool isSelected;

        public List<Node> currentPath;

        public void Init()
        {
            owner.RegisterCharacter(this);
            highlighter.SetActive(false);
        }

        public void LoadPath(List<Node> path)
        {
            currentPath = path;
        }

        public void OnSelect(PlayerHolder player)
        {
            highlighter.SetActive(true);
            isSelected = true;
            player.stateManager.CurrentCharacter = this;
        }

        public void OnDeselect(PlayerHolder player)
        {
            isSelected = false;
            highlighter.SetActive(false);

        }

        public void OnHighlight(PlayerHolder player)
        {
            highlighter.SetActive(true);
        }

        public void OnDeHighlight(PlayerHolder player)
        {
            if (!isSelected)
            {
                highlighter.SetActive(false);
            }
        }

        public Node OnDetect()
        {
            return currentNode;
        }
    }
}
