using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    public int score = 0;
    private Text uiText;
    
    void Start() {
        uiText = GetComponent<Text>();
    }

    void Update() {
        uiText.text = score.ToString( "#,0" );
    }
}