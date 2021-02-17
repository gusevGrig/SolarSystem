using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLines : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject Line;

    [SerializeField]
    public List<GameObject> planets;
    void Start()
    {
        foreach (GameObject planet in planets)
        {
            Planet pl = planet.GetComponent<Planet>();
            pl.Start();
            GameObject line = Instantiate(Line, gameObject.transform.position, new Quaternion(90, 0, 0, 1));
            Line l = line.GetComponent<Line>();
            l.segments = 100;
            l.xradius = Vector3.Distance(gameObject.transform.position, planet.transform.position);
            l.yradius = l.xradius;
            l.planet = planet;
            l.sun = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
