using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class DrawPoints : MonoBehaviour
{
    public GameObject box;
    public int numPoints;
    public Vector3 boxCenter;
    public Vector3 boxSize;
    public float speed = 0.05f;

    public GameObject enemy;

    Bounds boxBounds;

    bool printDebug = true;

    public List<Vector3> points;
    public List<GameObject> enemies;

    float timer;


    private void Awake()
    {

        
        if(box != null)
        {
            BoxCollider boxCol = box.GetComponent<BoxCollider>();
            boxBounds = boxCol.bounds;
            boxCenter = boxBounds.center;
            boxSize = boxBounds.extents;
        }
        Debug.Log(boxBounds);
        Debug.Log("center: " + boxCenter);
        Debug.Log("size: " + boxSize);
        Debug.Log("num_points: " + numPoints);
        points = new List<Vector3>(numPoints + 3);
        
        GenRandomPoints(numPoints + 3);
        enemies = new List<GameObject>(numPoints);
        //for (int i = 0; i < numPoints; ++i)
        //{
        //    enemies[i] = new GameObject
        //}
        
    }

    public void GenRandomPoints(int n)
    {
        int numUnique = n - 3;
        for (int i = 0; i < n; ++i)
        {
            if (i >= numUnique)
            {
                // repeat points to close loop
                points.Insert(i, points[i % numUnique]);
            }
            else
            {
                Vector3 v = UnityEngine.Random.insideUnitSphere * boxSize[0] + boxCenter;
                points.Insert(i, v);
            }

        }
        
        Debug.Log("Points: ");
        for (int i = 0; i < points.Count; ++i)
        {
            
            Debug.Log(i + ": " + points[i]);
        }
    }

    public static Vector3 InterpolateFromCatmullRomSpline(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        return 0.5f * ((2f * p2) + (-p1 + p3) * t + (2f * p1 - 5f * p2 + 4f * p3 - p4) * Mathf.Pow(t, 2f) + (-p1 + 3f * p2 - 3f * p3 + p4) * Mathf.Pow(t, 3f));
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; ++i)
        {
            Gizmos.DrawSphere(points[i], 0.25f);
        }

        for (int i = 0; i < points.Count - 3; ++i)
        {
            for (float t = 0.0f; t < 1.0f; t += 0.01f)
            {
                Vector3 v = InterpolateFromCatmullRomSpline(points[i], points[i + 1], points[i + 2], points[i + 3], t);
                if (printDebug)
                {
                    Debug.Log("v: " + v);
                }
                Gizmos.DrawSphere(v, 0.01f);
            }
        }

        printDebug = false;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        float t = ( timer ) % numPoints;
        int k = (int) t; // rounded to floor
        float u = t - k;
        //Debug.Log("t: " + t + " k: " + k + " u: " + u);
        
        if (enemy != null)
        {
            enemy.transform.position = InterpolateFromCatmullRomSpline(points[k], points[k + 1], points[k + 2], points[k + 3], u);
        }
    }
}
