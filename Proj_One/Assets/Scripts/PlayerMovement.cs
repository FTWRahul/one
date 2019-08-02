using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed;

    CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Storing inputs in variables
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        //Multiplaying the inputs with direction vectors and speed, normilizing with time.
        Vector3 moveDirSide = transform.right * horiz;
        Vector3 moveDirForward = transform.forward * vert;


        //Vector3 finalMoveDirection = (moveDirSide + moveDirForward).normalized * walkSpeed * Time.deltaTime;
        //Making the char controller do a Move by passing our desired vectors, The physics calculations are done in the PlayerMotor class.
        charController.Move((moveDirSide + moveDirForward).normalized * speed * Time.deltaTime);
    }
}
