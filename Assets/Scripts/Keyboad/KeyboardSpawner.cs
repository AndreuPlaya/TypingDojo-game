using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class KeyboardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] private float _rowOffset;
    [SerializeField] private float _keySpacing;
    private Transform KeyHolder;
    private Dictionary<char,Key> _keys;


    public void SpawnKeyboard()
    {

        string holderName = "Key Holder";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        KeyHolder = new GameObject(holderName).transform;
        KeyHolder.parent = this.transform;
        KeyHolder.transform.localPosition = Vector3.zero;

        _keys = new Dictionary<char, Key>();

        InstantiateKey(new int2(0, 0), 'q');
        InstantiateKey(new int2(1, 0), 'w');
        InstantiateKey(new int2(2, 0), 'e');
        InstantiateKey(new int2(3, 0), 'r');
        InstantiateKey(new int2(4, 0), 't');
        InstantiateKey(new int2(5, 0), 'y');
        InstantiateKey(new int2(6, 0), 'u');
        InstantiateKey(new int2(7, 0), 'i');
        InstantiateKey(new int2(8, 0), 'o');
        InstantiateKey(new int2(9, 0), 'p');

        InstantiateKey(new int2(0, 1), 'a');
        InstantiateKey(new int2(1, 1), 's');
        InstantiateKey(new int2(2, 1), 'd');
        InstantiateKey(new int2(3, 1), 'f');
        InstantiateKey(new int2(4, 1), 'g');
        InstantiateKey(new int2(5, 1), 'h');
        InstantiateKey(new int2(6, 1), 'j');
        InstantiateKey(new int2(7, 1), 'k');
        InstantiateKey(new int2(8, 1), 'l');
        InstantiateKey(new int2(9, 1), ';');

        InstantiateKey(new int2(0, 2), 'z');
        InstantiateKey(new int2(1, 2), 'x');
        InstantiateKey(new int2(2, 2), 'c');
        InstantiateKey(new int2(3, 2), 'v');
        InstantiateKey(new int2(4, 2), 'b');
        InstantiateKey(new int2(5, 2), 'n');
        InstantiateKey(new int2(6, 2), 'm');
        InstantiateKey(new int2(7, 2), ',');
        InstantiateKey(new int2(8, 2), '.');
        InstantiateKey(new int2(9, 2), '/');

    }

    public void ClearAllKeys(char exceptThisChar)
    {
        foreach (KeyValuePair<char, Key> key in _keys)
        {
            if (key.Key == exceptThisChar)
                continue;
            key.Value.RestoreKeyColor();
        }
    }
    public void SetKeyColor(char letter, Color color)
    {
        _keys.TryGetValue(letter, out Key key);
        key?.SetKeyColor(color);
    }
    public void SetKeyColorAndFade(char letter, Color color)
    {
        _keys.TryGetValue(letter, out Key key);
        key?.SetKeyColorAndFade(color);
    }
    private void InstantiateKey(int2 position, char letter)
    {
        GameObject keyObject = Instantiate(_keyPrefab, CalculateKeyPosition(position), Quaternion.identity, KeyHolder);
        keyObject.name = "Key_" + letter;
        KeyVisual keyVisual = keyObject.GetComponent<KeyVisual>();
        Key newKey = new Key(position, letter, keyVisual);
        newKey.SetPosition(CalculateKeyPosition(newKey._coords));
        _keys.Add(letter,newKey);
    }
    public void UpdateKeyPositions()
    {
        if (_keys == null)
            SpawnKeyboard();
        foreach (KeyValuePair<char, Key> key in _keys)
        {
            key.Value.SetPosition(CalculateKeyPosition(key.Value._coords));
        }
    }

    private Vector3 CalculateKeyPosition(int2 position)
    {
        float x = _keySpacing * position.x + _rowOffset * position.y;
        float y = -_keySpacing * position.y;

        return new Vector3(x, y) + transform.position;
    }

    private class Key
    {
        public char _letter;
        public int2 _coords;
        public KeyVisual _display;
        public Key(int2 position, char letter, KeyVisual display)
        {
            _letter = letter;
            _coords = position;
            _display = display;
            SetCharacter(letter);
        }
        public void RestoreKeyColor()
        {
            _display.RestoreKeyColor();
        }
        public void SetKeyColor(Color color)
        {
            _display.SetKeyColor(color);
        }
        public void SetKeyColorAndFade(Color color)
        {
            _display.SetKeyColorAndFade(color);
        }
        public void SetCharacter(char letter)
        {
            _display.SetChar(letter);
        }
        public void SetPosition(Vector3 position)
        {
            _display.SetKeyPosition(position);
        }
    }
}
