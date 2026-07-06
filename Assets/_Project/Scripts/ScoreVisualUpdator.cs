using TMPro;
using UnityEngine;

public class ScoreVisualUpdator : MonoBehaviour
{
    [SerializeField] private string _scoreText = "SCORE: ";
    [SerializeField] private TextMeshProUGUI _tmpObject;

    private void Start()
    {
        GameManager.Instance.OnPointsChanged += UpdateScore;
    }

    private void UpdateScore(object sender, GameManager.IntEventArgs e)
    {
        _tmpObject.text = _scoreText + e.Value.ToString();
    }
}
