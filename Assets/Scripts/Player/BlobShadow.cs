using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobShadow : MonoBehaviour
{
    public List<GameObject> _shadows;
    RaycastHit _hit;
    float _offset;


    private void FixedUpdate()
    {
        foreach (GameObject shadow in _shadows)
        {
            Ray Ray = new Ray(new Vector3(shadow.transform.position.x, shadow.transform.position.y - _offset, shadow.transform.position.z), Vector3.down);
            Vector3 hitPosition = _hit.point;
            shadow.transform.position = hitPosition;

            if (Physics.Raycast(Ray, out _hit))
            {
                Debug.Log("Hit: " + _hit.transform);
            }
        }
    }


}
