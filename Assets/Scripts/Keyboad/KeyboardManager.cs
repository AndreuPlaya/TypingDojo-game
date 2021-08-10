using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField]private WordManager _wordManager;
    private KeyboardSpawner _keyboardSpawner;
    private char _nextLetter;
   
    private void Awake()
    {
        InitializeKeyboard();
        _wordManager.OnKeyPressedNextLetter += HandleNextLetter;
    }
    
    public void InitializeKeyboard()
    {
        if (_keyboardSpawner == null)
            _keyboardSpawner = GetComponent<KeyboardSpawner>();
        _keyboardSpawner.SpawnKeyboard();
    }

    void Update()
    {
        if (Input.inputString == string.Empty)
            return;
        _keyboardSpawner.ClearAllKeys(exceptThisChar:_nextLetter);
        foreach (char letter in Input.inputString)
        {
            _keyboardSpawner.SetKeyColorAndFade(letter: letter, color: Color.red);
        }
    }
    private void HandleNextLetter(char nextLetter)
    {
        _nextLetter = nextLetter;
        _keyboardSpawner.SetKeyColor(letter: nextLetter, color: Color.blue);
    }

}
