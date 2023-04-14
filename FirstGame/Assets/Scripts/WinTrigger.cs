using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public TMPro.TMP_Text winText = null;
    public string message = "You Win!";
    public AudioClip winSound = null;
    public bool audioRestart = true;
    public float restartDelay = 5.0f;

    private double timer = 0.0;
    private bool playerWon = false;
    private AudioSource audioPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        playerWon = false;
        timer = 0;

        if (winText != null)
            winText.SetText("");
        else
            Debug.LogWarning("Need a TMPro Text box to display message");

        audioPlayer = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (audioRestart && playerWon && (timer > restartDelay)){
            timer = 0;
            playerWon = false;

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void OnTriggerEnter (Collider col){
        if (!playerWon && (col.gameObject.tag == "Player")){
            Debug.Log(message);

            if (winText != null)
                winText.SetText(message);

            playerWon = true;
            timer = 0.0;
            
            if (audioPlayer != null && winSound != null){
                audioPlayer.PlayOneShot(winSound);
            }
        }
    }
}
