using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressureMeter : MonoBehaviour
{
    private const float maxPressureAngle = 210;
    private const float minPressureAngle = -20;

    public float stability; // 0 - 1
    public bool autoPilot = true;

    private Transform needleTranform;
    //private Transform speedLabelTemplateTransform;

    private float pressureMax;
    private float pressure;

    // Start is called before the first frame update
    void Start()
    {

        needleTranform = transform.Find("needle");
        //speedLabelTemplateTransform = transform.Find("speedLabelTemplate");
        //speedLabelTemplateTransform.gameObject.SetActive(false);

        pressure = 100f;
        pressureMax = 200f;

        stability = 1; // stable at first

    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerInput();
        //Debug.Log(needleTranform.rotation.z);
        Debug.Log(pressure);
        //pressure += 30f * Time.deltaTime;
        //if (pressure > pressureMax) pressure = pressureMax;

        needleTranform.eulerAngles = new Vector3(0, 0, GetPressureRotation());
    }


    private float GetPressureRotation()
    {
        float totalAngleSize = minPressureAngle - maxPressureAngle;

        float pressureNormalized = pressure / pressureMax;

        return minPressureAngle - pressureNormalized * totalAngleSize;
    }

    private void HandlePlayerInput() {

        if (stability == 1)
        {
            autoPilot = true;
        }
        else if (stability == 0) {
            autoPilot = false;
        }

        if (85 <= pressure && pressure <= 115)
        {
            stability = 1;
        }
        else {
            stability = 0;
        }

        if (autoPilot == true) {
            pressure = Random.Range(85f, 115f);
        } else if (autoPilot == false) {
            if (pressure < 85) {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    float deceleration = 50f;
                    pressure += deceleration * Time.deltaTime;
                }
                else
                {
                    float acceleration = 20f;
                    pressure -= acceleration * Time.deltaTime;
                }
            }

            if (pressure > 115)
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    float deceleration = 50f;
                    pressure -= deceleration * Time.deltaTime;
                }
                else
                {
                    float acceleration = 20f;
                    pressure += acceleration * Time.deltaTime;
                }
            }
        }

        // Control stability with keys

        if (Input.GetKey(KeyCode.C))
        {
            stability = 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            stability = 0;
            pressure = 84;
        }

        if (Input.GetKey(KeyCode.E))
        {
            stability = 0;
            pressure = 116;
        }



        pressure = Mathf.Clamp(pressure, 0f, pressureMax); // to make sure it's in the right range

    }


}
