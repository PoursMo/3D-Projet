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
        //Référence a l'UI qui affiche le score
        scoreUI = GameObject.Find("UI").transform.Find("ScoreImage").transform.Find("Score").gameObject.GetComponent<Score>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //layer 7 = joueur
        if(other.gameObject.layer == 7)
        {
            //la pièce est ramassée : un son est joué, l'UI affichant le score est updatée et la pièce est détruite
            AudioSource.PlayClipAtPoint(coinAudio, transform.position);
            scoreUI.scoreCount += scoreValue;
            scoreUI.UpdateScore();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
