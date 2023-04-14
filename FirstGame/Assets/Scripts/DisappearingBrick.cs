using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBrick : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(this.transform.gameObject);
        }
    }
}