using UnityEngine;

namespace Src.GameplayView
{
    public class Instantiator : IInstantiator
    {
        public GameObject Instantiate(GameObject prefab)
        {
            return Object.Instantiate(prefab);
        }
    }
}