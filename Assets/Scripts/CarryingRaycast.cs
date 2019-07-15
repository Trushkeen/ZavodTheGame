using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarryingRaycast : MonoBehaviour
{
    private Transform CamPos;
    private RaycastHit hit;
    public GameObject TakenObject;
    private Collider TakenObjectCollider;
    private string TakenObjectName = string.Empty;
    public GameObject Holder;
    public GameObject PlayerCamera;
    private enum Rotation { Standard = 0, Ninety = 1, OneEighty = 2, TwoSeventy = 3 }
    private Rotation ObjRot = Rotation.Standard;
    private GameObject Builder1Menu;
    private bool ShowDeveloperInfo = true;

    public static GameObject FacingBuilder;
    public Text PickupText;
    // Start is called before the first frame update
    void Start()
    {
        CamPos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().gameObject.transform;
        hit = new RaycastHit();
        PickupText = GameObject.FindGameObjectWithTag("TextTaken").GetComponent<Text>();
        PickupText.text = "";
        TakenObject = null;
        Cursor.visible = false;
        Builder1Menu = GameObject.FindGameObjectWithTag("Builder1Menu").gameObject;
        Builder1Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F3) && ShowDeveloperInfo)
        {
            ShowDeveloperInfo = false;
        }
        else if (Input.GetKeyUp(KeyCode.F3) && !ShowDeveloperInfo)
        {
            ShowDeveloperInfo = true;
        }
        RotationLogic();
        DetectObjects();
        UpdDeveloperInfo();
    }

    private void UpdDeveloperInfo()
    {
        if (ShowDeveloperInfo)
        {
            RaycastHit rayhit;
            int layers = ~(1 << 8);
            if (Physics.Raycast(CamPos.position, CamPos.forward, out rayhit, 10.0F, layers, QueryTriggerInteraction.Ignore))
            {
                var devinfo = GameObject.FindGameObjectWithTag("DeveloperInfo").GetComponent<Text>();
                devinfo.text = $"Facing at {rayhit.transform.gameObject.name}";
                if (rayhit.transform.gameObject.tag == "Builder")
                {
                    try
                    {
                        devinfo.text += $"\nBuildable Object: {rayhit.transform.gameObject.GetComponent<Manufacturing>().BuildableObj.name}";
                    }
                    catch (UnassignedReferenceException)
                    {
                        devinfo.text += $"\nBuildable Object wasn't assigned";
                    }
                }
                devinfo.text += "\nSuper prealpha build 00006";
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("DeveloperInfo").GetComponent<Text>().text = "";
        }
    }

    private void DetectObjects()
    {
        int layers = ~(1 << 8);
        if (Physics.Raycast(CamPos.position, CamPos.forward, out hit, 5.0F, layers, QueryTriggerInteraction.Ignore))
        {
            //print("Facing " + hit.transform.gameObject.name);
            var go = hit.transform.gameObject;
            if (go.tag == "Carriable" && TakenObject == null)
            {
                PickupText.text = "[E] - Take " + go.name;
                if (Input.GetKeyUp(KeyCode.E))
                {
                    TakenObject = go;
                }
            }
            else if (go.tag == "Builder")
            {
                PickupText.text = "[E] - To change manufacturing item\n\n[L] - Sell " + go.name + " for $" + FactoryMachines.GetPrice(go, true);
                if (Input.GetKeyUp(KeyCode.E))
                {
                    var builderScript = go.GetComponent<Manufacturing>();
                    Manufacturing.BuilderToChangeType = go;
                    print("You're about to change type of " + go.name);
                    Builder1Menu.SetActive(true);
                    MouseMove.FreezeCamera();
                    Cursor.visible = true;
                }
                if (Input.GetKeyUp(KeyCode.L))
                {
                    SellItem(go);
                }
            }
            else if (go.tag == "Interactable")
            {
                PickupText.text = "[L] - Sell " + go.name + " for $" + FactoryMachines.GetPrice(go, true);
                if (Input.GetKeyUp(KeyCode.L))
                {
                    SellItem(go);
                }
            }
            else if (go.tag == "LinkToParent")
            {
                var goParent = go.GetComponentInParent<Transform>().parent.gameObject;
                PickupText.text = "[L] - Sell " + goParent.name + " for $" + FactoryMachines.GetPrice(goParent, true);
                if (Input.GetKeyUp(KeyCode.L))
                {
                    SellItem(goParent);
                }
            }
        }
        else if (TakenObject == null)
        {
            PickupText.text = string.Empty;
        }
    }

    private void SellItem(GameObject go)
    {
        Money.AddMoney(FactoryMachines.GetPrice(go, true));
        Destroy(go);
    }

    private void RotationLogic()
    {
        var holderRot = Holder.transform.rotation;
        if (TakenObject != null && Input.GetKeyUp(KeyCode.R))
        {
            if (ObjRot == Rotation.TwoSeventy)
                ObjRot = Rotation.Standard;
            else
                ObjRot++;
        }
        if (TakenObject != null)
        {
            TakenObjectCollider = TakenObject.gameObject.GetComponent<Collider>();
            TakenObjectCollider.enabled = false;
            TakenObject.transform.position = Holder.transform.position;
            TakenObject.transform.rotation = Holder.transform.rotation;
            PickupText.text = "[G] - Drop " + TakenObject.name + "\n\n[R] - Rotate " + TakenObject.name;
            switch (ObjRot)
            {
                case Rotation.Standard:
                    TakenObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Rotation.Ninety:
                    TakenObject.transform.rotation = Quaternion.Euler(90, 0, 0);
                    break;
                case Rotation.OneEighty:
                    TakenObject.transform.rotation = Quaternion.Euler(180, 0, 0);
                    break;
                case Rotation.TwoSeventy:
                    TakenObject.transform.rotation = Quaternion.Euler(270, 0, 0);
                    break;
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                TakenObjectCollider.enabled = true;
                TakenObjectCollider = null;
                TakenObject = null;
                PickupText.text = string.Empty;
            }
        }
    }
}
