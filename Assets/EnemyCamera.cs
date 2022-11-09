using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 15;
    public Transform target;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 30, 90) - 45);

        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir,-fovPoint.up);
        RaycastHit2D ray = Physics2D.Raycast(fovPoint.position, dir, range);

        if(angle < fovAngle / 2)
        {
            if (ray.collider.CompareTag("Player")){
                Debug.Log("Spotted");
                Debug.DrawRay(fovPoint.position, dir, Color.red);
            }
            else
            {
                Debug.Log("Nothing Seen");
            }
        }
    }
}
