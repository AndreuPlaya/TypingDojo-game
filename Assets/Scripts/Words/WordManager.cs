using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordManager : MonoBehaviour
{
    public event Action<char> OnKeyPressed;
    public event Action<char> OnKeyPressedNextLetter;
    public event Action<int> OnScoreUpdated;
    [SerializeField] private List<Word> _words;
    private WordSpawner _wordSpawner;
    private Word activeWord;
    private int _score = 0;
    public int ActiveWords => _words.Count;


    private void Awake()
    {
        _wordSpawner = GetComponent<WordSpawner>();
    }
    
    public void AddWord(float fallSpeed) 
    {
        (string word, int value) wordString = WordGenerator.GetRandomWord();
        WordDisplay wordDisplay = _wordSpawner.SpawnWord();
        Word word = new Word(wordString.word, wordString.value, fallSpeed, wordDisplay);
        word.OnWordTimeout += HandleWordTimeout;
        _words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        if (activeWord == null)
        {
            foreach (Word word in _words)
            {
                if (word.GetCurrentLetter() == letter)
                {
                    activeWord = word;
                    OnKeyPressed?.Invoke(letter);
                    OnKeyPressedNextLetter?.Invoke(activeWord.GetNextLetter());
                    word.TypeLetter();
                    CheckActiveWordIsComplete();
                    break;
                }
            }
            return;
        }
        if(activeWord?.GetCurrentLetter() == letter)
        {

            OnKeyPressed?.Invoke(letter);
            OnKeyPressedNextLetter?.Invoke(activeWord.GetNextLetter());
            activeWord.TypeLetter();
            CheckActiveWordIsComplete();
            return;
        }
    }
    private void CheckActiveWordIsComplete()
    {
        if (activeWord.IsComplete)
        {
            _score += activeWord.Value;
            OnScoreUpdated(_score);
            _words.Remove(activeWord);
            activeWord.RemoveWord();
            activeWord = null;
        }
    }

    private void HandleWordTimeout(Word word)
    {
        _score -= word.Value;
        OnScoreUpdated(_score);
        _words.Remove(word);

        if (activeWord == word)
            activeWord = null;
    }
  
}
