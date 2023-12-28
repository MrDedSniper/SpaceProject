using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Star))]
public class PointsListEditor : Editor
{
    private SerializedProperty _pointsProperty;
    private ReorderableList _pointsList;

    private void OnEnable()
    {
        _pointsProperty = serializedObject.FindProperty("_points");
        _pointsList = new ReorderableList(serializedObject, _pointsProperty, true, true, true, true);
        _pointsList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Points");
        _pointsList.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = _pointsProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _pointsList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        DrawDefaultInspector();
    }
}