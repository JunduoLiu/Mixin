using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycasttest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
    if (Input.GetMouseButtonDown(0)) {
        Debug.Log("Pressed left click, casting ray.");
        CastRay();
    }
}

void CastRay() {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100)) {
        Debug.DrawLine(ray.origin, hit.point);
        Debug.Log("Hit object: " );
    }
}
}
