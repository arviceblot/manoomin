using UnityEngine;

/// <summary>
/// Manages debug mode and application shutdown.
/// </summary>
public class ApplicationManager : MonoBehaviour
{
    /// <summary>
    /// The "singleton" instance.
    /// </summary>
    private static ApplicationManager instance = null;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static ApplicationManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private bool useDebugMode = true;

    [SerializeField]
    private KeyCode m_quitKey = KeyCode.Escape;

    /// <summary>
    /// Gets the debug mode.
    /// </summary>
    public bool UseDebugeMode
    {
        get { return useDebugMode; }
    }

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
