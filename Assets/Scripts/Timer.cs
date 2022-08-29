using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public AudioClip audioClip;
    Text txt;
    public GameObject gameOverObj;
    public float timer;
    bool gameOver;
    private void Start()
    {
        //reférence au texte pour afficher le timer
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        //si le timer arrive a 0, la fonction GameOver est appelée, sinon décrémentation du timer avec Time.deltaTime
        if ((timer <= 0 && !gameOver) || Input.GetKeyDown(KeyCode.R))
        {
            gameOver = true;
            GameOver();
        }
        else
        {
            timer -= Time.deltaTime;
            //F2 = format du string affiché avec 2 chiffres après la virgule
            txt.text = timer.ToString("F2");
        }
    }
    void GameOver()
    {
        //le timer est arrivé à 0, un son est joué, le jeu est mis en pause et un élément d'UI avec un bouton restart est affiché
        AudioSource.PlayClipAtPoint(audioClip, GameObject.Find("George").transform.position);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverObj.SetActive(true);
    }
}
