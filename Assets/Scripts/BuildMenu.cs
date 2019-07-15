using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    private GameObject BuildingMenu;
    private Transform CamPos;
    private RaycastHit hit;
    private bool HoldingItem = false;
    private GameObject CurrentObj;
    private enum Rotation { Standard = 0, Ninety = 1, OneEighty = 2, TwoSeventy = 3 }
    private Rotation ObjRot = Rotation.Standard;

    [SerializeField]
    private GameObject WhatToBuild;

    [SerializeField]
    private GameObject BuildingPositioner;

    void Start()
    {
        CamPos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>().gameObject.transform;
        BuildingMenu = GameObject.FindGameObjectWithTag("BuildMenu");
        BuildingMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            SetMenuActive();
        }
        if (WhatToBuild != null)
        {
            if (!HoldingItem)
            {
                if (Money.DiscardMoney(FactoryMachines.GetPrice(WhatToBuild)))
                {
                    CurrentObj = Instantiate(WhatToBuild);
                    CurrentObj.tag = "Prebuild";
                    HoldingItem = true;
                }
                else Reset();
            }
            CurrentObj.transform.position = BuildingPositioner.transform.position;
            RotationLogic();
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (CurrentObj.name.Contains("Builder"))
                {
                    CurrentObj.tag = "Builder";
                }
                else
                {
                    CurrentObj.tag = "Interactable";
                }
                Reset();
            }
        }
    }

    public void Reset()
    {
        WhatToBuild = null;
        HoldingItem = false;
        AfterSelecting();
    }

    public void SetMenuActive()
    {
        BuildingMenu.SetActive(true);
        MouseMove.FreezeCamera();
        Cursor.visible = true;
    }

    public void SetObjectToBuild(GameObject obj)
    {
        WhatToBuild = obj;
    }

    public void AfterSelecting()
    {
        BuildingMenu.SetActive(false);
        MouseMove.UnfreezeCamera();
        Cursor.visible = false;
    }

    private void RotationLogic()
    {
        var holderRot = BuildingPositioner.transform.rotation;
        if (CurrentObj != null && Input.GetKeyUp(KeyCode.R))
        {
            if (ObjRot == Rotation.TwoSeventy)
                ObjRot = Rotation.Standard;
            else
                ObjRot++;
        }
        if (CurrentObj != null)
        {
            switch (ObjRot)
            {
                case Rotation.Standard:
                    CurrentObj.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Rotation.Ninety:
                    CurrentObj.transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case Rotation.OneEighty:
                    CurrentObj.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case Rotation.TwoSeventy:
                    CurrentObj.transform.rotation = Quaternion.Euler(0, 270, 0);
                    break;
            }
        }
    }
}
