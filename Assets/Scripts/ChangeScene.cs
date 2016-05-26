using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Text.RegularExpressions;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    public string m_scenePath;

    // TODO: fix this in the editor script so it is visible
    [SerializeField]
    private KeyCode m_defaultSwitchTriggerKey = KeyCode.N;

    private void Update()
    {
        if (Input.GetKeyDown(m_defaultSwitchTriggerKey))
        {
            Change();
        }
    }

    public void Change()
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
