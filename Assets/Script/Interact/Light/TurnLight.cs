using System.Collections.Generic;
using UnityEngine;

public class TurnLight : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private float intensity;
    public float range;
    [SerializeField] private List<Light> lightsList;
    private bool isTurnOn;
    private bool isPowerOn = false;
    private void Start()
    {
        lightsList = new List<Light>();
        if (parentObject != null)
            GetChildsLights(parentObject.transform);
    }
    void GetChildsLights(Transform root)
    {
        lightsList = new List<Light>(root.GetComponentsInChildren<Light>(true));
        foreach (Light light in lightsList)
        {
            light.intensity = 0;
            light.range = range;
        }
    }

    public void SetLightsIntensity()
    {
        if (isTurnOn)
        {
            isTurnOn = false;
            foreach (Light light in lightsList)
            {
                light.intensity = 0;
            }
        }
        else
        {
            isTurnOn = true;
            foreach (Light light in lightsList)
            {
                light.intensity = intensity;
                light.range = range;
            }
        }
    }

    public void OnInteraction()
    {
        if (!isPowerOn) return;
        SetLightsIntensity();
    }

    public void OnPower()
    {
        isPowerOn = true;
    }
}
