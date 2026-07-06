using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<IntEventArgs> OnLivesUpdated;
    public event EventHandler<IntEventArgs> OnPointsChanged;
    public class IntEventArgs : EventArgs
    {
        public int Value;
    }

    [SerializeField] private int _startingLives = 3;
    private int _lives;

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
        SetDefaultValues();
    }


    private void SetDefaultValues()
    {
        SetLives(_startingLives);
    }

    private void SetLives(int lives)
    {
        _lives = lives;

        OnLivesUpdated?.Invoke(this, new IntEventArgs
        {
            Value = _lives
        });
    }

    private void OnPointsScored(object sender, BlockManager.OnPointsScoredEventArgs e)
    {
        int points = e.Points;
        _points += points;
        OnPointsChanged?.Invoke(this, new IntEventArgs
        {
            Value = _points
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

    private int GetLives()
    {
        return _lives;
    }
}
