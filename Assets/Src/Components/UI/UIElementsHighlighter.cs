using System.Collections.Generic;
using Src.Components.UI;
using UnityEngine;

public sealed class UIElementsHighlighter : MonoBehaviour
{
    [SerializeField] private AppearingImage shadow;
    [SerializeField] private List<RectTransform> elementsToHighlight;
    private Transform[] _elementsOriginalParents;
    
    private void Start()
    {
        _elementsOriginalParents = new Transform[elementsToHighlight.Count];
        for (int i = 0; i < elementsToHighlight.Count; i++)
        {
            var element = elementsToHighlight[i];
            _elementsOriginalParents[i] = element.parent;
        }
    }
    
    [ContextMenu("Highlight Element")]
    public void HighlightElements()
    {
        foreach (var element in elementsToHighlight)
        {
            element.SetParent(shadow.transform, true);
        }
        shadow.Appear();
    }
    

    
    [ContextMenu("Unhighlight Element")]
    public void UnhighlightElement()
    {
        for (int i = 0; i < elementsToHighlight.Count; i++)
        {
            elementsToHighlight[i].SetParent(_elementsOriginalParents[i], true);
        }
        shadow.Disappear();
    }
}
