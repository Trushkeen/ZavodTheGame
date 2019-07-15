using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterLogic : MonoBehaviour
{
    public static GameObject Comp1;
    public static GameObject Comp2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Carriable")
        {
            if (Comp1 == null)
            {
                Comp1 = other.gameObject;
                Destroy(other, 0F);
            }
            else if (Comp2 == null)
            {
                Comp2 = other.gameObject;
                Destroy(other, 0F);
            }
        }
    }

    private void Update()
    {
        
    }
}
