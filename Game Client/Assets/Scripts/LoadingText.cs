using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingText : MonoBehaviour
{
    public string[] loadingTexts = new string[] { "Summoning the Dark Lord...", "Uninstalling overwatch.exe...", "Setting Internet Explorer as default browser...", "Installing spyware...", "Collecting browser history...", "Finding hot singles in your area...", "Crashing server...", "Breeding bits..."};

    public TextMeshProUGUI loadingText;

    private void Start()
    {

        int randomText = UnityEngine.Random.Range(0, loadingTexts.Length);
        loadingText.text = loadingTexts[randomText];
    }
}
