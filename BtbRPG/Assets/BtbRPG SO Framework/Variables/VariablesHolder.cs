using UnityEngine;

namespace SO
{
	[CreateAssetMenu(menuName = "Variables/Game Variables Holder")]
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