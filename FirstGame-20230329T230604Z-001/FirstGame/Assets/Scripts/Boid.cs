using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Neighborhood neighborhood;
    private Rigidbody rigid;

    void Awake(){
        neighborhood = GetComponent<Neighborhood>();
        rigid = GetComponent<Rigidbody>();

        vel = Random.onUnitSphere * Spawner.SETTINGS.velocity;

        LookAhead();
        Colorize();
    }

    void FixedUpdate(){
        BoidSettings bSet = Spawner.SETTINGS;

        Vector3 sumVel = Vector3.zero;

        Vector3 delta = Attractor.POS - pos;

        if (delta.magnitude > bSet.attractPushDist) {
            sumVel += delta.normalized * bSet.attractPull;
        }
        else{
            sumVel -= delta.normalized * bSet.attractPush;
        }

        Vector3 velAvoid = Vector3.zero;
        Vector3 tooNearPos = neighborhood.avgNearPos;
        if (tooNearPos != Vector3.zero){
            velAvoid = pos - tooNearPos;
            velAvoid.Normalize();
            sumVel += velAvoid * bSet.nearAvoid;
        }

        Vector3 velAlign = neighborhood.avgVel;
        if(velAlign != Vector3.zero){
            velAlign.Normalize();
            sumVel += velAlign * bSet.velMatching;
        }

        Vector3 velCenter = neighborhood.avgPos;
        if (velCenter != Vector3.zero){
            velCenter.Normalize();
            sumVel += velCenter * bSet.flockCentering;
        }

        sumVel.Normalize();
        vel = Vector3.Lerp(vel.normalized, sumVel, bSet.velocityEasing);
        vel *= bSet.velocity;
        LookAhead();
    }
    
    void LookAhead(){
        transform.LookAt(pos + rigid.velocity);
    }

    void Colorize(){
        Color randColor = Random.ColorHSV( 0, 1, 0.5f, 1, 0.5f, 1 );

        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends) {
            r.material.color = randColor;
        }

        TrailRenderer trend = GetComponent<TrailRenderer>();
        trend.startColor = randColor;
        randColor.a = 0;
        trend.endColor = randColor;
        trend.endWidth = 0;
     }

     public Vector3 pos{
        get { return transform.position; }
        private set { transform.position = value; }
     }

     public Vector3 vel{
        get { return rigid.velocity; }
        private set { rigid.velocity = value; }
     }
}
