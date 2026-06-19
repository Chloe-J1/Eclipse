using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PerCameraSkybox : MonoBehaviour
{
    public Material m_skyboxMaterial;
    private Camera m_cam;
    private Material m_previousSkybox;

    void Awake()
    {
        m_cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCamera;
        RenderPipelineManager.endCameraRendering += OnEndCamera;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCamera;
        RenderPipelineManager.endCameraRendering -= OnEndCamera;
    }

    void OnBeginCamera(ScriptableRenderContext ctx, Camera camera)
    {
        if (camera == m_cam)
        {
            m_previousSkybox = RenderSettings.skybox;
            RenderSettings.skybox = m_skyboxMaterial;
        }
    }

    void OnEndCamera(ScriptableRenderContext ctx, Camera camera)
    {
        if (camera == m_cam)
        {
            RenderSettings.skybox = m_previousSkybox;
        }
    }
}
