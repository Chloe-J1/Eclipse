using Unity.VectorGraphics;
using UnityEngine;


[ExecuteAlways]

public class FXShapeSizeOverLifeTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    [SerializeField] float minRadius = 1f;
    [SerializeField] float maxRadius = 5f;

    private ParticleSystem ps;
    private ParticleSystem.ShapeModule Shape;



    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Shape = ps.shape;

    }

    // Update is called once per frame
    void Update()
    {
        float psDuration = ps.main.duration;

        float t = Mathf.PingPong(ps.time / (psDuration * 0.5f), 1f);    
        Shape.radius = Mathf.Lerp(minRadius , maxRadius , t);

    }
}
