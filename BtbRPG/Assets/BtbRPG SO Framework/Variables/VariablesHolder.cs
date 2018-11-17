using UnityEngine;

namespace btbrpg.so.variables
{
	[CreateAssetMenu(menuName = "BtbRPG/Variables/Game Variables Holder")]
	public class VariablesHolder : ScriptableObject
	{
		public float cameraMoveSpeed = 15;

		[Header("Scriptable Variables")]
		#region Scriptables
		public TransformVariable cameraTransform;
		public FloatVariable horizontalInput;
		public FloatVariable verticalInput;
		#endregion
	}
}