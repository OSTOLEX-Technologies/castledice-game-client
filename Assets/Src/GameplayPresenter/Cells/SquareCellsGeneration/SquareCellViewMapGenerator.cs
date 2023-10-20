using castledice_game_data_logic;

namespace Src.GameplayPresenter.Cells.SquareCellsGeneration
{
    public class SquareCellViewMapGenerator : ICellViewMapProvider
    {
        private ISquareCellAssetIdProvider _assetIdProvider;

        public SquareCellViewMapGenerator(ISquareCellAssetIdProvider assetIdProvider)
        {
            _assetIdProvider = assetIdProvider;
        }

        public CellViewData[,] GetCellViewMap(GameStartData gameStartData)
        {
            throw new System.NotImplementedException();
        }
    }
}