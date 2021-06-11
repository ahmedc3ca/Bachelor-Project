using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    public GameObject uiMenu;
    public GameObject furnitureMenu;
    public GameObject textureMenu;
    public static bool uiIsShown;
    public Volume HDRIVolume;
    public HDRISky HDRISkyExposure;
    public GameObject hand;

    void Awake()
    {
        //if (HDRIVolume.profile.TryGet<HDRISky>(out HDRISky tempE))
        //{
        //    HDRISkyExposure = tempE;
        //}
    }



    void Start()
    {
        uiIsShown = false;
        uiMenu.SetActive(true);
    }

    public void OpenTextureWindow()
    {
        uiMenu.SetActive(false);
        uiIsShown = false;
        textureMenu.SetActive(true);
    }

    public void OpenFurnitureWindow()
    {
        uiMenu.SetActive(false);
        uiIsShown = false;
        furnitureMenu.SetActive(true);
    }

    private void Update()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        transform.position = hand.transform.position + offset;
        transform.localRotation = hand.transform.localRotation * Quaternion.Euler(180, 0, 0) * Quaternion.Euler(0, 180, 0);
    }


    public void turnCommentsOn()
    {
        GameObject.Find("PlayerSkin(Clone)").GetComponent<WallMover>().commentIsOn = true;
    }

    public void turnCommentsOff()
    {
        GameObject.Find("PlayerSkin(Clone)").GetComponent<WallMover>().commentIsOn = false;
    }

    public void makeMorning()
    {
        GameObject.Find("MorningLight").GetComponent<Light>().enabled = true;
        GameObject.Find("AfterNoonLight").GetComponent<Light>().enabled = false;
        HDRISkyExposure.exposure.value = 12f;
        ControlLights(false);
    }

    public void makeAfternoon()
    {
        GameObject.Find("MorningLight").GetComponent<Light>().enabled = false;
        GameObject.Find("AfterNoonLight").GetComponent<Light>().enabled = true;
        HDRISkyExposure.exposure.value = 12f;
        ControlLights(false);
    }

    public void makeNight()
    {
        GameObject.Find("MorningLight").GetComponent<Light>().enabled = false;
        GameObject.Find("AfterNoonLight").GetComponent<Light>().enabled = false;
        HDRISkyExposure.exposure.value = 6f;
        ControlLights(true);
    }

    void ControlLights(bool onOrOff)
    {
        Light spotlight = GameObject.Find("SpotLight").GetComponent<Light>();
        Light pointLight = GameObject.Find("PointLight").GetComponent<Light>();
        Light pointLight2 = GameObject.Find("PointLight2").GetComponent<Light>();
        spotlight.enabled = onOrOff;
        pointLight.enabled = onOrOff;
        pointLight2.enabled = onOrOff;
    }
}
