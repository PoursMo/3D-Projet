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
        //Référence a l'élément d'UI "slider" pour représenter le timer du booster, avec sa maxValue étant de la taille de notre timer
        slider = GameObject.Find("UI").transform.Find("Booster").GetComponent<Slider>();
        slider.maxValue = timerMax;
    }
    private void Update()
    {
        //gestion du timer, une fois à 0 tout revient comme avant
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
            //vérification si le slider est déja activé le joueur est déja boosté donc ne peut en reprendre un
            if (slider.gameObject.activeInHierarchy == false)
            {
                //le booster est pris : un son est joué, la vitesse du joueur est augmentée, le slider UI est activé, le timer est commencé
                //le booster devient invisible et une trail (cosmétique) est instancié sur le joueur
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