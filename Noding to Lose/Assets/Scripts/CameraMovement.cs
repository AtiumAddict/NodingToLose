using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;
    public Transform center;

    public float zoomSpeed = 1f;

    [Space]
    [Header("Position")]
    public float camPosX;
    public float camPosY;
    public float camPosZ;

    [Space]
    [Header("Rotation")]
    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;

    [Space]
    [Range(0f, 10f)]
    public float turnSpeed;

    private void Start()
    {
        offset = new Vector3(center.position.x + camPosX, center.position.y + camPosY, center.position.z + camPosZ);
        transform.rotation = Quaternion.Euler(camRotationX, camRotationY, camRotationZ);
    }

    private void LateUpdate()
    {
        if (center != null)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") 
                * -turnSpeed, Vector3.right) * offset;
            
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                if (transform.position.z < center.position.z)
                {
                    if (center.position.z - transform.position.z < 30)
                    {
                        offset.z += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                    }
                }
                else
                {
                    if (transform.position.z - center.position.z < 30)
                    {
                        offset.z -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
                    }
                }
            }

            transform.position = center.position + offset;
            transform.LookAt(center.position);
        }
    }
}
