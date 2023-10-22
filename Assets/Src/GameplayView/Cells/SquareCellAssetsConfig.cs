using System;
using System.Collections.Generic;
using Src.GameplayPresenter.Cells;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using UnityEngine;

namespace Src.GameplayView.Cells
{
    [CreateAssetMenu(fileName = "SquareCellAssetsConfig", menuName = "Configs/SquareCellAssetsConfig", order = 0)]
    public class SquareCellAssetsConfig : ScriptableObject, ISquareCellAssetIdProvider
    {
        [SerializeField] private List<GameObject> upperLeftCornerCells = new();
        [SerializeField] private List<GameObject> upperRightCornerCells = new();
        [SerializeField] private List<GameObject> lowerLeftCornerCells = new();
        [SerializeField] private List<GameObject> lowerRightCornerCells = new();
        [SerializeField] private List<GameObject> upperBorderCells = new();
        [SerializeField] private List<GameObject> lowerBorderCells = new();
        [SerializeField] private List<GameObject> leftBorderCells = new();
        [SerializeField] private List<GameObject> rightBorderCells = new();
        [SerializeField] private List<GameObject> centerCells = new();

        private List<GameObject> _allPrefabs = new();

        private static readonly SquareCellData UpperLeftCorner = new(VerticalOrientationType.Upper,
            HorizontalOrientationType.Left, SquareCellBorderType.Corner);
        private static readonly SquareCellData UpperRightCorner = new(VerticalOrientationType.Upper,
            HorizontalOrientationType.Right, SquareCellBorderType.Corner);
        private static readonly SquareCellData LowerLeftCorner = new(VerticalOrientationType.Lower,
            HorizontalOrientationType.Left, SquareCellBorderType.Corner);
        private static readonly SquareCellData LowerRightCorner = new(VerticalOrientationType.Lower,
            HorizontalOrientationType.Right, SquareCellBorderType.Corner);
        private static readonly SquareCellData UpperBorder = new(VerticalOrientationType.Upper,
            HorizontalOrientationType.Middle, SquareCellBorderType.OneBorder);
        private static readonly SquareCellData LowerBorder = new(VerticalOrientationType.Lower,
            HorizontalOrientationType.Middle, SquareCellBorderType.OneBorder);
        private static readonly SquareCellData LeftBorder = new(VerticalOrientationType.Middle,
            HorizontalOrientationType.Left, SquareCellBorderType.OneBorder);
        private static readonly SquareCellData RightBorder = new(VerticalOrientationType.Middle,
            HorizontalOrientationType.Right, SquareCellBorderType.OneBorder);
        private static readonly SquareCellData Center = new(VerticalOrientationType.Middle,
            HorizontalOrientationType.Middle, SquareCellBorderType.None);
        

        private void OnValidate()
        {
            _allPrefabs.Clear();
            foreach (var prefab in upperBorderCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in lowerBorderCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in leftBorderCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in rightBorderCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in upperLeftCornerCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in upperRightCornerCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in lowerLeftCornerCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in lowerRightCornerCells)
            {
                _allPrefabs.Add(prefab);
            }
            foreach (var prefab in centerCells)
            {
                _allPrefabs.Add(prefab);
            }
        }

        public List<int> GetAssetIds(SquareCellData data)
        {
            var prefabs = GetPrefabListForSquareCellData(data);
            var ids = new List<int>();
            foreach (var prefab in prefabs)
            {
                ids.Add(_allPrefabs.IndexOf(prefab));
            }
            return ids;
        }

        private List<GameObject> GetPrefabListForSquareCellData(SquareCellData data)
        {
            if (data.Equals(UpperBorder))
            {
                return upperBorderCells;
            }
            if (data.Equals(LowerBorder))
            {
                return lowerBorderCells;
            }
            if (data.Equals(LeftBorder))
            {
                return leftBorderCells;
            }
            if (data.Equals(RightBorder))
            {
                return rightBorderCells;
            }
            if (data.Equals(UpperLeftCorner))
            {
                return upperLeftCornerCells;
            }
            if (data.Equals(UpperRightCorner))
            {
                return upperRightCornerCells;
            }
            if (data.Equals(LowerLeftCorner))
            {
                return lowerLeftCornerCells;
            }
            if (data.Equals(LowerRightCorner))
            {
                return lowerRightCornerCells;
            }
            if (data.Equals(Center))
            {
                return centerCells;
            }

            throw new ArgumentException("Unknown square cell data " + data);
        }

        public GameObject GetAssetPrefab(int id)
        {
            return _allPrefabs[id];
        }
    }
}