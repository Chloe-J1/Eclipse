using UnityEngine;
using Eclipse.Audio;

public class FootstepHandler : MonoBehaviour
{
    [SerializeField] private float _stepThreshold = 5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] public MaterialMapperDark _materialMapperDark;
    [SerializeField] public MaterialMapperLight _materialMapperLight;
    [SerializeField] private PlayerType m_player;

    private Vector3 _previousPosition;
    private float _elapsedDistance;



    private void Awake()
    {
        _previousPosition = transform.position;



    }

    /*void Start()
    {
        _materialMapperDark = Resources.Load<MaterialMapperDark>("MM_Dark");
        _materialMapperLight = Resources.Load<MaterialMapperLight>("MM_Light");
    }*/

    private void Update()
    {
        //Vector3 differenceThisFrame = transform.position - _previousPosition;
        //differenceThisFrame.y = 0;
        //_elapsedDistance += differenceThisFrame.magnitude;

        //if (_elapsedDistance > _stepThreshold)
        //{
        //    Footstep();
        //    _elapsedDistance -= _stepThreshold;
        //}

        //_previousPosition = transform.position;
    }

    public void Footstep()
    {
        // Debug.Log("Footstep called");

        Vector3 origin = transform.position + Vector3.up;
        RaycastHit hitInfo;

        bool didHit = Physics.Raycast(origin, Vector3.down, out hitInfo, 2f, _layerMask, QueryTriggerInteraction.Ignore);
        // Debug.Log(didHit ? $"Ray hit: {hitInfo.collider.name}" : "Ray missed");
        if (!didHit) return;

        Material material = GetMaterial(hitInfo);
        //  Debug.Log(material != null ? $"Material found: {material.name}" : "Material was NULL");

        if (material == null) return;




        if (m_player == PlayerType.Dark)
        {
            DarkElements element = _materialMapperDark.GetElement(material);
            // Debug.Log($"Element found: {element}");
            AudioEvents.DarkFootstep(element);
        }
        else
        {
            LightElements element = _materialMapperLight.GetElement(material);
            // Debug.Log($"Element found: {element}");
            AudioEvents.LightFootstep(element);
        }
    }

    private Material GetMaterial(RaycastHit hitInfo)
    {
        MeshRenderer meshRenderer = hitInfo.collider.GetComponent<MeshRenderer>();
        if (meshRenderer == null || meshRenderer.material == null)
        {
            Debug.LogWarning("No material found", hitInfo.collider);
            return null;
        }

        Material result = meshRenderer.sharedMaterial;
        MeshCollider meshCollider = hitInfo.collider as MeshCollider;

        if (meshCollider == null) return result;

        Mesh meshWeHit = meshCollider.sharedMesh;
        if (meshWeHit.subMeshCount == 1) return result;

        int submeshIndex = FindSubmeshIndex(meshWeHit, hitInfo.triangleIndex);
        return meshRenderer.sharedMaterials[submeshIndex];
    }

    private int FindSubmeshIndex(Mesh mesh, int triangleIndex)
    {
        int lookupTri = triangleIndex * 3;

        for (int submeshId = 0; submeshId < mesh.subMeshCount; submeshId++)
        {
            int[] indices = mesh.GetTriangles(submeshId);
            if (lookupTri >= indices.Length)
                lookupTri -= indices.Length;
            else
                return submeshId;
        }

        Debug.LogError("Could not find submesh index");
        return -1;
    }

    public void LandSFX()
    {
        // Debug.Log("Footstep called");

        Vector3 origin = transform.position + Vector3.up;
        RaycastHit hitInfo;

        bool didHit = Physics.Raycast(origin, Vector3.down, out hitInfo, 2f, _layerMask, QueryTriggerInteraction.Ignore);
        // Debug.Log(didHit ? $"Ray hit: {hitInfo.collider.name}" : "Ray missed");
        if (!didHit) return;

        Material material = GetMaterial(hitInfo);
        //  Debug.Log(material != null ? $"Material found: {material.name}" : "Material was NULL");

        if (material == null) return;

        if (m_player == PlayerType.Dark)
        {
            DarkElements element = _materialMapperDark.GetElement(material);

            if (element == DarkElements.stoneFootstep)
            {
                AudioEvents.DarkLandStone();
            }
            else if (element == DarkElements.woodFootstep)
            {
                AudioEvents.DarkLandWood();
            }
            else if (element == DarkElements.grateFootstep)
            {
                AudioEvents.DarkLandMetal();
            }

        }
        else
        {
            LightElements element = _materialMapperLight.GetElement(material);
            if (element == LightElements.stoneFootstep)
            {
                AudioEvents.LightLandStone();
            }
            else if (element == LightElements.woodFootstep)
            {
                AudioEvents.LightLandWood();
            }
            else if (element == LightElements.grateFootstep)
            {
                AudioEvents.LightLandMetal();
            }

            Debug.Log(element);
        }

    }
}