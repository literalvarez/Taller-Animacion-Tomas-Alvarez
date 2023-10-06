using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 3f; // Rotation speed around Y-axis

    void FixedUpdate()
    {
        // Calculate the rotation amount based on time and rotationSpeed
        float rotationAmount = rotationSpeed * Time.fixedDeltaTime;

        // Rotate the game object around its Y-axis
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
