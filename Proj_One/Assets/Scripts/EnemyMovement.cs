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
    public GameObject targetObj;

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
        try
        {
            transform.LookAt(targetObj.transform);
        }
        catch
        {
            Debug.Log("Target Destroyed");
        }
    }

    private void MoveEnemy()
    {
        //Debug.Log("MOVEMENT SCRIPT TARGET?   " + targetPosition);
        //Debug.Log("PLAYER POSITION???    " + transform.position);
        //Debug.Log("MOVEMENT SCRIPT MOVING?  " + isMoving);

        //if (transform.localPosition.x != targetPosition.x && transform.localPosition.z != targetPosition.z)
        if(Vector3.Distance(transform.position, targetPosition) > .5f)
        {
            isMoving = true;
            agent.SetDestination(targetPosition);
            //Debug.Log("MOVEMENT SCRIPT MOVING?  " + isMoving);           
        }
        else //if(transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
        {
            isMoving = false;
            GetComponent<EnemyPatrol>().moving = false;
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
