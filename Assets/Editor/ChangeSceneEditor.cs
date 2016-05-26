using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ChangeScene), true)]
public class ChangeSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
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
