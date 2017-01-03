using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Trigger class for changing scenes.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private List<SceneChangeTrigger> m_changeTriggers;
    [SerializeField]
    private bool changeOnStart = false;

    [HideInInspector]
    public string m_scenePath;

    private void OnEnable()
    {
        // add any trigger on the app manager
        var appManager = FindObjectOfType<ApplicationManager>();
        m_changeTriggers.AddRange(appManager.GetComponents<SceneChangeTrigger>());

        foreach (var trigger in m_changeTriggers)
        {
            trigger.OnTrigger += Change;
        }
    }

    private void Start()
    {
        if (changeOnStart)
        {
            Change();
        }
    }

    private void OnDisable()
    {
        foreach (var trigger in m_changeTriggers)
        {
            trigger.OnTrigger -= Change;
        }
    }

    /// <summary>
    /// Changes the scene.
    /// </summary>
    private void Change()
    {
        // remove the parts of the filepath necessary for loading the scene correctly
        // there really should be a better way of doing this
        var scene = Regex.Replace(m_scenePath, "\\.unity", String.Empty);
        scene = Regex.Replace(scene, "Assets/", String.Empty);

        // load the scene
        Debug.Log("Loading scene: " + scene);
        SceneManager.LoadScene(scene);
    }
}
