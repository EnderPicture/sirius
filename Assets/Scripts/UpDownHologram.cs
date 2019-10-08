using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownHologram : MonoBehaviour, Panel
{
    // Start is called before the first frame update

    public Vector2 speedMultiplierRange = new Vector2();
    private float speedMultiplier = 1;
    public float releaseMultiplier = 1;
    public float noiseMultiplier = 1;

    private GameObject dot;

    private bool autoPilot = true;
    private Vector3 initalLocation;

    private bool upsidedown = false;
    private float value = 0.5f;

    private float seed = 0;

    public char keyChar = 'E';
    public KeyCode key;

    private float movementRange = 1;

    void Start()
    {
        dot = transform.GetChild(0).gameObject;

        upsidedown = transform.lossyScale.y <= 0;
        seed = Random.Range(-1000,1000);
        speedMultiplier = Random.Range(speedMultiplierRange.x, speedMultiplierRange.y);
        initalLocation = dot.transform.localPosition;

        key = (KeyCode) System.Enum.Parse(typeof(KeyCode), keyChar+"");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = dot.transform.localPosition;

        float speed = Mathf.PerlinNoise(Time.realtimeSinceStartup * noiseMultiplier, seed);
        
        // clip top and bottom
        speed = Mathf.Clamp(speed*4-1, 0, 1);

        speed *= speedMultiplier;

        Debug.Log(speedMultiplier);
        if (Input.GetKey(key))
        {
            value -= releaseMultiplier * Time.deltaTime * speedMultiplier;
        }

        // button not pressed 
        value += speed * Time.deltaTime;

        value = Mathf.Clamp(value, 0, 1);

        // calc done


        if (value > .75 || value < .25) {
        }
        
        if (upsidedown)
            position.y = Mathf.Lerp(movementRange,-movementRange, value) + initalLocation.y;
        else
            position.y = Mathf.Lerp(-movementRange,movementRange, value) + initalLocation.y;
        dot.transform.localPosition = position;
    }

    public float getStability()
    {
        return 1;
    }
    public void setAutoPilot(bool on)
    {
        autoPilot = on;
    }
}
