using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolActions {Stand, Move, Rotate}
public enum Rotation {Up, Down, Left, Right}

public class EnemyPatrol : MonoBehaviour
{
    public List<AIActions> aiActions;
    bool moving;
    bool rotating;
    bool standing;
    EnemyMovement movementScript;

    int i = 0;

    Vector3 targetPosi;
    Quaternion targetRot;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<EnemyMovement>();
        CheckNextState();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            //Debug.Log("PATROL SCRIPT TARGET :   " + targetPosi);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosi - transform.position), 5 * Time.deltaTime);
            if(!movementScript.isMoving)
            {                
                moving = false;
                //CheckNextState();
                //return;
            }
            //return;
        }
        else if(rotating)
        {
            //Debug.Log(transform.rotation);
            if(transform.rotation != targetRot)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, time * Time.deltaTime);
                //return;
            }
            else
            {
                rotating = false;
                //CheckNextState();
            }
            
        }
        if(!standing && !moving && !rotating)
        {
            CheckNextState();
        }
        //Debug.Log("STANDING? " + standing + "  ROTATING?   " + rotating + "  MOVING????   " + moving);
        //CheckNextState();
        //PatrolDictionary.a
    }

    private void CheckNextState()
    {

        Debug.Log("CHECKING STATE");
        if(aiActions[i].patrolActions == PatrolActions.Move)
        {
            targetPosi = aiActions[i].patrolPositions.transform.position;
            //targetRot = ReturnDirection(aiActions[i].rotationEnum);
            //time = aiActions[i].time;
           // Debug.Log("Moving");
            moving = true;
            movementScript.targetPosition = targetPosi;
            //Debug.Log("PATROL POSI" + targetPosi);

        }
        else if(aiActions[i].patrolActions == PatrolActions.Rotate)
        {
            //targetPosi = aiActions[i].patrolPositions.transform.position;
            targetRot = ReturnDirection(aiActions[i].rotationEnum);
            targetPosi = transform.position;
            time = aiActions[i].time;
           // Debug.Log("Rotating");
            rotating = true;
            //Debug.Log(targetRot);
            //StartCoroutine(RotateToDir(ReturnDirection(aiActions[i].rotationEnum)));
        }
        else if(aiActions[i].patrolActions == PatrolActions.Stand)
        {
            //targetPosi = aiActions[i].patrolPositions.transform.position;
            //targetRot = ReturnDirection(aiActions[i].rotationEnum);
            targetPosi = transform.position;
            time = aiActions[i].time;
           // Debug.Log("Standing");
            //moving = false;
            StartCoroutine(StandForSeconds(time));
        }
        if(i < aiActions.Count)
        {
            Debug.Log(aiActions.Count);
            i++;
            Debug.Log("I IS THIS PLEASE   "+i);
            if(i == aiActions.Count)
            {
                i = 0;
            }
        }
    }

    IEnumerator StandForSeconds(float time)
    {
        standing = true;

        yield return new WaitForSeconds(time);
        standing = false;

        CheckNextState();
    }

    Quaternion ReturnDirection(Rotation rot)
    {
        if(rot == Rotation.Up)
        {
            return Quaternion.LookRotation(Vector3.forward);
        }
        else if (rot == Rotation.Down)
        {
            return Quaternion.LookRotation(Vector3.back);
        }
        else if (rot == Rotation.Left)
        {
            return Quaternion.LookRotation(Vector3.left);
        }
        else if (rot == Rotation.Right)
        {
            return Quaternion.LookRotation(Vector3.right);
        }
        else
        {
            return Quaternion.identity;
        }
    }
}
