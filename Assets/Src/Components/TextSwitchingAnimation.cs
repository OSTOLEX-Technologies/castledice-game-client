using Src.General;
using TMPro;
using UnityEngine;

namespace Src.Components
{
    /// <summary>
    /// This component changes text on the given TextMeshProUGUI component with a given interval.
    /// Text is being changed sequentially from the list of strings.
    /// </summary>
    public class TextSwitchingAnimation : MonoBehaviour
    {
       [SerializeField] private TextMeshProUGUI textMesh;
       [SerializeField] private StringsListConfig stringsListConfig;
       [SerializeField] private FloatValueConfig intervalConfig;
       private float _elapsedTime = 0;
       private int _currentStringIndex = 0;
       
       private void Update()
       {
              _elapsedTime += Time.deltaTime;
              if (_elapsedTime < intervalConfig.Value) return;
              _elapsedTime = 0;
              textMesh.text = stringsListConfig.Strings[_currentStringIndex];
              _currentStringIndex = (_currentStringIndex + 1) % stringsListConfig.Strings.Count;
       }
    }
}
