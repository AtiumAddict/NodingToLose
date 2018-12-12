using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovementMac : MonoBehaviour
{
    public float movementSpeed = 50f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();                 // Set rb as the RigidBody of the CubeManager.
    }

    void Update()
    {
        // Movement Controls:
        if (Input.GetKeyDown("left shift"))
        {
            transform.position = GetNearestPointOnGridY(transform.position);
            MoveUp();
        }

        if (Input.GetKeyDown("space"))
        {
            transform.position = GetNearestPointOnGridY(transform.position);
            MoveDown();
        }

        if (Input.GetKeyDown("d"))
        {
            transform.position = GetNearestPointOnGridX(transform.position);
            MoveRight();
        }

        if (Input.GetKeyDown("a"))
        {
            transform.position = GetNearestPointOnGridX(transform.position);
            MoveLeft ();
        }

        if (Input.GetKeyDown("w"))
        {
            transform.position = GetNearestPointOnGridZ(transform.position);
            MoveForward();
        }

        if (Input.GetKeyDown("s"))
        {
            transform.position = GetNearestPointOnGridZ(transform.position);
            MoveBackward();
        }
    }

    // Movement directions:
    private void MoveUp()
    {
        rb.velocity = transform.forward * movementSpeed;
    }

    private void MoveDown()
    {
        rb.velocity = -transform.forward * movementSpeed;
    }

    private void MoveRight ()
    {
        rb.velocity = transform.right * movementSpeed;
    }

    private void MoveLeft ()
    {
        rb.velocity = -transform.right * movementSpeed;
    }

    private void MoveForward()
    {
        rb.velocity = transform.up * movementSpeed;
    }

    private void MoveBackward()
    {
        rb.velocity = -transform.up * movementSpeed;
    }

    // Calculate the nearest round number Vector3 to the current position, when you are moving across the x axis:
    public Vector3 GetNearestPointOnGridX(Vector3 position)
    {
        float xRound = position.x;
        int yRound = Mathf.RoundToInt(position.y);
        int zRound = Mathf.RoundToInt(position.z);

        Vector3 result = new Vector3(xRound, yRound, zRound);

        return result;
    }

    // Calculate the nearest round number Vector3 to the current position, when you are moving across the y axis:
    public Vector3 GetNearestPointOnGridY(Vector3 position)
    {
        int xRound = Mathf.RoundToInt(position.x);
        float yRound = position.y;
        int zRound = Mathf.RoundToInt(position.z);

        Vector3 result = new Vector3(xRound, yRound, zRound);

        return result;
    }

    // Calculate the nearest round number Vector3 to the current position, when you are moving across the z axis:
    public Vector3 GetNearestPointOnGridZ(Vector3 position)
    {
        int xRound = Mathf.RoundToInt(position.x);
        int yRound = Mathf.RoundToInt(position.y);
        float zRound = position.z;

        Vector3 result = new Vector3(xRound, yRound, zRound);

        return result;
    }
}
