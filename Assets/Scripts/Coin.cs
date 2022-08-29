using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    Score scoreUI;
    public float scoreValue;
    public AudioClip coinAudio;
    private void Start()
    {
        //R�f�rence a l'UI qui affiche le score
        scoreUI = GameObject.Find("UI").transform.Find("ScoreImage").transform.Find("Score").gameObject.GetComponent<Score>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //layer 7 = joueur
        if(other.gameObject.layer == 7)
        {
            //la pi�ce est ramass�e : un son est jou�, l'UI affichant le score est updat�e et la pi�ce est d�truite
            AudioSource.PlayClipAtPoint(coinAudio, transform.position);
            scoreUI.scoreCount += scoreValue;
            scoreUI.UpdateScore();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
