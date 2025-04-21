using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsLightManage : MonoBehaviour
{
    [SerializeField]private List<TurnLight> lightList1F;
    [SerializeField] private List<TurnLight> lightList2F;
    [SerializeField] private List<TurnLight> lightListBM;
    private void Start()
    {
        foreach (var light in lightList1F)
        {
            light.SetManager(this);
        }
        Debug.Log(lightList1F[0].name);
        foreach (var light in lightList2F)
        {
            light.SetManager(this);
        }
        foreach (var light in lightListBM)
        {
            light.SetManager(this);
        }
    }

    public void TurnF1Light()
    {
        foreach (var light in lightList1F)
        { 
            light.SetLightsIntensity();
        }
    }
    public void TurnF2Light()
    {
        foreach (var light in lightList2F)
        {
            light.SetLightsIntensity();
        }
    }
    public void TurnBMLight()
    {
        foreach (var light in lightListBM)
        {
            light.SetLightsIntensity();
        }
    }

    public void TurnAllFloor()
    {
        TurnF1Light();
        TurnF2Light();
        TurnBMLight();
    }
}
