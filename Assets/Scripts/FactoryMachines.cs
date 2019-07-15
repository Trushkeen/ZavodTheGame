using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryMachines : MonoBehaviour
{
    private static int Builder1Cost = 1000;
    private static int TransporterCost = 200;
    private static int SellerCost = 1000;
    private static int CrafterCost = 2000;
    public static double GetPrice(GameObject obj, bool SellPrice = false)
    {
        var name = obj.name;
        if (name.Contains("Builder1"))
        {
            if (!SellPrice) return Builder1Cost; else return Builder1Cost / 2;
        }
        else if (name.Contains("Transporter1"))
        {
            if (!SellPrice) return TransporterCost; else return TransporterCost / 2;
        }
        else if (name.Contains("Seller1"))
        {
            if (!SellPrice) return SellerCost; else return SellerCost / 2;
        }
        else if (name.Contains("Crafter1"))
        {
            if (!SellPrice) return CrafterCost; else return CrafterCost / 2;
        }
        else return 0;
    }
}
