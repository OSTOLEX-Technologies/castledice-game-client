using UnityEngine;

namespace Src.GameplayView.Grid.GridGeneration
{
    public class UnitySquareGridGenerationConfig : ScriptableObject, ISquareGridGenerationConfig
    {
        [SerializeField] private float cellLength;
        [SerializeField] private float cellWidth;
        [SerializeField] private Vector3 startPosition;

        public float CellLength => cellLength;
        public float CellWidth => cellWidth;
        public Vector3 StartPosition => startPosition;
    }
}