using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    // TODO: singleton seems to work for one loop of scene changes, but not after
    private static ApplicationManager instance;

    public static ApplicationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ApplicationManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private KeyCode m_quitKey = KeyCode.Escape;

    private void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("There can be only one " + name + " and he does not share power.");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Awake()
    {
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
