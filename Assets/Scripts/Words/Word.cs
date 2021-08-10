using System;
using UnityEngine;
[System.Serializable]
public class Word
{
    private string _word;
    private WordDisplay _display;
    private int _cursorPosition;
    private int _wordLength;
    private int _value;
    public event Action<Word> OnWordTimeout;
    public Word(string word, int value, float fallSpeed, WordDisplay display)
    {
        _word = word;
        _value = value;
        _cursorPosition = 0;
        _wordLength = word.Length;
        _display = display;
        _display.OnWordTimeout += HandleDisplayTimeout;
        _display.StartFalling(fallSpeed);
        UpdateDisplay();
    }
    public bool IsComplete => _cursorPosition >= _wordLength;
    public int Value => _value;
    public void TypeLetter() 
    { 
        if (_cursorPosition < _wordLength)
        {
            _cursorPosition++;
            UpdateDisplay();
        }
    }
    
    public char GetCurrentLetter()
    {
        if (_cursorPosition >= _wordLength)
            return '!';
        return _word[_cursorPosition];
    }
    public char GetNextLetter()
    {
        if (_cursorPosition + 1 >= _wordLength)
            return '!';
        return _word[_cursorPosition + 1];
    }

    public string GetRemaingingLetters()
    {
        if (_cursorPosition < _wordLength)
            return _word.Substring(_cursorPosition, _wordLength - _cursorPosition);
        else
            return null;
    }
    public void RemoveWord()
    {
        _display.DestroyWord();
    }
    private void UpdateDisplay()
    {
        _display.SetWord(GetRemaingingLetters());
        if (IsComplete)
            _display?.DestroyWord();
    }

    private void HandleDisplayTimeout()
    {
        OnWordTimeout?.Invoke(this);
    }
}
