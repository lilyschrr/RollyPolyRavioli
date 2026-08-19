using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    private bool canInteract = false;
    private bool interacting = false;
    private string subSceneName;

    // Stores main scene's objects to turn back on later
    private List<GameObject> mainSceneRoots = new List<GameObject>();

    [Header("Objects")]
    public GameObject station;
    public GameObject player;
    public string sceneToEnter;

    [Header("Interactions")]
    public InputAction enterAction;

    private void OnEnable() => enterAction.Enable();
    private void OnDisable() => enterAction.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canInteract = false;
    }

    void Update()
    {
        if (enterAction.triggered && canInteract && !interacting)
        {
            interacting = true;
            LoadAndIsolateScene(sceneToEnter);
        }
    }

    public void LoadAndIsolateScene(string newSceneName)
    {
        subSceneName = newSceneName;
        
        // Get current active main scene
        Scene mainScene = SceneManager.GetActiveScene();
        mainSceneRoots.Clear();
        mainScene.GetRootGameObjects(mainSceneRoots);

        // Hide and disable everything in main scene
        foreach (GameObject root in mainSceneRoots)
        {
            root.SetActive(false);
        }

        // Load new scene additively
        SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive).completed += (op) => 
        {
            // Set new scene as active so new objects spawn there
            Scene newScene = SceneManager.GetSceneByName(newSceneName);
            SceneManager.SetActiveScene(newScene);
            
            // Re-enable time
            Time.timeScale = 1f; 
        };
    }

    public void ResumeMainScene()
    {
        // Unload completed sub-scene
        SceneManager.UnloadSceneAsync(subSceneName).completed += (op) =>
        {
            // Reactivate all hidden main scene objects
            foreach (GameObject root in mainSceneRoots)
            {
                if (root != null) root.SetActive(true);
            }

            // Reset states
            interacting = false;
            Time.timeScale = 1f;
        };
    }
}
