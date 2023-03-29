using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHello : MonoBehaviour
{
    public string userName = "Name";
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log ("Hello, " + userName + "!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
