using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Rendering.HighDefinition;

public class LightController : NetworkBehaviour
{
    Time state;
    // Start is called before the first frame update
    public enum Time
    {
        Morning,
        AfterNoon,
        Night
    }
    void Start()
    {
        state = Time.Morning;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    //HDRISky volume = GameObject.Find("Volume").GetComponent<HDRISky>();
        //    GameObject morningLight = GameObject.Find("MorningLight");
        //    GameObject AfterNoonLight = GameObject.Find("AfterNoonLight");

        //    if (state == Time.Morning)
        //    {
        //        morningLight.GetComponent<Light>().enabled = false;
        //        AfterNoonLight.GetComponent<Light>().enabled = true;
        //        state = Time.AfterNoon;
        //    }else if(state == Time.AfterNoon)
        //    {
        //        AfterNoonLight.GetComponent<Light>().enabled = false;
        //        //volume.exposure.SetValue);
        //        ControlLights(true);
        //        state = Time.Night;
        //    }
        //    else
        //    {
        //        morningLight.GetComponent<Light>().enabled = true;
        //        ControlLights(false);
        //        state = Time.Morning;
        //    }
        //}
    }

    void ControlLights(bool onOrOff)
    {
        Light spotlight = GameObject.Find("SpotLight").GetComponent<Light>();
        Light pointLight = GameObject.Find("PointLight").GetComponent<Light>();
        Light pointLight1 = GameObject.Find("PointLight1").GetComponent<Light>();
        Light pointLight2 = GameObject.Find("PointLight2").GetComponent<Light>();
        spotlight.enabled = onOrOff;
        pointLight.enabled = onOrOff;
        pointLight1.enabled = onOrOff;
        pointLight2.enabled = onOrOff;
    }

}
