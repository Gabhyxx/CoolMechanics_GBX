using UnityEngine;

public class ScreenToRay : MonoBehaviour
{
    Camera cam;
    Vector3 pos = new Vector3(Screen.width/2, Screen.height/2, 0);


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}
