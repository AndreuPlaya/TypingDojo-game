using System;
using TMPro;
using UnityEngine;
using System.Collections;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public event Action OnWordTimeout;
    private Coroutine _translateWord;
   
    public void StartFalling (float fallSpeed)
    {
        _translateWord = StartCoroutine(TranslateWord(fallSpeed));
    }
    public void SetWord(string word)
    {
        _text.text = word;
    }
    public void DestroyWord()
    {
        StopCoroutine(_translateWord);
        Destroy(gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnWordTimeout?.Invoke();
        DestroyWord();
    }
    private IEnumerator TranslateWord(float fallSpeed)
    {
        while (true)
        {
            transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);
            yield return null;
        }
    }
}
