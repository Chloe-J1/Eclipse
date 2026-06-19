using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public Material m_darkWorldSky;
    public Material m_lightWorldSky;

    public float m_rotationSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (m_darkWorldSky == null || m_lightWorldSky == null) return;
        float currentDark =  m_darkWorldSky.GetFloat("_Rotation");
        float currentLight =  m_lightWorldSky.GetFloat("_Rotation");

        m_darkWorldSky.SetFloat("_Rotation", currentDark + m_rotationSpeed * Time.deltaTime);   
        m_lightWorldSky.SetFloat("_Rotation", currentDark - m_rotationSpeed * Time.deltaTime);   
    }
}
