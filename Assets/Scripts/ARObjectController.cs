using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



public class ARObjectController : MonoBehaviour
{
   [System.Serializable]
    public struct TagToPlanet
    {
        public string tag;
        public GameObject planet;
    }

    [SerializeField]
    public GameObject canvas;
    [SerializeField]
    public ARPlaneManager manager;
    [SerializeField]
    public Transform ScaleTransform;
    [SerializeField]
    private GameObject objectPrefab;
    [SerializeField]
    public ARRaycastManager aRRaycastManager;
    [SerializeField]
    public Text text;
    private List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
    [SerializeField]
    private TagToPlanet[] planets;
    [SerializeField]
    public GameObject sun;
 
    public float initialFingersDistance;
    public Vector3 initialScale;

    private int count = 0;
    void Start()
    {
        canvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (count == 0)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (aRRaycastManager.Raycast(touch.position, aRRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                    {
                        //Touch hits a plane
                        Pose pose = aRRaycastHits[0].pose;
                        if (count == 0)
                        {
                            objectPrefab.SetActive(true);
                            count++;
                            objectPrefab.transform.position = pose.position;
                            manager.planePrefab = null;
                            foreach (var plane in manager.trackables)
                            {
                                plane.gameObject.SetActive(false);
                            }
                        }
                        //return;
                    }
                }
            }
            else
            {
                //text.text = "Make touch " + count.ToString();
                if(touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    //text.text = "Make raycast ";
                    RaycastHit hitObject;

                    if (Physics.Raycast(ray, out hitObject))
                    {
                        //hitObject.SetActive(false);
                        int ind = findPlanetByTag(hitObject.transform.tag);
                        if (ind == -1)
                        {
                            //canvas.SetActive(false);
                            return;
                        }
                        canvas.SetActive(true);
                        text.text = planets[ind].planet.GetComponent<Planet>().about;
                    }
                }
                
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (aRRaycastManager.Raycast(touch.position, aRRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = aRRaycastHits[0].pose;
                    sun.transform.position = pose.position;

                }
            }
            

        }
        int fingersOnScreen = 0;

        foreach (Touch touch in Input.touches)
        {
            fingersOnScreen++; //Count fingers (or rather touches) on screen as you iterate through all screen touches.

            //You need two fingers on screen to pinch.
            if (fingersOnScreen == 2)
            {

                //First set the initial distance between fingers so you can compare.
                if (touch.phase == TouchPhase.Began)
                {
                    initialFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                    initialScale = ScaleTransform.localScale;
                }
                else
                {
                    float currentFingersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                    float scaleFactor = currentFingersDistance / initialFingersDistance;

                    //transform.localScale = initialScale * scaleFactor;
                    ScaleTransform.localScale = initialScale * scaleFactor;
                }
            }
        }

    }

    int findPlanetByTag(string tag)
    {
        for (int j = 0; j < planets.Length; j++)
        {
            if (planets[j].tag == tag)
            {
                return j;
            }
        }
        return -1;
    }
}
