using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public Transform playerTransform;

    //l'objet fallbox permettant de savoir lorsque le joueur tombe suit le joueur en x et z
    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, 2980, playerTransform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        //layer 7 = joueur
        if(other.gameObject.layer == 7)
        {
            //si le joueur collide avec la fallbox, on appelle sa fonction Fall
            other.GetComponent<Movement>().Fall();
        }
    }
}
