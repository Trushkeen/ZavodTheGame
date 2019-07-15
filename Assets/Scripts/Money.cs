using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private static double PlayerMoney = 100000;

    void Start()
    {
        UpdateText();
    }

    public static void AddMoney(double value)
    {
        PlayerMoney += value;
        UpdateText();
    }

    public static bool DiscardMoney(double value)
    {
        if (value > PlayerMoney)
        {
            return false;
        }
        else
        {
            PlayerMoney -= value;
            UpdateText();
            return true;
        }
    }

    public static double GetBalance()
    {
        return PlayerMoney;
    }

    public static void UpdateText()
    {
        GameObject.FindGameObjectWithTag("Money").GetComponent<Text>().text = GetBalance().ToString();
    }
}
