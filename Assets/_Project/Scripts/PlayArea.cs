using UnityEngine;

public static class PlayArea
{
    private static PlayAreaDataSO _activeDataSO;


    public static float Width { get { return _activeDataSO.Width; } }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Setup()
    {
        DefaultAreaDataSO defaultData = Resources.Load("DefaultAreaData") as DefaultAreaDataSO;
        _activeDataSO = defaultData.Data;
    }

    public static void SetData(PlayAreaDataSO playAreaDataSO)
    {
        _activeDataSO = playAreaDataSO;
    }
}
