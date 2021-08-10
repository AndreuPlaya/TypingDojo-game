using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyVisual : MonoBehaviour
{

    private TextMeshPro _text;
    private Material _material;
    private Color _originalColor;
    private float _fadeTime = 10f;
    private Coroutine _fadeToColor;
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _originalColor = _material.color;
        _text = GetComponentInChildren<TextMeshPro>();
    }

    
    public void SetKeyPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    public void SetKeyColor(Color color)
    {
        _material.color = color;
    }
    public void SetKeyColorAndFade(Color color)
    {
        if (_fadeToColor != null)
            StopCoroutine(_fadeToColor);
        _fadeToColor = StartCoroutine(FadeToOriginalColorFrom(color: color));
    }
    public void RestoreKeyColor()
    {
        _material.color = _originalColor;
    }
    public void SetChar(char letter)
    {
        if (_text == null)
            _text = GetComponentInChildren<TextMeshPro>();
        _text.text = letter.ToString().ToUpper();
    }

    private IEnumerator FadeToOriginalColorFrom(Color color)
    {
        for (float t = 0; t< _fadeTime; t += 0.1f)
        {
            _material.color = Color.Lerp(color, _originalColor, t / _fadeTime);
            yield return null;
        }
    }
}
