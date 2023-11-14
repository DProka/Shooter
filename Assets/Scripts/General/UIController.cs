using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static float fps;
    [SerializeField] TextMeshProUGUI fpsText;

    public void UpdateUI()
    {



    }

    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        fpsText.text = $"FPS: {(int)fps}";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
