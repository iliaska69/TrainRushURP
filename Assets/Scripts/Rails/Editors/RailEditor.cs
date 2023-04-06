using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Rail))]
public class RailEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Rail myScript = (Rail)target;
        if (GUILayout.Button("Join"))
        {
            myScript.JoinToPoint();
        }
    }
}
