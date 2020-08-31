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
    public float stepSpeed;
    private Vector3[] cur;
    private Vector3[,] final;
    private bool reported = false;
    private float[] step;

    void Start()
    {
        StartPos = transform.position;
        l = GetComponent<LineRenderer>();
        script = GameObject.Find("DrawAgent");
        Draw comp = script.GetComponent<Draw>();
        order = comp.order;
        final = comp.positions;
        Debug.Log("Order: " + order);
        step = new float[2];
        length = Math.Abs(Vector3.Distance(comp.positions[order,0], comp.positions[order,1]));
        done = false;
    }

    void Update()
    {
        if (script.GetComponent<Draw>().order >= 9)
        {
            //Align into position
            for (int i = 0; i < 2; i++)
            {
                float stepSize = step[i] * Time.deltaTime;
                Vector3 target = final[order, i];
                cur[i] = Vector3.MoveTowards(cur[i], target, stepSize);
            }
            l.SetPositions(cur);

            if (!reported && final[order, 0] == cur[0] && final[order,1] == cur[1]) {
                script.GetComponent<Draw>().finished++;
                reported = true;
            }
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
            Vector3[] pos = {current, StartPos };
            cur = pos;
            
            Color c = new Color(252, 253, 189);

            l.startWidth = height;
            l.endWidth = height;
            l.SetPositions(pos);
            //l.startColor = c;
            //l.endColor = c;
            l.useWorldSpace = true;

            float distance = Math.Abs(Vector3.Distance(current, StartPos));
            if (distance >= length) {
                for (int i = 0; i < 2; i++)
                {
                    step[i] = Math.Abs(Vector3.Distance(final[order, i], pos[i])) * stepSpeed;
                }
                done = true;
                script.GetComponent<Draw>().order++;
            }
        }
    }
}
