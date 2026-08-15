using UnityEngine;
using System.Collections;

public class CameraSafeguard : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform cameraTransform;
    public Transform targetPos;
    public Transform otherPos;
    public float duration = 1.0f;


    
    
private void OnTriggerEnter(Collider other)
    {
        if (cameraTransform.position == otherPos.position)
        {
          StartMovingCamera();  
        }
     
    }

    public void StartMovingCamera()
    {
        if (cameraTransform != null && targetPos != null && otherPos != null)
        {
            StartCoroutine(MoveCameraSafeRoutine());
        }
    }

    private IEnumerator MoveCameraSafeRoutine()
    {
        float elapsedTime = 0f;

        // Position (Vector3) and Rotation (Quaternion) as data only
        Pose startPos = new Pose(transform.position, transform.rotation);
        Pose endPos = new Pose(transform.position, transform.rotation);



    // Logically determine where to start and end based on current camera position
        if (cameraTransform.position == otherPos.position)
        {
            startPos.position = otherPos.position;
            startPos.rotation = otherPos.rotation;
            endPos.position = targetPos.position;
            endPos.rotation = targetPos.rotation;
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
