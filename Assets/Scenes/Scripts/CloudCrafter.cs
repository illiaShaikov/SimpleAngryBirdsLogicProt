﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]

    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float CloudSpeedMult = 0.5f;

    private GameObject[] CloudInstances;
    void Awake()
    {
        CloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for(int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU;
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            cloud.transform.SetParent(anchor.transform);
            CloudInstances[i] = cloud;
        }
    }
    void Update()
    {
        foreach(GameObject cloud in CloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 Cpos = cloud.transform.position;
            Cpos.x -= scaleVal * Time.deltaTime * CloudSpeedMult;
            if(Cpos.x <=cloudPosMin.x)
            {
                Cpos.x = cloudPosMax.x;
            }
            cloud.transform.position = Cpos;
        }
    }
}
