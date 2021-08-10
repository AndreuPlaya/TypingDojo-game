using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _wordPrefab;
    [SerializeField] private RectTransform _wordCanvas;

    public WordDisplay SpawnWord()
    {
        WordDisplay wordDisplay = InstantiateNewWord();
        return wordDisplay;
    }

    private WordDisplay InstantiateNewWord()
    {
        GameObject wordObject = Instantiate(_wordPrefab, GetSpawnPosition(),Quaternion.identity, _wordCanvas);
        WordDisplay wordDisplay = wordObject.GetComponent<WordDisplay>();
        return wordDisplay;
    }


    private Vector3 GetSpawnPosition()
    {
        float maxX = 2.3f;
        float maxY = 6;

        float randomX = Random.Range(-maxX, maxX);
        Vector3 newPosition = new Vector3(randomX, maxY);

        return newPosition;
    }


}
