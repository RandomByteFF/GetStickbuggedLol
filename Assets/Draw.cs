using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject video;
    public GameObject line;
    private Touch touch;
    public int order;
    public int finished = 0;
    //if there is a better way pls tell me. I can only think about reading this from a file.
    public Vector2[,] positions = { { new Vector2(2.17f, 1.3f), new Vector2(0.62f, 1.3f) } , //back
                                    { new Vector2(-0.92f, 2.13f), new Vector2(0.51f, 1.37f) } , //front
                                    { new Vector2(-0.5f, 1.83f), new Vector2(-1.74f, 1.22f)} , //back leg top
                                    { new Vector2(-1.74f, 1.22f), new Vector2(-2.6f, 0.2f) } , //back leg bottom
                                    { new Vector2(-0.54f, 1.77f), new Vector2(-1.26f, 0.09f) } , //2nd leg (from the left)
                                    { new Vector2(0.14f, 1.52f), new Vector2(-0.53f, 0.84f)} , //3rd leg top
                                    { new Vector2(-0.53f, 0.84f), new Vector2(-0.74f, 0.06f) } , //3rd leg bottom
                                    { new Vector2(0.47f, 1.24f), new Vector2(0.06f, -0.1f) } , //4th leg
                                    { new Vector2(1.8f, 1.18f), new Vector2(1.88f, -0.23f)} }; //5th leg
    void Start() {
        //Set screen orientation
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    void Update()
    {
        if (order >= 9) {
            //Align lines into place
            if (finished == 9) {
                StartVideo();
            }
        }
        else if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            touchpos.z = 0;
            transform.position = touchpos;

            if (touch.phase == TouchPhase.Began) {
                Instantiate(line, transform.position, transform.rotation);
            }
        }
    }

    void StartVideo() {
        var script = video.GetComponent<Video>();
        script.Play();
    }
}
