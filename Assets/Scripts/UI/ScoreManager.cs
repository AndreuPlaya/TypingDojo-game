using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private WordManager _wordManager;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _wordManager.OnScoreUpdated += HandleScoreUpdate;
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void HandleScoreUpdate(int score)
    {
        _text.text = score.ToString();
    }
}
