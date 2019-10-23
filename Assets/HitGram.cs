using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGram : MonoBehaviour, GamePanel
{
    public Material badMaterial;
    public Material warnMaterial;
    public Material originalMaterial;

    float nextWarn = 0;
    float hitDuration = 10;

    float range = 10;

    public GameObject border;
    List<GameObject> borderContent = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        nextWarn = nextRandom();

        // game get all border pieces, they should have the same material
        for (int c = 0; c < border.transform.childCount; c++)
        {
            borderContent.Add(border.transform.GetChild(c).gameObject);
        }

        setBorderMaterial(originalMaterial);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > nextWarn)
        {
            if (Time.realtimeSinceStartup > nextWarn + hitDuration)
            {
                setBorderMaterial(badMaterial);
            }
            else
            {
                setBorderMaterial(warnMaterial);
            }
        }
        else
        {
                Debug.Log("ok");
            setBorderMaterial(originalMaterial);
        }
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
        return range;
        // return Time.realtimeSinceStartup + Random.Range(0f, range);
    }

    public float getStability()
    {
        return 1;
    }
    public void setAutoPilot(bool on)
    {

    }
}
