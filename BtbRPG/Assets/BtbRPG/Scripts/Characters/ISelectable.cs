using btbrpg.grid;
using btbrpg.holders;

namespace btbrpg.characters
{
	public interface ISelectable : IDetectable
	{
		void OnSelect(PlayerHolder player);
        void OnDeselect(PlayerHolder player);

        void OnHighlight(PlayerHolder player);
        void OnDeHighlight(PlayerHolder player);        
	}
}
