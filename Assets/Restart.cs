using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    private Draw comp;
    public float wait;
    private float time;
    public float speed;
    private bool restartSequence = false;
    SpriteRenderer renderer;
    public GameObject agent;
    private float transparency;

    void Start()
    {
        //Set screen orientation
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        comp = GameObject.Find("DrawAgent").GetComponent<Draw>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!restartSequence)
        {
            if (comp.order >= 10)
            {
                restartSequence = true;
                time = wait;
            }
        }
        else if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        else {
            if (!renderer.enabled) {
                renderer.enabled = true;
                transparency = 0;
            }
            if (transparency <= 1) {
                transparency += Time.deltaTime * speed;
                renderer.color = new Color(1f, 1f, 1f, transparency);
            }
            if (Input.touchCount > 0) {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPos = new Vector2(worldPoint.x, worldPoint.y);
                if (GetComponent<BoxCollider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    Debug.Log("A new round begins");
                    restartSequence = false;
                    comp = Instantiate(agent).GetComponent<Draw>();
                    renderer.enabled = false;
                    GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = new Color(68/255f, 103/255f, 159/255f, 1);
                    GameObject.Find("Video Player").GetComponent<VideoPlayer>().Stop(); //még szar lehet

                }
            }
        }
    }
}
