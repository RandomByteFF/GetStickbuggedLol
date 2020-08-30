using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject video;
    public GameObject line;
    private Vector2 touchStart;
    private Touch touch;
    private GameObject store;
    private List<GameObject> doneLine;
    void Start() {
        //Set screen orientation
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    void Update()
    {
        //Debug.Log(Input.touchCount);
        if (touch.phase == TouchPhase.Ended)
        {
            Destroy(store);
        }
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            touchpos.z = 0;
            transform.position = touchpos;

            if (touch.phase == TouchPhase.Began) {
                Debug.Log(touchpos);
                touchStart = touchpos;
                store = Instantiate(line, transform.position, transform.rotation);
            }
        }
    }

    void StartVideo() {
        var script = video.GetComponent<Video>();
        script.Play();
    }
}
