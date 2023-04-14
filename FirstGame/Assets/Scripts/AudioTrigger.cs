using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioFile;
    private AudioSource audioPlayer = null;
    
    private float multiPlayDelay = 1;
    private double timer = 0.0;

    void Start()
    {
        audioPlayer = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter (Collider col)
    {
        if ((col.gameObject.tag == "Player") && (timer >= multiPlayDelay))
        {
            if ((audioFile != null) && (audioPlayer != null))
                audioPlayer.PlayOneShot (audioFile);
        }
    }
}
