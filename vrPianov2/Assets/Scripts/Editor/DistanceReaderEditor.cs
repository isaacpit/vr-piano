using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DistanceReader))]
public class DistanceReaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var reader = (DistanceReader)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("Distance"))
        {
            Debug.Log(reader.DistanceBetween(reader.objToMeasure));
        }
        GUILayout.Label("Below buttons only work in play mode");
        if (GUILayout.Button("Distance X"))
        {            
            Debug.Log(reader.GetDistanceRatioX());
        }
        if (GUILayout.Button("Distance Y"))
        {
            Debug.Log(reader.GetDistanceRatioY());
        }
        if (GUILayout.Button("Distance Z"))
        {
            Debug.Log(reader.GetDistanceRatioX());
        }
    }
}