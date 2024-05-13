using System.Collections.Generic;
using UnityEngine;

namespace Src.Tutorial.HintPointer
{
    public class HintPointerPool : MonoBehaviour, IHintPointerPool
    {
        [SerializeField] private HintPointer hintPointerPrefab;
        [SerializeField] private int initialPoolAmount;
        
        private readonly Queue<HintPointer> _pool = new();
        private readonly List<HintPointer> _occupied = new();

        private void Start()
        {
            for (var i = 0; i < initialPoolAmount; i++)
            {
                SpawnPointer();
            }
        }

        private void SpawnPointer()
        {
            var pointer = Instantiate(hintPointerPrefab, transform, true);
            pointer.gameObject.SetActive(false);
            _pool.Enqueue(pointer);
        }

        public HintPointer Obtain()
        {
            if (_pool.TryDequeue(out var pointer))
            {
                _occupied.Add(pointer);
                pointer.gameObject.SetActive(true);
                return pointer;
            }
            
            SpawnPointer();
            return Obtain();
        }

        public void Reclaim(HintPointer pointer)
        {
            if (!_occupied.Contains(pointer))
            {
                Debug.LogError("Reclaim attempt of unregistered pointer");
            }
            
            pointer.gameObject.SetActive(false);
            _occupied.Remove(pointer);
            _pool.Enqueue(pointer);
        }
    }
}