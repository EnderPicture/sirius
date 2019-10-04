using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressureMeter : MonoBehaviour
{
    private const float maxPressureAngle = -20;
    private const float minPressureAngle = 210;

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

        pressure = 0f;
        pressureMax = 200f;

    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerInput();

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
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float deceleration = 50f;
            pressure -= deceleration * Time.deltaTime;
        }
        else {
            float acceleration = 20f;
            pressure += acceleration * Time.deltaTime;
        }

        pressure = Mathf.Clamp(pressure, 0f, pressureMax); // to make sure it's in the right range

    }

}
