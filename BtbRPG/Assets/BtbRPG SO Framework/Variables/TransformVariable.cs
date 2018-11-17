using UnityEngine;

namespace btbrpg.so.variables
{
    [CreateAssetMenu(menuName = "BtbRPG/Variables/Transform")]
    public class TransformVariable : ScriptableObject
    {
        public Transform value;

        public void Set(Transform v)
        {
            value = v;
        }

        public void Set(TransformVariable v)
        {
            value = v.value;
        }

        public void Clear()
        {
            value = null;
        }
    }
}
