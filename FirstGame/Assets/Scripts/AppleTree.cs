using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public GameObject applePrefab;

    public List<float> speedLevels = new List<float>() {5f, 10f, 20f, 50f};
    public List<float> appleDropDelayLevels = new List<float>() {1f, 0.9f, 0.7f, 0.5f};
    public List<float> changeDirChanceLevels = new List<float>() {0.01f, 0.02f, 0.03f, 0.05f};
    private int level = 0;
    private float speed;
    private float appleDropDelay;
    private float changeDirChance;

    public float leftAndRightEdge = 10f;

    public GameObject goldenApplePrefab;
    public GameObject posionApplePrefab;
    public GameObject rottenApplePrefab;
    public float specialAppleChance = 0.1f;

    public ScoreCounter scoreCounter;

    void Start() {
        speed = speedLevels[level];
        appleDropDelay = appleDropDelayLevels[level];
        changeDirChance = changeDirChanceLevels[level];
        
        Invoke("DropApple", 2f);

        GameObject scoreGO = GameObject.Find( "ScoreCounter" );
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    void DropApple() {
        GameObject appleType = applePrefab;
        if ( Random.value < specialAppleChance ) {
            int appletype = Random.Range(1,4);
            if (appletype == 1){
                appleType = goldenApplePrefab;
            }
            if (appletype == 2){
                appleType = posionApplePrefab;
            }
            if (appletype == 3){
                appleType = rottenApplePrefab;
            }
        }

        GameObject apple = Instantiate<GameObject>(appleType);
        apple.transform.position = transform.position;
        Invoke( "DropApple", appleDropDelay);

    }

    void Update () {
        if (scoreCounter.score >= 2000 && level < 1){
            level = 1;
            speed = speedLevels[level];
            appleDropDelay = appleDropDelayLevels[level];
        }
        if (scoreCounter.score >= 5000 && level < 2){
            level = 2;
            speed = speedLevels[level];
            appleDropDelay = appleDropDelayLevels[level];
        }
        if (scoreCounter.score >= 10000 && level < 3){
            level = 3;
            speed = speedLevels[level];
            appleDropDelay = appleDropDelayLevels[level];
        }

        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if ( pos.x < -leftAndRightEdge ) {
            speed = Mathf.Abs(speed);               
        } else if ( pos.x > leftAndRightEdge ) {
            speed = -Mathf.Abs(speed);               
        }
    }

    void FixedUpdate(){
        if ( Random.value < changeDirChance ) {
            speed *= -1; 
        } 
    }
}
