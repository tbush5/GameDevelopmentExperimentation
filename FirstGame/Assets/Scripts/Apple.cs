using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float     bottomY = -20f;
    public int points = 100;
 
    void Update () {
        if ( transform.position.y < bottomY ) {
            Destroy( this.gameObject );

            if (this.gameObject.tag == "Apple"){
                ApplePicker apscript = Camera.main.GetComponent<ApplePicker>();
                apscript.AppleMissed();
            }
        }
    }
}
