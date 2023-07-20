using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextManager : MonoBehaviour
{
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI greatText;
    public TextMeshProUGUI badText;
    public TextMeshProUGUI missText;

    void Update()
    {
        perfectText.text = GManager.instance.perfect.ToString();
        greatText.text =  GManager.instance.great.ToString();
        badText.text =  GManager.instance.bad.ToString();
        missText.text =  GManager.instance.miss.ToString();
    }
}