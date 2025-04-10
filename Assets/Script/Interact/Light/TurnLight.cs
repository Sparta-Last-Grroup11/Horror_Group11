using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLight : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private float intensity;
    private List<Light> lightsList;
    private bool isTurnOn;
    private void Start()
    {
        lightsList = new List<Light>();
        if (parentObject != null)
            GetChildsLights(parentObject.transform);
    }

    void GetChildsLights(Transform root)
    {
        int childCount = root.childCount;
        for(int i = 0; i < childCount; i++)
        {
            Transform child = root.GetChild(i);
            int grandChildCount = child.childCount;

            for(int j = 0; j < grandChildCount; j++)
            {
                Transform grandChild = child.GetChild(j);
                Light light = grandChild.GetComponent<Light>();
                if(light != null)
                    lightsList.Add(light);
            }
        }
    }

    void SetLightsIntensity()
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
            }
        }
    }

    public void OnInteraction()
    {
        SetLightsIntensity();
    }

    
}
