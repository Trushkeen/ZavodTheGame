using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            var goName = other.gameObject.name;
            var text = GameObject.FindGameObjectWithTag("Money").GetComponent<Text>();
            if (goName.Contains("Key"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(10);
                text.text = Money.GetBalance().ToString();
            }
            else if (goName.Contains("Nails"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(10);
                text.text = Money.GetBalance().ToString();
            }
            else if (goName.Contains("Barrel"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(50);
                text.text = Money.GetBalance().ToString();
            }
            else if (goName.Contains("Pallet"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(20);
                text.text = Money.GetBalance().ToString();
            }
            else if (goName.Contains("Stool"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(40);
                text.text = Money.GetBalance().ToString();
            }
            else if (goName.Contains("Wood"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(40);
                text.text = Money.GetBalance().ToString();
            }
            else if (goName.Contains("Junk"))
            {
                Destroy(other.gameObject, 1.0F);
                Money.AddMoney(1);
                text.text = Money.GetBalance().ToString();
            }
        }
    }
}
