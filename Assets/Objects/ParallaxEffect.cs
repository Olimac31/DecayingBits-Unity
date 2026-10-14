using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Vector2 startpos = new Vector2(0, 0);
    private Vector2 length = new Vector2(0, 0);
    public float parallaxFactor;
    public GameObject cam; //Retrocamera object
 
    void Start()
    {
        startpos.x = transform.position.x;
        startpos.y = transform.position.y;
        length.x = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        length.y = GetComponentInChildren<SpriteRenderer>().bounds.size.y;
    }
 
    void LateUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);

        Vector2 distance = new Vector2(0, 0);
        distance.x = cam.transform.position.x * parallaxFactor;
        distance.y = cam.transform.position.y * parallaxFactor;

        Vector3 newPosition = new Vector3(startpos.x + distance.x, startpos.y + distance.y, transform.position.z);
        transform.position = newPosition;

        //Keep the background inside the boundaries of the camera
        // HORIZONTAL PARALLAX
        if (temp > startpos.x + (length.x / 2))    startpos.x += length.x;
        else if (temp < startpos.x - (length.x / 2)) startpos.x -= length.x;

        // VERTICAL PARALLAX
        temp = cam.transform.position.y * (1 - parallaxFactor);
        if (temp > startpos.y + (length.y / 2))    startpos.y += length.y;
        else if (temp < startpos.y - (length.y / 2)) startpos.y -= length.y;
    }
}
