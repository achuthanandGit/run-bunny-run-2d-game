﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private float nextSpawn = 0;
    public Transform prefabToSpawn;
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 30f;
    private float startTime;
    public float jitter = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            float curverPos = (Time.time - startTime) / curveLengthInSeconds;
            if(curverPos > 1f)
            {
                curverPos = 1f;
                startTime = Time.time;
            }
            nextSpawn = Time.time + spawnCurve.Evaluate(curverPos) + Random.Range(-jitter, jitter);
        }
    }
}