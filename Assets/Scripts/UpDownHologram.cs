using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownHologram : MonoBehaviour, GamePanel
{
    // Start is called before the first frame update

    public Vector2 speedMultiplierRange = new Vector2();
    private float speedMultiplier = 1;
    public float releaseMultiplier = 1;
    public float noiseMultiplier = 1;

    private GameObject dot;

    private bool autoPilot = true;
    private bool autoPilotPress = false;

    private Vector3 initalLocation;

    private bool upsidedown = false;
    private bool flipped = false;
    private float value = 0.5f;

    private float seed = 0;

    public char keyChar = 'E';
    public KeyCode key;

    private float movementRange = 1;

    public Material badMaterial;
    public Material warnMaterial;
    private Material originalMaterial;

    private Renderer rend;

    private float stability;

    public GameObject text;

    void Start()
    {
        dot = transform.GetChild(0).gameObject;

        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        upsidedown = transform.lossyScale.y <= 0;
        flipped = transform.lossyScale.x <= 0;
        seed = Random.Range(-1000, 1000);
        speedMultiplier = Random.Range(speedMultiplierRange.x, speedMultiplierRange.y);
        initalLocation = dot.transform.localPosition;

        key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyChar + "");

        badMaterial = new Material(badMaterial);
        warnMaterial = new Material(warnMaterial);
        originalMaterial = new Material(originalMaterial);

        TextMesh textMesh = text.GetComponent<TextMesh>();
        textMesh.text = keyChar+"";
        Vector3 textScale = text.transform.localScale;
        if (flipped) {
            textScale.x = -textScale.x;
        } 
        if (upsidedown) {
            textScale.y = -textScale.y;
        }
        text.transform.localScale = textScale;
        textMesh.color = new Color(5,5,5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = dot.transform.localPosition;

        float speed = Mathf.PerlinNoise(Time.realtimeSinceStartup * noiseMultiplier, seed);

        // clip top and bottom
        speed = Mathf.Clamp(speed * 4 - 1, 0, 1);

        speed *= speedMultiplier;

        if (autoPilot)
        {
            if (value > .6 && !autoPilotPress)
            {
                autoPilotPress = true;
            }
            else if (value < .4 && autoPilotPress)
            {
                autoPilotPress = false;
            }
            
            badMaterial.SetFloat("_EmissiveExposureWeight", 1);
            warnMaterial.SetFloat("_EmissiveExposureWeight", 1);
            originalMaterial.SetFloat("_EmissiveExposureWeight", 1);
        } else {
            autoPilotPress = false;

            badMaterial.SetFloat("_EmissiveExposureWeight", 0);
            warnMaterial.SetFloat("_EmissiveExposureWeight", 0);
            originalMaterial.SetFloat("_EmissiveExposureWeight", 0);
        }


        if ((!autoPilot && Input.GetKey(key)) || autoPilotPress)
        {
            value -= releaseMultiplier * Time.deltaTime * speedMultiplier;
        }

        value += speed * Time.deltaTime;

        value = Mathf.Clamp(value, 0, 1);


        // calc done
        stability = 1-Mathf.Abs(value - .5f);

        if (value > 0.875 || value < 0.125)
        {
            stability = 0;
            rend.material = badMaterial;
        }
        else if (value > 0.75 || value < 0.25)
        {
            rend.material = warnMaterial;
        }
        else
        {
            rend.material = originalMaterial;
        }

        if (upsidedown)
            position.y = Mathf.Lerp(movementRange, -movementRange, value) + initalLocation.y;
        else
            position.y = Mathf.Lerp(-movementRange, movementRange, value) + initalLocation.y;
        dot.transform.localPosition = position;
    }

    public float getStability()
    {
        return stability;
    }
    public void setAutoPilot(bool on)
    {
        autoPilot = on;
    }
}
