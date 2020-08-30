using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float height;
    private Vector3 StartPos;
    private LineRenderer l;
    void Start()
    {
        StartPos = transform.position;
        l = GetComponent<LineRenderer>();
        Debug.Log(StartPos);
    }

    void Update()
    {
        if (Input.touchCount > 0) {
            Vector3 current = Input.GetTouch(0).position;
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
        }
    }
}
