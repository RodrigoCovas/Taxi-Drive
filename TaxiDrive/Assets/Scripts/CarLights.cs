using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    public enum Type
    {
        Front,
        Back
    }

    [System.Serializable]
    public struct Light
    {
        public Type type;
        public GameObject lightObject;
        public Material lightMaterial;
    }

    public List<Light> lights;
    public bool FrontLightsOn;
    public bool BackLightsOn;
    public bool isPlayer = false;
    public GameObject spriteOff;
    public GameObject spriteOn;

    void Start()
    {
        FrontLightsOn = false;
        BackLightsOn = true;

        if (!isPlayer)
        {
            FrontLightsOn = true;
            OperateFrontLights();
        }
        else
        {
            spriteOff.SetActive(true);
            spriteOn.SetActive(false);
        }
    }

    public void OperateFrontLights()
    {
        if (!isPlayer)
            return;

        FrontLightsOn = !FrontLightsOn;

        if (FrontLightsOn)
        {
            foreach (Light light in lights)
            {
                if (light.type == Type.Front && light.lightObject.activeInHierarchy == false)
                {
                    light.lightObject.SetActive(true);
                    spriteOff.SetActive(false);
                    spriteOn.SetActive(true);
                }
            }
        }
        else
        {
            foreach (Light light in lights)
            {
                if (light.type == Type.Front && light.lightObject.activeInHierarchy == true)
                {
                    light.lightObject.SetActive(false);
                    spriteOff.SetActive(true);
                    spriteOn.SetActive(false);
                }
            }
        }
    }

    public void OperateBackLights()
    {
        if (BackLightsOn)
        {
            foreach (Light light in lights)
            {
                if (light.type == Type.Back && light.lightObject.activeInHierarchy == false)
                {
                    light.lightObject.SetActive(true);
                }
            }
            BackLightsOn = false;
        }
        else
        {
            foreach (Light light in lights)
            {
                if (light.type == Type.Back && light.lightObject.activeInHierarchy == true)
                {
                    light.lightObject.SetActive(false);
                }
            }
            BackLightsOn = true;
        }
    }
}
