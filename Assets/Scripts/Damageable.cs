using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float speedDebuff;

    //coroutine permettant de ralentir la vitesse du joueur pour un certain temps lorsqu'il est touché par un ennemi
    IEnumerator SlowedTimer()
    {
        if (GetComponent<Movement>().speed >= 5)
        {
            GetComponent<Movement>().speed -= speedDebuff;
            yield return new WaitForSeconds(4);
            GetComponent<Movement>().speed += speedDebuff;
        }
    }
    //appelé lorsqu'un ennemi collisionne avec le joueur
    public void GetHit()
    {
        StartCoroutine(SlowedTimer());
    }
}
