using UnityEngine;
using System.Collections.Generic;

using btbrpg.characters;

namespace btbrpg.holders
{
    [CreateAssetMenu(menuName = "BtbRPG/Player/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public List<GridCharacter> characters = new List<GridCharacter>();

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
