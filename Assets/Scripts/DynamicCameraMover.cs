using UnityEngine;
using System.Collections;

public class DynamicCameraMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform cameraTransform;
    public Transform firstPos;
    public Transform secondPos;
    public float duration = 2.0f;


    
    
private void OnTriggerEnter(Collider other)
    {
     StartMovingCamera();
    }

    public void StartMovingCamera()
    {
        if (cameraTransform != null && firstPos != null && secondPos != null)
        {
            StartCoroutine(MoveCameraRoutine());
        }
    }

    private IEnumerator MoveCameraRoutine()
    {
        float elapsedTime = 0f;

        // Position (Vector3) and Rotation (Quaternion) as data only
        Pose startPos = new Pose(transform.position, transform.rotation);
        Pose endPos = new Pose(transform.position, transform.rotation);



    // Logically determine where to start and end based on current camera position
        if(cameraTransform.position == firstPos.position)
        {
            startPos.position = firstPos.position;
            startPos.rotation = firstPos.rotation;
            endPos.position = secondPos.position;
            endPos.rotation = secondPos.rotation;
        }
        else if (cameraTransform.position == secondPos.position)
        {
            startPos.position = secondPos.position;
            startPos.rotation = secondPos.rotation;
            endPos.position = firstPos.position;
            endPos.rotation = firstPos.rotation;
        } else
        {
            Debug.Log("Dynamic camera error");
        }

        cameraTransform.position = startPos.position;
        cameraTransform.rotation = startPos.rotation;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;

            // Smoothly interpolate position:
                // Linear movement: 
                //cameraTransform.position = Vector3.Lerp(startPosition.position, endPosition.position, percentageComplete);
                float smoothPercentage = Mathf.SmoothStep(0f, 1f, percentageComplete);
                // Exponential ease:
                cameraTransform.position = Vector3.Lerp(startPos.position, endPos.position, smoothPercentage);

            
            // Smoothly interpolate rotation
            cameraTransform.rotation = Quaternion.Lerp(startPos.rotation, endPos.rotation, percentageComplete);

            yield return null; // Wait until the next frame
        }

        // Ensure it snaps perfectly to the final position and rotation
        cameraTransform.position = endPos.position;
        cameraTransform.rotation = endPos.rotation;
    }
}
