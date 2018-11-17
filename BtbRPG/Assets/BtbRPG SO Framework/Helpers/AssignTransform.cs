using UnityEngine;

using btbrpg.so.variables;


namespace btbrpg.so.helper
{
    public class AssignTransform : MonoBehaviour
    {
        public TransformVariable transformVariable;

		private void OnEnable()
		{
			transformVariable.value = this.transform;
			Destroy(this);
		}

	}
}
