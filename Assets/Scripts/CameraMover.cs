using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform cameraTransform; // Assign Main Camera here
    public Transform startPosition;   // Assign empty GameObject for Start
    public Transform endPosition;     // Assign empty GameObject for End
    public float duration = 2.0f;     // Time (sec) to complete movement

    // Call this function to trigger movement (e.g., a button or trigger)
    public void StartMovingCamera()
    {
        if (cameraTransform != null && startPosition != null && endPosition != null)
        {
            StartCoroutine(MoveCameraRoutine());
        }
        else
        {
            Debug.LogWarning("Assign all transforms in the inspector!");
        }
    }

    private IEnumerator MoveCameraRoutine()
    {
        float elapsedTime = 0f;

        // Set initial position and rotation to the start marker
        cameraTransform.position = startPosition.position;
        cameraTransform.rotation = startPosition.rotation;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;

            // Smoothly interpolate position:
                // Linear movement: 
                //cameraTransform.position = Vector3.Lerp(startPosition.position, endPosition.position, percentageComplete);
                float smoothPercentage = Mathf.SmoothStep(0f, 1f, percentageComplete);
                // Exponential ease:
                cameraTransform.position = Vector3.Lerp(startPosition.position, endPosition.position, smoothPercentage);

            
            // Smoothly interpolate rotation
            cameraTransform.rotation = Quaternion.Lerp(startPosition.rotation, endPosition.rotation, percentageComplete);

            yield return null; // Wait until the next frame
        }

        // Ensure it snaps perfectly to the final position and rotation
        cameraTransform.position = endPosition.position;
        cameraTransform.rotation = endPosition.rotation;
    }
}
