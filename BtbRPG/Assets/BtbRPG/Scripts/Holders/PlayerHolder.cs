using System;
using System.Collections.Generic;
using UnityEngine;

using btbrpg.characters;
using btbrpg.fsn;


namespace btbrpg.holders
{
    [CreateAssetMenu(menuName = "BtbRPG/Player/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        [NonSerialized] public StateManager stateManager;
        [NonSerialized] private GameObject stateManagerObject;

        [SerializeField] private GameObject stateManagerPrefab;

        public List<GridCharacter> characters = new List<GridCharacter>();

        public void Init()
        {
            stateManagerObject = Instantiate(stateManagerPrefab) as GameObject;
            stateManager = stateManagerObject.GetComponent<StateManager>();
        }

        public void RegisterCharacter(GridCharacter c)
        {
            if (characters.Contains(c) == false)
                characters.Add(c);
        }

        public void UnRegisterCharacter(GridCharacter c)
        {
            if (characters.Contains(c))
            {
                characters.Remove(c);
            }
        }
    }
}
