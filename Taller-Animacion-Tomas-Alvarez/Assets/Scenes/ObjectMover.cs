using UnityEngine;
using Cinemachine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToMove;

    private bool isClicked = false;

    void Update()
    {
        // Get the active virtual camera from CinemachineBrain
        CinemachineBrain cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        if (cinemachineBrain == null)
        {
            Debug.LogError("CinemachineBrain not found in the scene.");
            return;
        }

        // Get the active virtual camera's transform
        Transform virtualCameraTransform = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.transform;

        // Check for mouse button down to start moving
        if (Input.GetMouseButtonDown(0))
        {
            // Set isClicked to true to enable movement
            isClicked = true;
        }

        // Check for mouse button release to stop moving
        if (Input.GetMouseButtonUp(0))
        {
            // Set isClicked to false to stop movement
            isClicked = false;
        }

        // If isClicked is true, move the object based on mouse position
        if (isClicked)
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Keep the z-coordinate of the object unchanged
            mousePosition.z = objectToMove.transform.position.z;
            // Move the object to the mouse position
            objectToMove.transform.position = mousePosition;
        }
    }
}
