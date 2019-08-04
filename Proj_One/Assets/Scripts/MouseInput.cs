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

    public bool cloneUsed;
    public bool switchUsed;

    AudioSource cloneSound;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        cloneSound = GetComponent<AudioSource>();
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

        if (Physics.Raycast(ray, out hit, myLayerMask))
        {
            mousePosi = new Vector3(hit.point.x, 2f, hit.point.z);
            Vector3 rotation = mousePosi - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation), rotationSepeed * Time.deltaTime);
            if (hit.collider.CompareTag("Ground"))
            {
                if (Input.GetMouseButtonDown(0) && !cloneUsed)
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
        Vector3 previousPosi;
        Vector3 clonesPosition;
        clonesPosition = clonePrefab.transform.position;
        previousPosi = transform.position;

        ButtonZone[] buttons = FindObjectsOfType<ButtonZone>();
        if (buttons != null)
        {
            float difference = 0;
            foreach (ButtonZone button in buttons)
            {
                difference = Vector3.Distance(transform.position, button.transform.position);
                if (difference < 1f)
                {
                    button.itemsInZone--;
                }
            }

        }
        clonePrefab.GetComponent<ButtonHandeling>().DisableHandling();
        // hit.collider.GetComponent<CloneMovement>().enabled = false;
        FindObjectOfType<CloneMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        //clonePrefab.SetActive(false);
        transform.position = clonesPosition;
        //clonePrefab.transform.position = Vector3.Lerp(clonePrefab.transform.position, previousPosi, 10000 * Time.deltaTime);
        clonePrefab.transform.position = previousPosi;
        GetComponent<PlayerMovement>().enabled = true;
        //clonePrefab.SetActive(true);
        //clonePrefab.GetComponent<ButtonHandeling>().EnableHandling();
        switchUsed = true;
        //ButtonZone[] buttons2 = FindObjectsOfType<ButtonZone>();
        //if (buttons != null)
        //{
        //    float difference = 0;
        //    foreach (ButtonZone button in buttons2)
        //    {
        //        difference = Vector3.Distance(transform.position, button.transform.position);
        //        if (difference < .5f)
        //        {
        //            button.itemsInZone--;
        //        }
        //    }
        //    //FindObjectOfType<ButtonZone>().ExitFunction();
        //    //FindObjectOfType<ButtonZone>().EnterFuncion();

        //    //Destroy(clonePrefab);
        //}
    }

    public void SendClone(Vector3 mousePosi)
    {
        cloneSound.Play();
        clonePrefab.SetActive(true);
        clickPosition = mousePosi;
        cloneUsed = true;
        //clonePrefab.GetComponent<CloneMovement>().
    }
}
