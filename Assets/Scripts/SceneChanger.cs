using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing scenes

public class SceneChanger : MonoBehaviour
{
    // Call from button
    public void LoadTargetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}