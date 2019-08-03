using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseInput : MonoBehaviour
{
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    float rotationSepeed;
    [SerializeField]
    LayerMask myLayerMask;

    Vector3 mousePosi;
    RaycastHit hit;

    public GameObject clonePrefab;

    public static Vector3 clickPosition;

    public static bool cloneUsed;
    public static bool switchUsed;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //mainCam.ScreenPointToRay(Input.mousePosition + new Vector3(0,0,5));
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RotatePlayer(ray);
    }

    private void RotatePlayer(Ray ray)
    {

        if(Physics.Raycast(ray, out hit, myLayerMask))
        {
            mousePosi = new Vector3(hit.point.x, 2f, hit.point.z);
            Vector3 rotation = mousePosi - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation),rotationSepeed * Time.deltaTime);
            if(hit.collider.CompareTag("Ground"))
            {
                if(Input.GetMouseButtonDown(0) && !cloneUsed)
                {
                    SendClone(mousePosi);
                }                
            }
            //else if(hit.collider.CompareTag("Clone"))
            //{
            //    if (Input.GetKeyDown(KeyCode.Space) && cloneUsed && !switchUsed)
            //    {
            //        ProcessClone(hit);
            //    }
            //}
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && cloneUsed && !switchUsed)
        {
            ProcessClone(hit);
        }
    }

    private void ProcessClone(RaycastHit hit)
    {
        // hit.collider.GetComponent<CloneMovement>().enabled = false;
        FindObjectOfType<CloneMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        Vector3 previousPosi;
        Vector3 clonesPosition;
        clonesPosition = clonePrefab.transform.position;
        previousPosi = transform.position;
        //transform.position = Vector3.Lerp(transform.position, clonesPosition, 10000 * Time.deltaTime);
        transform.position = clonesPosition;
        //clonePrefab.transform.position = Vector3.Lerp(clonePrefab.transform.position, previousPosi, 10000 * Time.deltaTime);
        clonePrefab.transform.position = previousPosi;
        GetComponent<PlayerMovement>().enabled = true;
        switchUsed = true;
        FindObjectOfType<ButtonZone>().ExitFunction();
        FindObjectOfType<ButtonZone>().EnterFuncion();

        //Destroy(clonePrefab);
    }

    private void SendClone(Vector3 mousePosi)
    {
        clonePrefab.SetActive(true);
        clickPosition = mousePosi;
        cloneUsed = true;
        //clonePrefab.GetComponent<CloneMovement>().
    }
}
