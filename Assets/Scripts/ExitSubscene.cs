using UnityEngine;

public class ExitSubscene : MonoBehaviour
{

// Call when objectives are complete
public void CompleteObjective()
{
    Interactable mainScript = Object.FindFirstObjectByType<Interactable>();
    if (mainScript != null)
    {
        mainScript.ResumeMainScene();
    }
}
}
