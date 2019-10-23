using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitGram : MonoBehaviour, GamePanel
{
    public float hitDuration = 10;

    public float min = 5;
    public float max = 10;

    public char keyChar = 'E';
    public KeyCode key;


    public Material badMaterial;
    public Material warnMaterial;
    public Material originalMaterial;

    float nextWarn = 0;


    public GameObject border;
    public GameObject midLine;
    public GameObject movingLine;
    List<GameObject> borderContent = new List<GameObject>();

    int state = -1; //state of this item 0=green 1=orange 2=bad
    private static readonly int STATE_GOOD = 0;
    private static readonly int STATE_WARN = 1;
    private static readonly int STATE_BAD = 2;

    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        nextWarn = nextRandom();

        // game get all border pieces, they should have the same material
        for (int c = 0; c < border.transform.childCount; c++)
        {
            borderContent.Add(border.transform.GetChild(c).gameObject);
        }
        setState(STATE_GOOD);

        key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyChar + "");

        TextMeshPro textMeshPro = text.GetComponent<TextMeshPro>();
        textMeshPro.SetText(keyChar + "");
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.realtimeSinceStartup;
        if (time > nextWarn)
        {
            float delta = (time - nextWarn) / hitDuration;
            if (time > nextWarn + hitDuration)
            {
                setState(STATE_BAD);
            }
            else
            {
                setState(STATE_WARN);
                Vector3 loc = movingLine.transform.localPosition;
                loc.y = Mathf.Lerp(4, -4, delta);
                movingLine.transform.localPosition = loc;

            }

            if (Input.GetKey(key))
            {
                nextWarn = nextRandom();
            }
        }
        else
        {
            setState(STATE_GOOD);
        }
    }

    void setState(int state)
    {
        if (this.state != state)
        {
            if (state == STATE_GOOD)
            {
                // nothing to do
                setBorderMaterial(originalMaterial);
                midLine.SetActive(false);
                movingLine.SetActive(false);
            }
            else if (state == STATE_WARN)
            {
                // hit timer
                setBorderMaterial(warnMaterial);
                midLine.SetActive(true);
                movingLine.SetActive(true);
            }
            else if (state == STATE_BAD)
            {
                // very bad
                setBorderMaterial(badMaterial);
                midLine.SetActive(false);
                movingLine.SetActive(false);
            }
        }
        this.state = state;
    }

    void setBorderMaterial(Material material)
    {
        foreach (GameObject obj in borderContent)
        {
            obj.GetComponent<Renderer>().material = material;
        }
    }

    float nextRandom()
    {
        return Time.realtimeSinceStartup + Random.Range(min, max);
    }

    public float getStability()
    {
        return 1;
    }
    public void setAutoPilot(bool on)
    {

    }
}
