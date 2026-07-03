using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelLayoutCreator))]
public class LevelLayoutCreatorEditor : Editor
{

    private float previousTime;

    private void OnSceneGUI()
    {
        LevelLayoutCreator t = target as LevelLayoutCreator;

        if (t == null)
            return;


        Vector2 mousePosition = Event.current.mousePosition;
        Vector2 worldPosition = HandleUtility.GUIPointToWorldRay(mousePosition).origin;

        if(Event.current.type == EventType.MouseDown && Time.time - previousTime > 0.1f)
        {
            previousTime = Time.time;
            t.ToggleCellAt(worldPosition);
        }
    }
}
