using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.Content.Placeable;

namespace Src.GameplayPresenter.Cells.SquareCellsGeneration
{
    public class SquareCellViewMapGenerator : ICellViewMapProvider
    {
        private ISquareCellAssetIdProvider _assetIdProvider;

        public SquareCellViewMapGenerator(ISquareCellAssetIdProvider assetIdProvider)
        {
            _assetIdProvider = assetIdProvider;
        }

        public CellViewData[,] GetCellViewMap(BoardData boardData)
        {
            var squareCellDataMap = SquareCellDataMapGenerator.GetSquareCellDataMap(boardData.CellsPresence);
            var cellViewDataMap = new CellViewData[squareCellDataMap.GetLength(0), squareCellDataMap.GetLength(1)];
            for (int i = 0; i < squareCellDataMap.GetLength(0); i++)
            {
                for (int j = 0; j < squareCellDataMap.GetLength(1); j++)
                {
                    var cellData = squareCellDataMap[i, j];
                    var assetsIdsList = _assetIdProvider.GetAssetIds(cellData);
                    var assetId = GetRandomElement(assetsIdsList);
                    cellViewDataMap[i, j] = new CellViewData(assetId, false);
                }
            }

            return cellViewDataMap;
        }
        
        private T GetRandomElement<T>(List<T> list)
        {
            var rnd = new Random();
            var randomIndex = rnd.Next(0, list.Count);
            return list[randomIndex];
        }
    }
}