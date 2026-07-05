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

    private int _points;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BlockManager.Instance.OnPointsScored += OnPointsScored;
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


    public int GetPoints()
    {
        return _points;
    }
}
