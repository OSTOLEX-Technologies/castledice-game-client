using UnityEngine;

namespace Src.GameplayView
{
    public class Instantiator : IInstantiator
    {
        public T Instantiate<T>(T prefab) where T : Object
        {
            return Object.Instantiate(prefab);
        }
    }
}