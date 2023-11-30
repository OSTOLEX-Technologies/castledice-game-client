using UnityEngine;

namespace Src.GameplayView
{
    public interface IInstantiator
    {
        T Instantiate<T>(T prefab) where T: Object;
    }
}