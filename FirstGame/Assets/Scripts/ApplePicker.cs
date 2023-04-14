using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ApplePicker : MonoBehaviour
{
    public GameObject basketPrefab;
    public int         numBaskets     = 3;
    public float       basketBottomY  = -14f;
    public float       basketSpacingY = 2f;
    public List<GameObject> basketList;

    void Start () {
        basketList = new List<GameObject>();
        for (int i=0; i <numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>( basketPrefab );
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + ( basketSpacingY * i );
            tBasketGO.transform.position = pos;
            basketList.Add( tBasketGO );
        }
    }

    public void AppleMissed(){
        GameObject[] appleArray=GameObject.FindGameObjectsWithTag("Apple");
        appleArray = appleArray.Concat(GameObject.FindGameObjectsWithTag("PosionApple")).ToArray();
        appleArray = appleArray.Concat(GameObject.FindGameObjectsWithTag("RottenApple")).ToArray();
        foreach ( GameObject tempGO in appleArray ) {
            Destroy( tempGO );
        }
        int basketIndex = basketList.Count -1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        if(basketList.Count == 0){
            SceneManager.LoadScene("ApplePicker");
        }
    }
}