using UnityEngine;

namespace btbrpg.so.variables
{
    [CreateAssetMenu(menuName = "BtbRPG/Variables/Sprite")]
    public class SpriteVariable : ScriptableObject
    {
        public Sprite value;

        public void Set(Sprite v)
        {
            value = v;
        }

        public void Set(SpriteVariable v)
        {
            value = v.value;
        }
    }
}
