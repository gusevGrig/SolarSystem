using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    [SerializeField]
    public LineRenderer line;
    public GameObject planet;
    public GameObject sun;

    void Start()
    {
        //line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        
        CreatePoints();
        gameObject.transform.rotation = new Quaternion(90, 0, 0, 1);
    }


    void CreatePoints()
    {
        float x;
        float z;
        float y = 0f;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }

    private void Update()
    {
        //gameObject.transform.localScale = sun.transform.localScale * planet.transform.localScale;
        gameObject.transform.position = sun.transform.position;
        float r = Vector3.Distance(gameObject.transform.position, planet.transform.position);
        if (r != xradius)
        {
            xradius = r;
            yradius = xradius;
            CreatePoints();
        }
    }
}