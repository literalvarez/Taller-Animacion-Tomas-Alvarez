using UnityEngine;
using Cinemachine;

public class MoveCams : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePoints; // An array to store the points the object should move between.
    [SerializeField]
    private CinemachineVirtualCamera[] virtualCameras; // Array to store virtual cameras. Assign them manually through the Unity Editor.
    private int currentPointIndex = 0; // Index of the current move point.

    private float moveSpeed = 0f; // Speed at which the object moves between points.
    private float normalMoveSpeed = 3f; // Normal move speed when only W is pressed.
    private float sprintMoveSpeed = 6f; // Move speed when W and Shift are pressed.

    void Update()
    {
        // Check for movement input and set the move speed accordingly.
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = sprintMoveSpeed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = normalMoveSpeed;
        }
        else
        {
            moveSpeed = 0f;
        }

        // Calculate the distance to the current move point.
        float distanceToTarget = Vector3.Distance(transform.position, movePoints[currentPointIndex].position);

        // Move the object towards the current move point with the specified speed.
        // The Mathf.Min is used to prevent overshooting the target.
        transform.position = Vector3.MoveTowards(transform.position, movePoints[currentPointIndex].position, Mathf.Min(moveSpeed * Time.deltaTime, distanceToTarget));

        // Rotate the object to face the current move point.
        Vector3 directionToTarget = movePoints[currentPointIndex].position - transform.position;
        directionToTarget.y = 0;
        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Check if the object has reached the current move point.
        if (distanceToTarget < 0.1f)
        {
            // Update the current move point index to move to the next point (with wrapping around to the first point).
            currentPointIndex = (currentPointIndex + 1) % movePoints.Length;

            // Switch active virtual camera when reaching a specific point based on the index.
            if (currentPointIndex < virtualCameras.Length)
            {
                // Deactivate all virtual cameras.
                foreach (var camera in virtualCameras)
                {
                    camera.gameObject.SetActive(false);
                }

                // Activate the virtual camera at the current index.
                virtualCameras[currentPointIndex].gameObject.SetActive(true);
            }
        }
    }
}
