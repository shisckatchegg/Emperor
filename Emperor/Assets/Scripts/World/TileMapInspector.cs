using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor {

	// Use this for initialization
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawDefaultInspector();

        if(GUILayout.Button("Regenerate"))
        {
            ((TileMap)target).RegenerateMesh();
        }
    }
}
