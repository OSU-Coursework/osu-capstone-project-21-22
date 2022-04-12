using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    public IEnumerator FadeOut()
    {
        for (float t = 0f; t < 3f; t += Time.deltaTime)
        {
            float norm = t / 3f;
            gameObject.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.black, norm);
            yield return null;
        }
        gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
    }

    public IEnumerator FadeIn()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime)
        {
            float norm = t / 1f;
            gameObject.GetComponent<Image>().color = Color.Lerp(Color.black, Color.clear, norm);
            yield return null;
        }
        gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
    }
}
