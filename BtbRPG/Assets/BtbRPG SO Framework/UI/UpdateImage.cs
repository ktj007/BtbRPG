using UnityEngine.UI;

using btbrpg.so.variables;

namespace btbrpg.so.ui
{
    public class UpdateImage : UIPropertyUpdater
    {
        public SpriteVariable spriteVariable;
        public Image targetImage;

        /// <summary>
        /// Update the sprite of an Image UI element based on what you've set on the sprite variable
        /// </summary>
        public override void Raise()
        {
            targetImage.sprite = spriteVariable.value;
        }
    }
}
