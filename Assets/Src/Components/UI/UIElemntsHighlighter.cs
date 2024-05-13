using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Move shadow to the separate object. 
public class UIElemntsHighlighter : MonoBehaviour
{
    [SerializeField] private Image shadow;
    [SerializeField] private float transitionTime = 0.5f;
    [SerializeField] [Range(0, 1)] private float shadowTargetAlpha = 0.5f;
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
        StartCoroutine(ShowShadow());
    }
    
    private IEnumerator ShowShadow()
    {
        var time = 0f;
        var startColor = shadow.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, shadowTargetAlpha);
        while (time < transitionTime)
        {
            time += Time.deltaTime;
            shadow.color = Color.Lerp(startColor, endColor, time / transitionTime);
            yield return null;
        }
    }
    
    [ContextMenu("Unhighlight Element")]
    public void UnhighlightElement()
    {
        for (int i = 0; i < elementsToHighlight.Count; i++)
        {
            elementsToHighlight[i].SetParent(_elementsOriginalParents[i], true);
        }
        StartCoroutine(HideShadow());
    }
    
    private IEnumerator HideShadow()
    {
        var time = 0f;
        var startColor = shadow.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        while (time < transitionTime)
        {
            time += Time.deltaTime;
            shadow.color = Color.Lerp(startColor, endColor, time / transitionTime);
            yield return null;
        }
    }
}
