using castledice_game_logic.GameObjects;
using UnityEngine;

namespace Src.GameplayView.CellsContent
{
    public abstract class ContentView : MonoBehaviour
    {
        protected GameObject Model;
        
        public abstract void StartView();
        public abstract void UpdateView();
        public abstract void DestroyView();

        public abstract Content Content{ get; }

        /// <summary>
        /// This method should be called in the beginning of Init method of each child class.
        /// </summary>
        /// <param name="model"></param>
        protected void Init(GameObject model)
        {
            Model = model;
            Model.transform.SetParent(transform);
            Model.transform.localPosition = Vector3.zero;
        }
    }
}