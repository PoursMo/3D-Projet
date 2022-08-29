using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    bool isAttached;
    Transform cameraTransform;
    public float distance;
    public float force;
    Collider collid;
    Rigidbody rb;
    GameObject crosshair;
    private void Start()
    {
        //R�f�rences au collider, rigidbody, la crosshair et main cam�ra
        collid = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        crosshair = GameObject.Find("UI").transform.Find("Crosshair").gameObject;
        cameraTransform = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        //si l'objet est "attach�" au joueur, il le suivra ainsi que les mouvements de la cam�ra pour etre toujours en face du joueur
        if (isAttached)
        {
            transform.position = Vector3.Lerp(transform.position, cameraTransform.position + Camera.main.transform.forward * distance, 0.02f);
        }
        //destruction de l'objet s'il tombe dans le vide
        if (transform.position.y <= 2900)
        {
            Destroy(gameObject);
        }
    }
    public void GetPickedUp()
    {
        {
            //l'objet est ramass� par le joueur, l'UI affiche un point qui sert de crosshair, le collider et rigidbody sont d�sactiv�s
            crosshair.SetActive(true);
            collid.enabled = false;
            rb.useGravity = false;
            isAttached = true;
        }
    }
    public void Throw()
    {
        if (isAttached)
        {
            //le joueur lance l'objet en direction de la ou il regarde, le collider et rigidbody sont r�activ�s pour le soumettre a la gravit� et collider avec d'autres objets
            crosshair.SetActive(false);
            isAttached = false;
            collid.enabled = true;
            rb.useGravity = true;
            rb.AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
        }
    }
}
