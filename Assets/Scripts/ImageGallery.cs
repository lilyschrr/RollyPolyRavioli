using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageGallery : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image displayImage; 

    [Header("Gallery Data")]
    [SerializeField] private List<Sprite> imageList = new List<Sprite>(); 

    private int currentIndex = 0; 


    public int CurrentIndex 
    {
        get { return currentIndex; }
    }


    void Start()
    {
        UpdateGalleryDisplay();
    }

    public void ShowNextImage()
    {
        if (imageList.Count == 0) return;
        currentIndex++;
        if (currentIndex >= imageList.Count) currentIndex = 0;
        UpdateGalleryDisplay();
    }

    public void ShowPreviousImage()
    {
        if (imageList.Count == 0) return;
        currentIndex--;
        if (currentIndex < 0) currentIndex = imageList.Count - 1;
        UpdateGalleryDisplay();
    }

    private void UpdateGalleryDisplay()
    {
        if (imageList.Count > 0 && displayImage != null)
        {
            displayImage.sprite = imageList[currentIndex];
        }
    }
}
