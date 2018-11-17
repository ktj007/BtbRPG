using UnityEngine.UI;

using btbrpg.so.variables;

namespace btbrpg.so.ui
{
    public class UpdateButtonInteractability : UIPropertyUpdater
    {
        public BoolVariable targetBool;
        public Button targetButton;

        public override void Raise()
        {
            targetButton.interactable = targetBool.value;
        }
    }
}
