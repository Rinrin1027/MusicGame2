using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    private TextMeshProUGUI speedText;

    private void Start()
    {
        speedText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (GManager.instance != null)
        {
            speedText.text = GManager.instance.noteSpeed.ToString("0.00");
        }
    }
}
