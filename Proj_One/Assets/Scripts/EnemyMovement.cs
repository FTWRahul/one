using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody rb;
    CharacterController charController;

    [SerializeField]
    float speed;

    public Vector3 targetPosition;

    public NavMeshAgent agent;

     public bool isMoving;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
       // Debug.Log("MOVEMENT SCRIPT TARGET?   " + targetPosition);
        //Debug.Log("PLAYER POSITION???    " + transform.position);
        Debug.Log("MOVEMENT SCRIPT MOVING?  " + isMoving);

        if (transform.localPosition.x != targetPosition.x && transform.localPosition.z != targetPosition.z)
        {
            isMoving = true;
            agent.SetDestination(targetPosition);
            //Debug.Log("MOVEMENT SCRIPT MOVING?  " + isMoving);
            //Vector3 moveDir = new Vector3(MouseInput.clickPosition.x, 0, MouseInput.clickPosition.z) - transform.position;
            //charController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else //if(transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
        {
            isMoving = false;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player killed");
        }
        else if (other.CompareTag("Clone"))
        {
            Debug.Log("Bamboozled");
            Destroy(other.gameObject);
            MouseInput.switchUsed = true;
        }
    }
}
