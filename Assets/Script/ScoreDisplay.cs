using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI tmpCText;
    [SerializeField] private TextMeshProUGUI tmpBText;
    [SerializeField] private TextMeshProUGUI tmpAText;
    [SerializeField] private TextMeshProUGUI tmpSText;

    private const int scoreCThreshold = 300000;
    private const int scoreBThreshold = 500000;
    private const int scoreAThreshold = 700000;
    private const int scoreSThreshold = 900000;

    private void Update()
    {
        int score = GManager.instance.score;
        scoreText.text = score.ToString();

        if (score >= scoreCThreshold)
        {
            tmpCText.gameObject.SetActive(true);
        }
        else
        {
            tmpCText.gameObject.SetActive(false);
        }

        if (score >= scoreBThreshold)
        {
            tmpBText.gameObject.SetActive(true);
        }
        else
        {
            tmpBText.gameObject.SetActive(false);
        }

        if (score >= scoreAThreshold)
        {
            tmpAText.gameObject.SetActive(true);
        }
        else
        {
            tmpAText.gameObject.SetActive(false);
        }

        if (score >= scoreSThreshold)
        {
            tmpSText.gameObject.SetActive(true);
        }
        else
        {
            tmpSText.gameObject.SetActive(false);
        }
    }
}
