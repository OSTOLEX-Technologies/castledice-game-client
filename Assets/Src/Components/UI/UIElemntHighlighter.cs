using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElemntHighlighter : MonoBehaviour
{
    [SerializeField] private Image _shadow;
    [SerializeField] private float _transitionTime = 0.5f;
    [SerializeField] private int _shadowFinalTransparency = 150;
    [SerializeField] private RectTransform _elementToHighlight;
    private RectTransform _elementOriginalParent;
    
    private void Start()
    {
        _elementOriginalParent = _elementToHighlight.parent.GetComponent<RectTransform>();
    }
    
    [ContextMenu("Highlight Element")]
    public void HighlightElement()
    {
        _elementToHighlight.SetParent(_shadow.transform);
        StartCoroutine(ShowShadow());
    }
    
    private IEnumerator ShowShadow()
    {
        var time = 0f;
        var startColor = _shadow.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, _shadowFinalTransparency / 255f);
        while (time < _transitionTime)
        {
            time += Time.deltaTime;
            _shadow.color = Color.Lerp(startColor, endColor, time / _transitionTime);
            yield return null;
        }
    }
    
    [ContextMenu("Unhighlight Element")]
    public void UnhighlightElement()
    {
        _elementToHighlight.SetParent(_elementOriginalParent);
        StartCoroutine(HideShadow());
    }
    
    private IEnumerator HideShadow()
    {
        var time = 0f;
        var startColor = _shadow.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        while (time < _transitionTime)
        {
            time += Time.deltaTime;
            _shadow.color = Color.Lerp(startColor, endColor, time / _transitionTime);
            yield return null;
        }
    }
}
