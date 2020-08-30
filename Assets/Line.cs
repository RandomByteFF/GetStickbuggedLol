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
    private bool reported = false;

    void Start()
    {
        StartPos = transform.position;
        l = GetComponent<LineRenderer>();
        script = GameObject.Find("DrawAgent");
        Draw comp = script.GetComponent<Draw>();
        order = comp.order;
        Debug.Log("Order: " + order);
        length = Math.Abs(Vector2.Distance(comp.positions[order,0], comp.positions[order,1]));
        done = false;
    }

    void Update()
    {
        if (script.GetComponent<Draw>().order >= 9)
        {
            //Align into position
            float step = stepSpeed * Time.deltaTime;
            for (int i = 0; i < 2; i++)
            {
                Vector3 target = new Vector3(script.GetComponent<Draw>().positions[order, i].x, script.GetComponent<Draw>().positions[order, i].y, 0);
                cur[i] = Vector3.MoveTowards(cur[i], target, step);
            }
            l.SetPositions(cur);

            Debug.Log("I should go into my place now");
            if (new Vector3(script.GetComponent<Draw>().positions[order, 0].x, script.GetComponent<Draw>().positions[order, 0].y, 0) == cur[0] && new Vector3(script.GetComponent<Draw>().positions[order, 1].x, script.GetComponent<Draw>().positions[order, 1].y, 0) == cur[1] && !reported) {
                script.GetComponent<Draw>().finished++;
                reported = true;
            }
            //TODO: prettier
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
                done = true;
                script.GetComponent<Draw>().order++;
            }
        }
    }
}
