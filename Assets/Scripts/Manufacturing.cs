using System.Diagnostics;
using UnityEngine;

public class Manufacturing : MonoBehaviour
{
    Stopwatch sw = new Stopwatch();

    public static GameObject BuilderToChangeType;
    public static GameObject RequestedObj;
    public GameObject BuildableObj;
    public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
        sw.Restart();
    }

    public void SetRequestedObj(GameObject obj)
    {
        RequestedObj = obj;
    }

    public void SetBuildableObject(GameObject obj)
    {
        BuildableObj = obj;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Builder")
        {
            if (sw.ElapsedMilliseconds > 5000)
            {
                if (BuildableObj != null)
                    CreateItem();
                else
                    sw.Restart();
            }
        }
    }

    private void CreateItem()
    {
        GameObject obj = Instantiate(BuildableObj, this.transform);
        print("Created " + obj.name + " on " + name + " | Buildable obj: " + BuildableObj);
        obj.transform.position = Spawner.transform.position;
        sw.Restart();
    }

    public void HideMenu()
    {
        if (BuildableObj != null)
            print("Set " + BuildableObj.name + " to " + name);
        GameObject.FindGameObjectWithTag("Builder1Menu").SetActive(false);
        BuilderToChangeType.GetComponent<Manufacturing>().BuildableObj = RequestedObj;
        MouseMove.UnfreezeCamera();
        Cursor.visible = false;
    }

    public void CancelInteraction()
    {
        GameObject.FindGameObjectWithTag("Builder1Menu").SetActive(false);
        BuilderToChangeType = null;
        MouseMove.UnfreezeCamera();
        Cursor.visible = false;
    }

    public void StopManufacturing()
    {
        RequestedObj = null;
    }

    public static void ShowBuilderMenu(GameObject menu)
    {
        menu.SetActive(true);
        MouseMove.FreezeCamera();
        Cursor.visible = true;
    }
}
