using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    private static ApplicationManager instance = null;

    public static ApplicationManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private KeyCode m_quitKey = KeyCode.Escape;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("There can be only one " + name + " and he does not share power.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_quitKey))
        {
#if UNITY_EDITOR
            Debug.Break();
#else
            Application.Quit();
#endif
        }

    }
}
