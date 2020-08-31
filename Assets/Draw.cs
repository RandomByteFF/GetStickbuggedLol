using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;

public class Draw : MonoBehaviour
{
    public GameObject video;
    public GameObject line;
    private Touch touch;
    public int order;
    public int finished = 0;
    public float wait;
    private bool doneWaiting = false;
    public AudioClip[] clips;
    private bool playedAudio = false;
    public float wait2;
    private bool waitingForVideo = false;

    private List<GameObject> lines;
    //if there is a better way pls tell me. I can only think about reading this from a file.
    public Vector3[,] positions = { { new Vector3(2.17f, 1.3f, 0f), new Vector3(0.62f, 1.3f, 0f) } , //back
                                    { new Vector3(-0.92f, 2.13f, 0f), new Vector3(0.51f, 1.37f, 0f) } , //front
                                    { new Vector3(-0.5f, 1.83f, 0f), new Vector3(-1.74f, 1.22f, 0f)} , //back leg top
                                    { new Vector3(-1.74f, 1.22f, 0f), new Vector3(-2.6f, 0.2f, 0f) } , //back leg bottom
                                    { new Vector3(-0.54f, 1.77f, 0f), new Vector3(-1.26f, 0.09f, 0f) } , //2nd leg (from the left)
                                    { new Vector3(0.14f, 1.52f, 0f), new Vector3(-0.53f, 0.84f, 0f)} , //3rd leg top
                                    { new Vector3(-0.53f, 0.84f, 0f), new Vector3(-0.74f, 0.06f, 0f) } , //3rd leg bottom
                                    { new Vector3(0.47f, 1.24f, 0f), new Vector3(0.06f, -0.1f, 0f) } , //4th leg
                                    { new Vector3(1.8f, 1.18f, 0f), new Vector3(1.88f, -0.23f, 0f)} }; //5th leg
    void Start() {
        lines = new List<GameObject>();
        video = GameObject.Find("Video Player");
    }

    void Update()
    {
        if (order >= 9) {
            if (wait <= 0 && !doneWaiting)
            {
                GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = Color.black;
                doneWaiting = true;
                GetComponent<AudioSource>().clip = clips[0];
                GetComponent<AudioSource>().Play();
            }
            else if (!doneWaiting)
            {
                wait -= Time.deltaTime;
            }
            //Align lines into place
            else if (finished == 9)
            {
                if (!waitingForVideo)
                {
                    StartVideo();
                    waitingForVideo = true;
                }
                else if(GameObject.Find("Video Player").GetComponent<VideoPlayer>().isPlaying){
                    foreach (GameObject i in lines)
                    {
                        Destroy(i);
                    }
                    Destroy(gameObject);
                }
            }
            else if (!playedAudio && wait2 >= 0) {
                wait2 -= Time.deltaTime;
            }
            else if (!playedAudio)
            {
                GetComponent<AudioSource>().clip = clips[1];
                GetComponent<AudioSource>().Play();
                order++;
                playedAudio = true;
            }
        }
        else if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            touchpos.z = 0;
            transform.position = touchpos;

            if (touch.phase == TouchPhase.Began) {
                lines.Add(Instantiate(line, transform.position, transform.rotation));
                lines[lines.Count - 1].GetComponent<Line>().script = gameObject;
            }
        }
    }

    void StartVideo() {
        var script = video.GetComponent<Video>();
        script.Play();
    }
}
