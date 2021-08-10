using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(KeyboardSpawner))]
public class KeyboardEditor : Editor
{
    KeyboardSpawner _keyboard;
    private void OnEnable()
    {
        _keyboard = (KeyboardSpawner)target;
        _keyboard.SpawnKeyboard();
    }
    public override void OnInspectorGUI()
    {
        _keyboard.UpdateKeyPositions();
    }
}
#endif
