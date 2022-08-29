using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Instance : MonoBehaviour
{
    public Movement playerMovement;
    public GameObject[] plateForms;
    public GameObject[] coin;
    public GameObject[] trees;
    public GameObject booster;
    public GameObject timer;
    public GameObject slime;
    public int platformXRows;
    public int platformZRows;
    public GameObject enemy;
    public CinemachineVirtualCamera cutsceneCamera;
    public CinemachineFreeLook mainCamera;
    public float cutsceneTime;

    private void Start()
    {
        //Gestion des caméras pour une cutscene au lancement du jeu + lancement de coroutines d'instantiations
        cutsceneCamera.Priority = 1;
        mainCamera.Priority = 0;
        StartCoroutine(InstancePlateforme());
        StartCoroutine(Camera());
    }
    //instance d'un ennemi a un point au hasard pas très loin du joueur
    void InstanceMobs()
    {
        Instantiate(enemy, new Vector3(Random.Range(-15, -10), 3005, Random.Range(10, 15)), Quaternion.identity);
    }
    //instance de pièces sur les plateformes, 15% chance pièce en or, 25% chance pièce en argent, 50% chance pièce en cuivre et 10% chances pas de pièce
    void InstanceCoins(Vector3 pos, Transform parent)
    {
        float rnd = Random.value;
        if (rnd <= 0.15f)
        {
            GameObject i = Instantiate(coin[2], pos + new Vector3(0 + Random.Range(-1, 1), 2, 0 + Random.Range(-1, 1)), Quaternion.identity);
            i.transform.parent = parent;
        }
        else if (rnd <= 0.40f)
        {
            GameObject i = Instantiate(coin[1], pos + new Vector3(0 + Random.Range(-1, 1), 2, 0 + Random.Range(-1, 1)), Quaternion.identity);
            i.transform.parent = parent;
        }
        else if (rnd <= 0.90f)
        {
            GameObject i = Instantiate(coin[0], pos + new Vector3(0 + Random.Range(-1, 1), 2, 0 + Random.Range(-1, 1)), Quaternion.identity);
            i.transform.parent = parent;
        }
    }

    //instance les boosters et objets (slime), 15% de chance booster et 15% de chance objet
    void InstanceBoosterSlime(Vector3 pos, Transform parent)
    {
        float rnd = Random.Range(0, 100);
        if (rnd <= 15)
        {
            GameObject i = Instantiate(booster, pos + new Vector3(0 + Random.Range(-1, 1), 2, 0 + Random.Range(-1, 1)), Quaternion.identity);
            i.transform.parent = parent;
        }
        else if(rnd <= 30)
        {
            Instantiate(slime, pos + new Vector3(0 + Random.Range(-1, 1), 2, 0 + Random.Range(-1, 1)), Quaternion.identity);
        }
    }

    //instance les arbres, 1/2 chance qu'un arbre spawn
    void InstanceTree(Vector3 pos, Transform parent)
    {
        int rnd = Random.Range(0, 101);
        if (rnd <= 50)
        {
            int tree = Random.Range(0, trees.Length);
            GameObject i = Instantiate(trees[tree], pos + Vector3.up * 0.5f, Quaternion.identity);
            i.transform.parent = parent;
        }
    }

    //instantiation des plateformes, en coroutine pour créer un effet en créant 1 plateforme toute les 0.1 secondes
    IEnumerator InstancePlateforme()
    {
        //grace aux paramètres X et Z on peut choisir combient de plateformes on veut en X ou Z, le total de plateformes créées sera égal a X*Z-1
        for (int o = 0; o < platformXRows; o++)
        {
            for (int q = 0; q < platformZRows; q++)
            {
                //la plateforme de base en 0,0,0 étant déja créée pas besoin d'en faire une autre
                if (q == 0 && o == 0)
                {
                    continue;
                }
                //instantiation de plateforme + appel des autres fonctions d'instantiations pour créer les éléments de chaque plateforme aléatoirement
                float y = 3000 + Mathf.Round(Random.Range(-2, 2));
                GameObject p = Instantiate(plateForms[Random.Range(0,2)], new Vector3(o * 15 + Random.Range(-1, 1), y, q * 15 + Random.Range(-1, 1)), Quaternion.identity);
                InstanceCoins(p.transform.Find("CoinSpawn").position, p.transform);
                InstanceCoins(p.transform.Find("CoinSpawn1").position, p.transform);
                InstanceBoosterSlime(p.transform.Find("BoosterSpawn").position, p.transform); int rnd = Random.Range(0, 2);
                if (rnd == 1)
                {
                    InstanceTree(p.transform.Find("TreeSpawn").position, p.transform);
                }
                else
                {
                    InstanceTree(p.transform.Find("TreeSpawn1").position, p.transform);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    //Cutscene au début du jeu, on change les priority des caméras qui sont gérées par cinemachine et transitionnent de manière fluide
    //puis a la fin de celle-ci on instance un ennemi avec sa fonction et on commence le timer
    IEnumerator Camera()
    {
        yield return new WaitForSeconds(cutsceneTime);
        cutsceneCamera.Priority = 0;
        mainCamera.Priority = 1;
        yield return new WaitForSeconds(1.5f);
        playerMovement.characterState = true;
        InstanceMobs();
        timer.SetActive(true);
    }
}
