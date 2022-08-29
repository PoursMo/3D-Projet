using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    public GameObject PressE;
    GameObject throwableObject;
    public bool pickedUp;
    Throwable throwable;
    private void OnTriggerEnter(Collider other)
    {
        //layer 11 = objet
        if (other.gameObject.layer == 11)
        {
            //si le joueur est proche d'un objet a ramasser, un element d'UI affichant la touche a utiliser s'affichera et on cr�� une r�f�rence a l'objet
            throwableObject = other.gameObject;
            PressE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            //lorsque le joueur n'est plus proche de l'objet a ramasser, l'UI du bouton E disparait et la r�f�rence a l'objet annul�e
            throwableObject = null;
            PressE.SetActive(false);
        }
    }
    public void PickUp()
    {
        if (throwableObject != null && !pickedUp)
        {
            //le joueur a appuy� sur E, l'objet est ramass�, on appelle sa fonction GetPickedUp
            pickedUp = true;
            PressE.SetActive(false);
            throwable = throwableObject.GetComponent<Throwable>();
            throwable.GetPickedUp();
        }
    }
    public void Throw()
    {
        if (pickedUp)
        {
            //le joueur a appuy� sur ctrl gauche, la fonction Throw de l'objet est appel�e
            pickedUp = false;
            throwable.Throw();
            throwableObject = null;
        }
    }
}
