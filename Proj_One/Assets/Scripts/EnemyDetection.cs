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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Collider[] hitColliders = Physics.OverlapBox(new Vector3(transform.position.x, transform.position.y, transform.position.z + VisionSize.z/2), VisionSize / 2, Quaternion.identity, myLayerMask);
        Collider[] sphereColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f), 4f, myLayerMask);
        //if (Physics.BoxCast(transform.position, new Vector3(2f, 4f, 2f), Vector3.forward, out hit, Quaternion.identity, castDistance, myLayerMask))
        if(Physics.Raycast(transform.position, transform.forward, out hit,castDistance, myLayerMask))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Clone"))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("INSIDE");
                GameObject go = hit.transform.gameObject;
                EnemyMovement.targetPosition = go.transform.position;
            }
        }
        foreach (Collider col in sphereColliders)
        {
            if (col.CompareTag("Player") || col.CompareTag("Clone"))
            {
                //Debug.Log(hit.collider.gameObject.name);
                Debug.Log("INSIDE");
                GameObject go = col.transform.gameObject;
                EnemyMovement.targetPosition = go.transform.position;
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z + VisionSize.z/2), VisionSize);
        Gizmos.DrawLine(transform.position, hit.point);
        //Gizmos.DrawRay(transform.position, hit.point);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f), 4f);

        //Gizmos.DrawWireCube(hit.point)
    }
}
