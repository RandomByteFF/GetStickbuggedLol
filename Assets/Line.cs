using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float height;
    private Vector3 StartPos;
    private LineRenderer l;
    private float length;
    private bool done;
    private GameObject script;
    private int order;

    private Touch touch;

    void Start()
    {
        StartPos = transform.position;
        l = GetComponent<LineRenderer>();
        script = GameObject.Find("DrawAgent");
        Draw comp = script.GetComponent<Draw>();
        order = comp.order;
        length = Math.Abs(Vector2.Distance(comp.positions[order,0], comp.positions[order,1]));
        Debug.Log(length);
        done = false;
    }

    void Update()
    {
        if (script.GetComponent<Draw>().order == 9)
        {
            //Align into position
            Debug.Log("I should go into my place now");
        }
        else if (touch.phase == TouchPhase.Ended && !done)
        {
            Destroy(gameObject);
        }
        else if (!done && Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            Vector3 current = touch.position;
            current = Camera.main.ScreenToWorldPoint(current);
            current.z = 0;

            Vector3[] pos = {StartPos, current};
            Color c = new Color(252, 253, 189);

            l.startWidth = height;
            l.endWidth = height;
            l.SetPositions(pos);
            //l.startColor = c;
            //l.endColor = c;
            l.useWorldSpace = true;

            float distance = Math.Abs(Vector3.Distance(current, StartPos));
            if (distance >= length) {
                done = true;
                script.GetComponent<Draw>().order++;
            }
        }
    }
}
