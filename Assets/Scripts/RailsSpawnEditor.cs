using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RailsSpawn))]
public class RailsSpawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RailsSpawn myScript = (RailsSpawn)target;
        if (GUILayout.Button("Spawn rails"))
        {
            myScript.Spawn();
        }
    }
}
