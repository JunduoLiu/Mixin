using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    Transform castPoint;

    private Mesh mesh;
    private float fovAngle;
    private float angle;
    private float angleIncrease;
    private float distance;
    private int raycount;
    private Vector3 origin;
    public LayerMask layerMask;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        fovAngle = 90f;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, 45 * Time.deltaTime));
        //light.transform.Rotate(new Vector3(0, 0, 45*Time.deltaTime));

        raycount = 50;
        angle = 0f;
        angleIncrease = fovAngle / raycount;
        distance = 3f;
        origin = Vector3.zero;
        layerMask = LayerMask.NameToLayer("item");
        
        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;
        int vertextIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= raycount; i++)
        {
            Vector3 vertex = origin + UtilsClass.GetVectorFromAngle(angle) * distance;
 
            /* Method 1 with Raycast
            RaycastHit2D raycast = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), distance);
            //Debug.Log(transform.position);
            if (raycast)
            {
                vertex = raycast.point;
                Debug.Log("hit: " + raycast.collider.name);
            }
            else
            {
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * distance;
            }*/

            /* Method 2 without Raycast
            Collider2D targetInView = Physics2D.OverlapCircle(vertex, 3f);
            Debug.Log(targetInView.name);
            Transform targetView = targetInView.transform;
            Vector3 dirToTarget = (targetView.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < angle / 2)
            {
                Debug.Log("hit");
            }*/

                vertices[vertextIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertextIndex - 1;
                triangles[triangleIndex + 2] = vertextIndex;

                triangleIndex += 3;
            }

            vertextIndex++;
            angle -= angleIncrease; //clockwise
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        
        
    }

    /*bool SeePlayer()
    {
        bool val = false;

        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, castPoint.up);
        Debug.DrawRay(castPoint.position, dir, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(castPoint.position, dir, range);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Pickable Item"))
            {
                val = true;
            }
        }
        return val;
    }*/

}