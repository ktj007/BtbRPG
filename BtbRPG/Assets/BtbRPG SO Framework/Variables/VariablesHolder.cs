using btbrpg.so.events;
using UnityEngine;

namespace btbrpg.so.variables
{
	[CreateAssetMenu(menuName = "BtbRPG/Variables/Game Variables Holder")]
	public class VariablesHolder : ScriptableObject
	{
		public float cameraMoveSpeed = 15;

        [Header("Game Events")]
        public GameEvent updateActionPoints;

        [Header("Scriptable Variables")]
        #region Scriptables
        public StringVariable actionPointsText;
		public TransformVariable cameraTransform;
		public FloatVariable horizontalInput;
		public FloatVariable verticalInput;
        #endregion

        #region Methods
        public void UpdateActionPoints(int ap)
        {
            actionPointsText.value = ap.ToString();
            updateActionPoints.Raise();
        }
        #endregion
    }
}