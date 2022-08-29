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
        //ref�rence au texte pour afficher le timer
        txt = GetComponent<Text>();
    }
    private void Update()
    {
        //si le timer arrive a 0, la fonction GameOver est appel�e, sinon d�cr�mentation du timer avec Time.deltaTime
        if ((timer <= 0 && !gameOver) || Input.GetKeyDown(KeyCode.R))
        {
            gameOver = true;
            GameOver();
        }
        else
        {
            timer -= Time.deltaTime;
            //F2 = format du string affich� avec 2 chiffres apr�s la virgule
            txt.text = timer.ToString("F2");
        }
    }
    void GameOver()
    {
        //le timer est arriv� � 0, un son est jou�, le jeu est mis en pause et un �l�ment d'UI avec un bouton restart est affich�
        AudioSource.PlayClipAtPoint(audioClip, GameObject.Find("George").transform.position);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverObj.SetActive(true);
    }
}
