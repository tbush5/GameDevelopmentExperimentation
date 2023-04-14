using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public Color waterColor = new Color (0, 0.2f, 0.3f, 0);
    public float waterDensity = 0.1f;
    public float surfaceBrightness = 1.5f;

    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private float defaultFogEndDistance;
    private Material defaultSkybox;

    private Camera playerCam = null;
    
    private bool effectApplied = false;
    private bool underwaterTrigger = false;
    private float waterLevel = 0.0f;

    void Start()
    {
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultFogEndDistance = RenderSettings.fogEndDistance;
        defaultSkybox = RenderSettings.skybox;

        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
            playerCam = playerObject.transform.Find("Main Camera").GetComponent<Camera>();

        waterLevel = transform.position.y;

        waterEffectOn();
        waterEffectOff();
    }

    // Update is called once per frame
    void Update()
    {
        if ( !effectApplied && underwaterTrigger && (playerCam.transform.position.y < waterLevel))
        {
            effectApplied = true;
            waterEffectOn();
        }
        else if (effectApplied && underwaterTrigger && (playerCam.transform.position.y >= waterLevel))
        {
            effectApplied = false;
            waterEffectOff();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerBehaviorShooting>().underwater = true;
            underwaterTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerBehaviorShooting>().underwater = false;
            underwaterTrigger = false; 
            effectApplied = false;
            waterEffectOff();
        }
    }

    void waterEffectOn()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterDensity;
        RenderSettings.fogEndDistance = 600;
        RenderSettings.skybox = null;
    }

    void waterEffectOff()
    {
        RenderSettings.fog = defaultFog;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
        RenderSettings.fogEndDistance = defaultFogEndDistance;
        RenderSettings.skybox = defaultSkybox;
    }
}
