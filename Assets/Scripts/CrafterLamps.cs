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
        var parent = Lamp1.transform.parent.transform.parent;
        var Comp1 = parent.GetComponentInChildren<CrafterLogic>().Comp1;
        var Comp2 = parent.GetComponentInChildren<CrafterLogic>().Comp2;
        if (Comp1 != null)
        {
            Lamp1.intensity = 2;
        }
        else Lamp1.intensity = 0;
        if (Comp2 != null)
        {
            Lamp2.intensity = 2;
        }
        else Lamp2.intensity = 0;
    }
}
