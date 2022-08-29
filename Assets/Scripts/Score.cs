using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text txt;
    [HideInInspector]
    public float scoreCount;
    private void Start()
    {
        //référence au texte pour afficher le score
        txt = GetComponent<Text>();
    }
    //fonction appellée lorsqu'une pièce est ramassée pour afficher le nouveau score
    public void UpdateScore()
    {
        txt.text = scoreCount.ToString();
    }
}
