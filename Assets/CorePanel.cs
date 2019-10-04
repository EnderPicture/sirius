using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class CorePanel : MonoBehaviour
{
    public GameObject projectionPoint;
    public float speed = 5;  
    private float angle = 0;

    public Vector2 axis;

    private VisualEffect vfx;
 
    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        vfx.SetFloat("Sweep Angle", angle);
        vfx.SetVector3("Projection Point", projectionPoint.transform.position);
        transform.rotation = Quaternion.Euler(axis.x, 0, axis.y);
    }
}
