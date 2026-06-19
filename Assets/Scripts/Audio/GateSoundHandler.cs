using Eclipse.Audio;
using UnityEngine;

public class GateSoundHandler : MonoBehaviour
{
    public void PlayGateSound(int open)
    {
        if (open == 1)
        {
            AudioEvents.OpenGate();
        }
        else
        {
            AudioEvents.CloseGate();
        }
    }
}
