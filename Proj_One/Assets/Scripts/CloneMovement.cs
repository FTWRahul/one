using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneMovement : MonoBehaviour
{

    Rigidbody rb;
    CharacterController charController;

    [SerializeField]
    float speed;

    Vector3 movePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveClone();
    }

    private void MoveClone()
    {
        if (transform.position != MouseInput.clickPosition)
        {
            Vector3 moveDir = new Vector3(MouseInput.clickPosition.x, 0 , MouseInput.clickPosition.z) - transform.position;
            charController.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Debug.Log("Wall touched");
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        transform.parent = null;
    }
}
