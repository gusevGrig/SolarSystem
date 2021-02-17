using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Planet : MonoBehaviour
{
    public GameObject sun;
    public float distance;
    public float time;
    public TextAsset text;
    public string about;
    float speed;
    public void Start()
    {
        //adjust distance
        speed = 2 * Mathf.PI * distance / time;
        about = text.text;
        transform.position = (new Vector3(1, 0, 0)) * distance + sun.transform.position;
    }
    void Update()
    {
        if (sun != null)
        {
            //Orbit at distance
            transform.RotateAround(sun.transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}