using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(WordManager))]
public class WordTimer : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 3f;
    private WordManager _wordManager;
    private float nextSpawnTime = 0f;


    public float WordFallSpeed => 2 / spawnDelay;

    private void Awake()
    {
        _wordManager = GetComponent<WordManager>();
    }
    private void Update()
    {
        if (nextSpawnTime < Time.time )
        {
            nextSpawnTime = Time.time + spawnDelay;
            spawnDelay *= 0.99f;
            _wordManager.AddWord(WordFallSpeed);
        }
    }
   
}


