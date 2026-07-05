using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<OnPointsChangedEventArgs> OnPointsChanged;
    public class OnPointsChangedEventArgs : EventArgs
    {
        public int Points;
    }

    [SerializeField] private float _levelLoadDelay = 2f;
    private int _points;
    private int _currentLevel;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BlockManager.Instance.OnPointsScored += OnPointsScored;
        BlockManager.Instance.OnLevelComplete += OnLevelComplete;

        StartLevel();
    }


    private void StartLevel()
    {
        BlockManager.Instance.LoadLevel(_currentLevel);
        Player.Instance.GetReady();
    }


    
    private void OnPointsScored(object sender, BlockManager.OnPointsScoredEventArgs e)
    {
        int points = e.Points;
        _points += points;
        OnPointsChanged?.Invoke(this, new OnPointsChangedEventArgs
        {
            Points = _points
        });
    }

    private void OnLevelComplete(object sender, EventArgs e)
    {
        // level complete logic
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
        Ball.Instance.gameObject.SetActive(false);
    }

    private void LoadNextLevel()
    {
        Ball.Instance.gameObject.SetActive(true);
        _currentLevel++;
        StartLevel();
    }


    public int GetPoints()
    {
        return _points;
    }
}
