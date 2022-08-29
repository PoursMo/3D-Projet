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
        //r�f�rence au texte pour afficher le score
        txt = GetComponent<Text>();
    }
    //fonction appell�e lorsqu'une pi�ce est ramass�e pour afficher le nouveau score
    public void UpdateScore()
    {
        txt.text = scoreCount.ToString();
    }
}
