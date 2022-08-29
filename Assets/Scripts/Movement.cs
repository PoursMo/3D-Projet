using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator anim;
    public CharacterController controller;
    public Transform cam;
    public float speed;
    public float rotationSpeed;
    public float gravity;
    public float jumpForce;
    float verticalVelocity;
    float var = 0;
    public bool characterState;
    bool resetLock = false;
    public CharacterPickup characterPickup;
    Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }
    private void Awake()
    {
        //vérouillage du curseur
        Cursor.lockState = CursorLockMode.Locked;
    }
    bool IsGrounded()
    {
        //vérification si le joueur est grounded a l'aide d'un raycast, qui collide seulement avec la layer "platforms"
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f, LayerMask.GetMask("Platforms"));
    }
    void Update()
    {
        if (characterState)
        {
            //récupère l'input du joueur
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            //reset animations
            anim.SetBool("isMoving", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isThrowing", false);
            //gestion du mouvement et de la rotation du joueur avec la fonction Move du Character Controller
            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isMoving", true);
                float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref var, rotationSpeed);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
                anim.SetFloat("Speed", speed);
            }
            //gestion gravité + saut du joueur

            if (!IsGrounded())
            {
                verticalVelocity -= gravity * Time.deltaTime;
                resetLock = false;
            }
            else
            {
                if (!resetLock)
                {
                    verticalVelocity = 0;
                    resetLock = true;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                anim.SetBool("isJumping", true);
                verticalVelocity = jumpForce;
            }
            Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
            controller.Move(jumpVector * Time.deltaTime);
            //Input E pour ramasser un objet
            if (Input.GetKey(KeyCode.E))
            {
                characterPickup.PickUp();
            }

            //Input control gauche pour lancer objet
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (characterPickup.pickedUp)
                {
                    anim.SetBool("isThrowing", true);
                    characterPickup.Throw();
                }
            }
        }
    }
    //appelé lorsque le joueur tombe, il est téléporté a sa position initiale
    public void Fall()
    {
        controller.enabled = false;
        transform.position = initialPos;
        verticalVelocity = 0;
        controller.enabled = true;
    }
}
