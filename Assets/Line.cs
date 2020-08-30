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

    private Touch touch;
    void Start()
    {
        StartPos = transform.position;
        l = GetComponent<LineRenderer>();
        script = GameObject.Find("DrawAgent");
        Draw comp = script.GetComponent<Draw>();
        length = comp.lengths[comp.order];
        done = false;
    }

    void Update()
    {
        if (!done && Input.touchCount > 0) {
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
        if (touch.phase == TouchPhase.Ended && !done)
        {
            Debug.Log("I should destroy now");
            Destroy(gameObject);
        }
    }
}
