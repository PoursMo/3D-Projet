using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Vector3 direction;
    Transform playerTransform;
    public float moveSpeed;
    bool pushAway;
    Vector3 pushAwayDirection;
    private void Start()
    {
        //référence au joueur
        playerTransform = GameObject.Find("George").transform;
    }
    void Update()
    {
        //lorsque pushaway est true, l'ennemi fuit dans une direction aléatoire, autrement il va en direction du joueur
        if (pushAway == true)
        {
            direction = (transform.position + pushAwayDirection) - transform.position;
        }
        else
        {
            direction = playerTransform.position - transform.position;
        }
        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

        //rotation de l'ennemi en direction du joueur
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        //layer 7 = joueur
        if (other.gameObject.layer == 7)
        {
            //lorsque l'ennemi collisionne avec le joueur, la fonction GetHit du joueur est appelée
            //PushAway commence la coroutine pour que l'ennemi fuit pendant un certain temps
            other.GetComponent<Damageable>().GetHit();
            PushAway();
        }
        if(other.gameObject.layer == 11)
        {
            PushAway();
        }
    }

    public void PushAway()
    {
        StartCoroutine(PushingAway());
    }
    IEnumerator PushingAway()
    {
        pushAway = true;
        pushAwayDirection = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10));
        yield return new WaitForSeconds(4);
        pushAway = false;
    }
}
