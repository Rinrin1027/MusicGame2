using UnityEngine;
using TMPro;

public class ScoreGradeManager : MonoBehaviour
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

        if (score < scoreCThreshold)
        {
            tmpCText.gameObject.SetActive(false);
            tmpBText.gameObject.SetActive(false);
            tmpAText.gameObject.SetActive(false);
            tmpSText.gameObject.SetActive(false);
        }
        else if (score < scoreBThreshold)
        {
            tmpCText.gameObject.SetActive(true);
            tmpBText.gameObject.SetActive(false);
            tmpAText.gameObject.SetActive(false);
            tmpSText.gameObject.SetActive(false);
        }
        else if (score < scoreAThreshold)
        {
            tmpCText.gameObject.SetActive(false);
            tmpBText.gameObject.SetActive(true);
            tmpAText.gameObject.SetActive(false);
            tmpSText.gameObject.SetActive(false);
        }
        else if (score < scoreSThreshold)
        {
            tmpCText.gameObject.SetActive(false);
            tmpBText.gameObject.SetActive(false);
            tmpAText.gameObject.SetActive(true);
            tmpSText.gameObject.SetActive(false);
        }
        else
        {
            tmpCText.gameObject.SetActive(false);
            tmpBText.gameObject.SetActive(false);
            tmpAText.gameObject.SetActive(false);
            tmpSText.gameObject.SetActive(true);
        }
    }
}