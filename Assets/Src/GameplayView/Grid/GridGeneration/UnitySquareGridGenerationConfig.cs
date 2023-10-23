using UnityEngine;

namespace Src.GameplayView.Grid.GridGeneration
{
    [CreateAssetMenu(fileName = "UnitySquareGridGenerationConfig", menuName = "Configs/UnitySquareGridGenerationConfig", order = 1)]
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