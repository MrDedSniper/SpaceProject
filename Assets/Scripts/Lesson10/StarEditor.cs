using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Star)), CanEditMultipleObjects]
public class StarEditor : Editor
{
    private SerializedProperty _centerProperty;
    private SerializedProperty _pointsProperty;
    private SerializedProperty _frequencyProperty;

    private Star _star;

    private void OnEnable()
    {
        _centerProperty = serializedObject.FindProperty("_center");
        _pointsProperty = serializedObject.FindProperty("_points");
        _frequencyProperty = serializedObject.FindProperty("_frequency");

        _star = (Star)target;
        _star.UpdateMesh();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_centerProperty);
        EditorGUILayout.PropertyField(_pointsProperty);
        EditorGUILayout.IntSlider(_frequencyProperty, 1, 20);

        var totalPoints = _frequencyProperty.intValue * _pointsProperty.arraySize;

        if (totalPoints < 3)
        {
            EditorGUILayout.HelpBox("At least three points are needed.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox(totalPoints + " points in total.", MessageType.Info);
        }

        if (serializedObject.ApplyModifiedProperties() || 
            (Event.current.type == EventType.ExecuteCommand && 
             Event.current.commandName == "UndoRedoPerformed"))
        {
            _star.UpdateMesh();
        }
    }

    /*private void OnSceneGUI()
    {
        var star = (Star)target;
        var starTransform = star.transform;
        var angle = -360f / (star.Frequency * star.Points.Length);

        for (var i = 0; i < star.Points.Length; i++)
        {
            var rotation = Quaternion.Euler(0f, 0f, angle * i);
            var oldPoint = starTransform.TransformPoint(rotation * star.Points[i].Position);
            var fmh_61_61_638392219890180111 = Quaternion.identity; var newPoint = Handles.FreeMoveHandle(oldPoint,
                0.02f, Vector3.zero, Handles.DotHandleCap);

            if (oldPoint != newPoint)
            {
                star.Points[i].Position = Quaternion.Inverse(rotation) * 
                                          starTransform.InverseTransformPoint(newPoint);
                _star.UpdateMesh();
            }
        }
    }*/
}