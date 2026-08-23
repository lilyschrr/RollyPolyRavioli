using UnityEngine;

public class ReadGallery : MonoBehaviour
{
    [SerializeField] private ImageGallery gallery;


    public void CompleteObjective()
    {
        SubSceneResult result = new SubSceneResult
        {
            score = 100,
            isCompleted = true,
            message = "Recipe Saved!",
            recipe = gallery.CurrentIndex
        };

        // Call ResumeMainScene on the specific interactable that opened this scene
        if (Interactable.CurrentActive != null)
        {
            Interactable.CurrentActive.ResumeMainScene(result);
        }
        else
        {
            Debug.LogError("No active Interactable found! Make sure the subscene was opened via an Interactable trigger.");
        }
    }
}
