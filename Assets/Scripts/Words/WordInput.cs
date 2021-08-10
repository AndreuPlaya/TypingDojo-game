using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WordManager))]
public class WordInput : MonoBehaviour
{
    private WordManager _wordManager;
    private void Awake()
    {
        _wordManager = GetComponent<WordManager>();
    }

    void Update()
    {
       foreach (char letter in Input.inputString)
        {
            _wordManager.TypeLetter(letter);
        }
    }
}
