using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range;
    public Transform target;
    public bool rotates = false;
    [SerializeField] playerController pc;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(rotates){
            transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * 30, 90) - 45);
        }

        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, -fovPoint.up);
        RaycastHit2D ray = Physics2D.Raycast(fovPoint.position, dir, range);
        
        if (angle < fovAngle / 2)
        {
            if(ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(fovPoint.position, dir, Color.blue);
                    if (pc.currentColor != ColorState.Red) 
                    {
                       GameManager.isGameOver = true;
                    }
                   
                }
            }
            
        }
    }
}
