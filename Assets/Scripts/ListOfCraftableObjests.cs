using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfCraftableObjests : MonoBehaviour
{
    [SerializeField]
    private GameObject Stool;
    [SerializeField]
    private GameObject Junk;

    public GameObject GetCraftableObject(CraftableObjects obj)
    {
        switch (obj)
        {
            case CraftableObjects.Stool: return Stool;
            case CraftableObjects.Junk: return Junk;
            default: return null;
        }
    }
}

public enum CraftableObjects
{
    Junk, Stool
}
