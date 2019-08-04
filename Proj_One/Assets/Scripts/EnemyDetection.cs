using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField]
    LayerMask myLayerMask;
    [SerializeField]
    float castDistance;
    [SerializeField]
    Vector3 VisionSize;

    EnemyMovement movementScript;

    AudioSource detectionSound;
    bool audioPlayed;

    // Start is called before the first frame update
    void Start()
    {
        detectionSound = GetComponent<AudioSource>();
        movementScript = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        //Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y, transform.position.z + VisionSize.z/2), VisionSize / 2, Quaternion.identity, myLayerMask);
        Collider[] sphereColliders = Physics.OverlapSphere(transform.position + transform.forward * 3f, 4f, myLayerMask);
        //if (Physics.BoxCast(transform.position, new Vector3(2f, 4f, 2f), Vector3.forward, out hit, Quaternion.identity, castDistance, myLayerMask))
        if(Physics.Raycast(transform.position, transform.forward, out hit,castDistance, myLayerMask))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Clone"))
            {
                try
                {
                    GetComponent<EnemyPatrol>().enabled = false;
                }
                catch
                {

                }
                //Debug.Log(hit.collider.gameObject.name);
                //Debug.Log("INSIDE");
                DetectionSound();
                GameObject go = hit.transform.gameObject;
                movementScript.targetPosition = go.transform.position;
                movementScript.targetObj = go;

            }
        }
        foreach (Collider col in sphereColliders)
        {
            if (col.CompareTag("Player") || col.CompareTag("Clone"))
            {
                try
                {
                    GetComponent<EnemyPatrol>().enabled = false;
                }
                catch
                {
                    
                }

                //Debug.Log(hit.collider.gameObject.name);
                //Debug.Log("INSIDE");
                DetectionSound();
                GameObject go = col.transform.gameObject;
                movementScript.targetPosition = go.transform.position;
                movementScript.targetObj = go;
            }
        }


    }

    void DetectionSound()
    {
        if(!audioPlayed)
        {
            detectionSound.Play();
            audioPlayed = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z + VisionSize.z/2), VisionSize);
        Gizmos.DrawLine(transform.position, hit.point);
        //Gizmos.DrawRay(transform.position, hit.point);
        Gizmos.DrawWireSphere(transform.position + transform.forward * 3f, 4f);

        //Gizmos.DrawWireCube(hit.point)
    }
}
