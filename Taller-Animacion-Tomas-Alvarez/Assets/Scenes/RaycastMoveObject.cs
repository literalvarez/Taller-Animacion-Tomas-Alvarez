using UnityEngine;
using Cinemachine;

public class RaycastMoveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToMove;

    [SerializeField]
    private Camera cam;

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Create a ray from the mouse position using the Cinemachine camera
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Cast the ray and check if it hits something
            if (Physics.Raycast(ray, out hit))
            {
                // Get the hit position
                Vector3 targetPosition = hit.point;

                // Move the specified game object to the hit position
                objectToMove.transform.position = targetPosition;
            }
        }
    }
}
