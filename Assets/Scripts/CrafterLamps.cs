using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterLamps : MonoBehaviour
{
    [SerializeField]
    private Light Lamp1;
    [SerializeField]
    private Light Lamp2;

    private void Start()
    {
        Lamp1.intensity = 0;
        Lamp2.intensity = 0;
    }

    private void Update()
    {
        if (CrafterLogic.Comp1 != null)
        {
            Lamp1.intensity = 2;
        }
        if(CrafterLogic.Comp2 != null)
        {
            Lamp2.intensity = 2;
        }
    }
}
