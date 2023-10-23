using System;
using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.Cells
{
    public class UnityCellsGeneratorsFactory : MonoBehaviour, ICellsGeneratorsFactory
    {
        private UnitySquareCellsGenerator3D _unitySquareCellsGenerator3D;

        public void Init(UnitySquareCellsGenerator3D squareCellsGenerator)
        {
            _unitySquareCellsGenerator3D = squareCellsGenerator;
        }
        
        public ICellsGenerator GetGenerator(CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Square:
                    return _unitySquareCellsGenerator3D;
                default:
                    throw new ArgumentException("Unfamiliar cell type: " + nameof(cellType));
            }
        }
    }
}
