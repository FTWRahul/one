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

    public static Vector3 targetPosition;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
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
        if (transform.position != targetPosition)
        {
            agent.SetDestination(targetPosition);
            //Vector3 moveDir = new Vector3(MouseInput.clickPosition.x, 0, MouseInput.clickPosition.z) - transform.position;
            //charController.Move(moveDir.normalized * speed * Time.deltaTime);
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
        }
    }
}
