using System.Collections.Generic;
using UnityEngine;

namespace Src.General
{
    [CreateAssetMenu(menuName = "Configs/StringsListConfig", fileName = "StringsListConfig", order = 0)]
    public class StringsListConfig : ScriptableObject
    {
        [SerializeField] private List<string> strings;
        
        public List<string> Strings => strings;
    }
}