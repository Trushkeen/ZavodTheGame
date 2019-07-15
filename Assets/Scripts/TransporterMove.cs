using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterMove : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
        var pos = obj.transform.position;
        var rot = obj.transform.rotation;
        if (other.tag == "Carriable")
        {
            obj.transform.position += gameObject.transform.forward * -(1 - other.gameObject.GetComponent<Rigidbody>().mass / 10) * Time.deltaTime;
            //rot = this.gameObject.transform.rotation;
        }
    }
}
