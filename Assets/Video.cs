using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public Camera camera;
    public void Play() {
        camera.backgroundColor = Color.black;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }
}
