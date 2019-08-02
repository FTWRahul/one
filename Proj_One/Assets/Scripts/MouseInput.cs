﻿using System;
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
                if(Input.GetMouseButtonDown(0))
                {
                    SendClone(mousePosi);
                }
            }
        }
    }

    private void SendClone(Vector3 mousePosi)
    {
        clonePrefab.SetActive(true);

    }
}
