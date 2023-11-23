using UnityEngine;

namespace Src.GameplayView
{
    public interface IInstantiator
    {
        GameObject Instantiate(GameObject prefab);
    }
}