using UnityEngine.UI;

using btbrpg.so.variables;

namespace btbrpg.so.ui
{
    public class UpdateToggle : UIPropertyUpdater
    {
        public BoolVariable boolVariable;
        public Toggle targetToggle;

        /// <summary>
        /// Use this to set the state of a toggle based on a bool variable
        /// </summary>
        public override void Raise()
        {
            targetToggle.isOn = boolVariable.value;
        }
    }
}
