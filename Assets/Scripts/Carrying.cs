using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrying : MonoBehaviour
{
    public GameObject TakenObject;
    private bool ObjectTaken = false;
    private string TakenObjectName = string.Empty;
    public GameObject Holder;
    public GameObject PlayerCamera;
    private enum Rotation { Standard = 0, Ninety = 1, OneEighty = 2, TwoSeventy = 3 }
    private Rotation ObjRot = Rotation.Standard;

    public Text PickupText;

    private void Start()
    {
        PickupText = GameObject.FindGameObjectWithTag("TextTaken").GetComponent<Text>();
        PickupText.text = "";
    }

    void Update()
    {
        if (ObjectTaken && TakenObject != null)
        {
            var holderRot = Holder.transform.rotation;
            var camRot = PlayerCamera.transform.forward;
            TakenObject.transform.position = new Vector3(Holder.transform.position.x,
                Holder.transform.position.y, Holder.transform.position.z);
            switch (ObjRot)
            {
                case Rotation.Standard:
                    TakenObject.transform.rotation = Quaternion.Euler(camRot);
                    break;
                case Rotation.Ninety:
                    TakenObject.transform.rotation = Quaternion.Euler(holderRot.x + 90, holderRot.y, holderRot.z);
                    break;
                case Rotation.OneEighty:
                    TakenObject.transform.rotation = Quaternion.Euler(holderRot.x + 180, holderRot.y, holderRot.z);
                    break;
                case Rotation.TwoSeventy:
                    TakenObject.transform.rotation = Quaternion.Euler(holderRot.x + 270, holderRot.y, holderRot.z);
                    break;
            }
            TakenObject.GetComponent<Collider>().enabled = false;
        }
        if (ObjectTaken && Input.GetKey(KeyCode.G))
        {
            if (TakenObject != null)
                TakenObject.GetComponent<Collider>().enabled = true;
            TakenObject = null;
        }
        if (ObjectTaken && Input.GetKeyUp(KeyCode.R))
        {
            if (ObjRot == Rotation.TwoSeventy)
                ObjRot = Rotation.Standard;
            else
                ObjRot++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Carriable" && Input.GetKey(KeyCode.E) && TakenObject == null)
        {
            TakenObject = other.gameObject;
            ObjectTaken = true;
        }
        if (other.tag == "Carriable" && TakenObject == null)
        {
            TakenObjectName = other.name;
            PickupText.text = "[E] - Take " + TakenObjectName;
        }
        else if (TakenObject != null)
        {
            PickupText.text = "[G] - Drop " + TakenObjectName + "\n\n[R] - Rotate";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TakenObject == null)
        {
            PickupText.text = string.Empty;
            TakenObjectName = string.Empty;
        }
    }
}
