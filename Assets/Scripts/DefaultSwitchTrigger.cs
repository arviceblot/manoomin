using UnityEngine;
using System.Collections;

public class DefaultSwitchTrigger : SceneChangeTrigger
{
    [SerializeField]
    private KeyCode m_defaultSwitchTriggerKey = KeyCode.N;

    private void Update()
    {
        if (Input.GetKeyDown(m_defaultSwitchTriggerKey))
        {
            Trigger();
        }
    }
}
