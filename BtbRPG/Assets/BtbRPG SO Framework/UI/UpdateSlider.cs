﻿using UnityEngine.UI;

using btbrpg.so.variables;

namespace btbrpg.so.ui
{
    public class UpdateSlider : UIPropertyUpdater
    {
        public NumberVariable targetVariable;
        public Slider targetSlider;

        public override void Raise()
        {
            if(targetVariable is FloatVariable)
            {
                FloatVariable f = (FloatVariable)targetVariable;
                targetSlider.value = f.value;
                return;
            }

            if(targetVariable is IntVariable)
            {
                IntVariable i = (IntVariable)targetVariable;
                targetSlider.value = i.value;
            }
        }
    }
}
