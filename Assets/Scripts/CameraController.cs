using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Variables")]
    public float rotationSpeed;
    public float distance;
    [Header("Pivot Around Point")]
    [SerializeField] Transform PivotPoint;

    [Header("LineRenderer Thigns")]
    LineRenderer lineofsight;
    public Gradient redColor;
    public Gradient hitColor;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        lineofsight = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.RotateAround(PivotPoint.position, Vector3.forward * rotationSpeed, 90f);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            lineofsight.SetPosition(1, hitInfo.point);
            if (hitInfo.collider.CompareTag("Player"))
            {
                lineofsight.colorGradient = hitColor;
                Destroy(hitInfo.collider.gameObject);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
            lineofsight.SetPosition(1, transform.position + transform.right * distance);
            lineofsight.colorGradient = redColor;
        }
        lineofsight.SetPosition(0, transform.position);
    }
}

