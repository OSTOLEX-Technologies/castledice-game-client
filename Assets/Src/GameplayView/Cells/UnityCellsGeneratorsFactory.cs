using System;
using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.Cells
{
    public class UnityCellsGeneratorsFactory : MonoBehaviour, ICellsGeneratorsFactory
    {
        [SerializeField] private SquareCellsGenerator3D squareCellsGenerator3D;
        
        public ICellsGenerator GetGenerator(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Square:
                    return squareCellsGenerator3D;
                default:
                    throw new ArgumentException("Unfamiliar cell type: " + nameof(cellType));
            }
        }
    }
}
