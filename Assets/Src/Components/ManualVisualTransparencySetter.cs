using System.Collections.Generic;
using Src.GameplayView.ContentVisuals;
using UnityEngine;

public class ManualVisualTransparencySetter : MonoBehaviour
{
    [SerializeField] private List<ContentVisual> _contentVisuals = new();
    
    [ContextMenu("SetTransparency to 0.3")]
    public void SetTransparencyToZeroThree()
    {
        foreach (var contentVisual in _contentVisuals)
        {
            contentVisual.SetTransparency(0.3f);
        }
    }
    
    [ContextMenu("SetTransparency to 1")]
    public void SetTransparencyToFull()
    {
        foreach (var contentVisual in _contentVisuals)
        {
            contentVisual.SetTransparency(1);
        }
    }
}
