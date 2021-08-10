using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class WordGenerator
{
    private static List<(string word, int value)> _valueWords;
    public static (string word, int value) GetRandomWord()
    {
        if (_valueWords == null || _valueWords.Count <= 0)
        {
            _valueWords = WordDictionary.GetWordTouple();
        }
        int randomIndex = Random.Range(0, _valueWords.Count);
        (string word,int value) randomWord = _valueWords[randomIndex];
        return randomWord;
    }
    
   
}
