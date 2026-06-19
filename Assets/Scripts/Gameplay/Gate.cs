using Eclipse;
using Eclipse.Audio;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gate : MonoBehaviour
{
    enum OpenableType
    {
        Gate,
        Door
    }
    [SerializeField] private OpenableType m_openableType = OpenableType.Gate;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float m_timeBeforeClosing;
    public bool m_canClose = true;

    public Button[] m_buttons;
    bool m_isActivated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var button in m_buttons)
        {
            button.m_buttonActivatedEvent += ButtonActivated;
        }
    }

    private void ButtonActivated(object sender, EventArgs e)
    {
        if(m_isActivated == false)
        {
            OpenDoor();
            if (m_canClose)
                StartCoroutine(HandleCloseTimer());
        }
    }

    IEnumerator HandleCloseTimer()
    {
        yield return new WaitForSeconds(m_timeBeforeClosing);
        
        CloseDoor();
    }

    void CloseDoor()
    {
        m_animator.SetBool("Open", false);
        m_animator.SetTrigger("Close");
        AudioEvents.CloseGate();

        m_isActivated = false;
    }

    void OpenDoor()
    {
        m_animator.SetBool("Open", true);
        switch (m_openableType)
        {
            case OpenableType.Gate:
                AudioEvents.OpenGate();
                break;
            case OpenableType.Door:
                AudioEvents.OpenDoor();
                break;
            default:
                break;
        }
        
        m_isActivated = true;
        Gamepad gamepad = Gamepad.current;
        HapticFeedbackManager.Instance.Rumble(gamepad);
    }
}
