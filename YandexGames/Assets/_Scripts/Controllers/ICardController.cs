using _Scripts.Data;
using UnityEngine.UI;

namespace _Scripts.Controllers
{
    public interface ICardController
    {
        void UpdateProgress(PlayerProgress progress);
        void LoadProgress(PlayerProgress progress);
        void ShowPlayerCards();
        void ShowAllCards();

        void Construct(GridLayoutGroup gridLayoutGroup);
    }
}