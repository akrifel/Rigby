using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlelr : MonoBehaviour
{
    public float rotationSpeed;
    public float distance;

    LineRenderer lineofsight;
    public Gradient redColor;
    public Gradient hitColor; 

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        lineofsight = GetComponentInChildren<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hitInfo.collider != null)
        { 
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            lineofsight.SetPosition(1, hitInfo.point);
            if(hitInfo.collider.CompareTag("Player"))
            {
                lineofsight.colorGradient = hitColor;
                Destroy(hitInfo.collider.gameObject);
            }
        }
        else { Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
            lineofsight.SetPosition(1, transform.position + transform.right * distance);
            lineofsight.colorGradient = redColor;
        }
        lineofsight.SetPosition(0, transform.position);
    }
}
