using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    // On scene load, wait a couple seconds then fade in
    private void Awake()
    {
        StartCoroutine(WaitOnLoad());
    }

    // Fade out over DUR seconds
    public IEnumerator FadeOut(float dur)
    {
        for (float t = 0f; t < dur; t += Time.deltaTime)
        {
            float norm = t / dur;
            gameObject.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.black, norm);
            yield return null;
        }
        Debug.Log("ere");
        gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
    }
    
    // Fade in over DUR seconds
    public IEnumerator FadeIn(float dur)
    {
        for (float t = 0f; t < dur; t += Time.deltaTime)
        {
            float norm = t / dur;
            gameObject.GetComponent<Image>().color = Color.Lerp(Color.black, Color.clear, norm);
            yield return null;
        }
        gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
    }


    // Wait for 1.5 seconds, then fade in fast
    private IEnumerator WaitOnLoad()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeIn(1f));
    }
}
