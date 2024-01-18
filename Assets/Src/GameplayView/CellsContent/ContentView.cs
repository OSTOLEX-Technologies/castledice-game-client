using castledice_game_logic.GameObjects;
using UnityEngine;

namespace Src.GameplayView.CellsContent
{
    public abstract class ContentView : MonoBehaviour
    {
        public abstract void StartView();
        public abstract void UpdateView();
        public abstract void DestroyView();

        public abstract Content Content { get; }
        
        protected void SetAsChildAndCenter(GameObject target)
        {
            target.transform.SetParent(transform);
            target.transform.localPosition = Vector3.zero;
        }
    }
}