using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    float timer;
    public float timerMax = 5;
    Movement player;
    public float speedBoost = 4;
    Slider slider;
    bool isTimerOn;
    public AudioClip boosterAudio;
    public GameObject trail;
    GameObject tr;
    private void Start()
    {
        //R�f�rence a l'�l�ment d'UI "slider" pour repr�senter le timer du booster, avec sa maxValue �tant de la taille de notre timer
        slider = GameObject.Find("UI").transform.Find("Booster").GetComponent<Slider>();
        slider.maxValue = timerMax;
    }
    private void Update()
    {
        //gestion du timer, une fois � 0 tout revient comme avant
        if (isTimerOn)
        {
            timer -= Time.deltaTime;
            slider.value = timer;
        }
        if (timer <= 0 && isTimerOn)
        {
            player.speed -= speedBoost;
            slider.gameObject.SetActive(false);
            isTimerOn = false;
            Destroy(tr);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //layer 7 = le joueur
        if (other.gameObject.layer == 7)
        {
            player = other.GetComponent<Movement>();
            //v�rification si le slider est d�ja activ� le joueur est d�ja boost� donc ne peut en reprendre un
            if (slider.gameObject.activeInHierarchy == false)
            {
                //le booster est pris : un son est jou�, la vitesse du joueur est augment�e, le slider UI est activ�, le timer est commenc�
                //le booster devient invisible et une trail (cosm�tique) est instanci� sur le joueur
                AudioSource.PlayClipAtPoint(boosterAudio, transform.position);
                player.speed += speedBoost;
                slider.gameObject.SetActive(true);
                timer += timerMax;
                isTimerOn = true;
                GetComponentInParent<MeshRenderer>().enabled = false;
                GetComponent<Renderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                tr = Instantiate(trail, other.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity, other.transform);
            }
        }
    }
}