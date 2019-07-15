using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CrafterLogic : MonoBehaviour
{
    public GameObject Comp1;
    public GameObject Comp2;
    public GameObject SpawnPoint;
    private Stopwatch sw = new Stopwatch();

    private void Start()
    {
        sw.Restart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Carriable")
        {
            if (Comp1 == null)
            {
                Comp1 = other.gameObject;
                Comp1.transform.position = new Vector3(-1000, -1000, -1000);
            }
            else if (Comp2 == null)
            {
                Comp2 = other.gameObject;
                Comp2.transform.position = new Vector3(-1000, -1000, -1000);
            }
        }
    }

    private void Update()
    {
        if (sw.Elapsed.Seconds > 5)
        {
            if (Comp1 != null && Comp2 != null)
            {
                print(transform.parent.gameObject.name);
                var list = transform.parent.gameObject.GetComponent<ListOfCraftableObjests>();
                if ((Comp1.name.Contains("Nails") && Comp2.name.Contains("Wood"))
                    || (Comp2.name.Contains("Nails") && Comp1.name.Contains("Wood")))
                {
                    CreateItem(list.GetCraftableObject(CraftableObjects.Stool));
                }
                else
                {
                    CreateItem(list.GetCraftableObject(CraftableObjects.Junk));
                }
            }
        }
    }

    private void CreateItem(GameObject obj)
    {
        print(obj.name);
        var newobj = Instantiate(obj, this.transform.parent);
        newobj.transform.position = SpawnPoint.transform.position;
        Destroy(Comp1);
        Destroy(Comp2);
        Comp1 = null;
        Comp2 = null;
    }
}
