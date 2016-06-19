using UnityEngine;
using UnityEditor;
using System;
using System.Text.RegularExpressions;

[CustomEditor(typeof(ChangeScene), true)]
public class ChangeSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var picker = target as ChangeScene;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.m_scenePath);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("m_scenePath");
            scenePathProperty.stringValue = newPath;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
