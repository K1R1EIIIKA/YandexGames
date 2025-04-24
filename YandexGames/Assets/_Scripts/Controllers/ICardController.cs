using _Scripts.Data;
using _Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine.UI;

namespace _Scripts.Controllers
{
    public interface ICardController
    {
        void Initialize();
        void UpdateProgress(PlayerProgress progress);
        void LoadProgress(PlayerProgress progress);
        void ShowPlayerBeds();
        void ShowAllBeds();
        void SortInventoryCards(InventoryCardsSortType sortType);
        void SortCollectionCards(CollectionSortType sortType);
        ISavedProgress GetSavedProgress();

        void Construct(GridLayoutGroup gridLayoutGroup);
    }
}