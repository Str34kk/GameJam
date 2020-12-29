//Attach this script to the GameObject you would like to have mouse hovering detected on
//This script outputs a message to the Console when the mouse pointer is currently detected hovering over the GameObject and also when the pointer leaves.

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class btnAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Sprite[] images;
    public float animFrameTime;

    Coroutine coroutine;
    IEnumerator AnimRoutine()
    {
        int imageIndex = 0;
        while (true)
        {
            image.sprite = images[imageIndex];
            imageIndex = (imageIndex + 1) % images.Length;
            yield return new WaitForSeconds(animFrameTime);
        }
    }
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        coroutine = StartCoroutine(AnimRoutine());
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        StopCoroutine(coroutine);
        image.sprite = images[0];
    }
}