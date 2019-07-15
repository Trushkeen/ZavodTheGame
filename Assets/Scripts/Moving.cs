using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public GameObject Player;
    public int Speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        Player = (GameObject)this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Player.transform.position += Player.transform.forward * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Player.transform.position += Player.transform.forward * -Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.position += Player.transform.right * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Player.transform.position += Player.transform.right * -Speed * Time.deltaTime;
        }
    }
}
