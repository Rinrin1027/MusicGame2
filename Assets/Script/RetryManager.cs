using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RetryManager : MonoBehaviour
{
    private Button retryButton;

    [SerializeField] private GameObject[] messageObjects;
    [SerializeField] private NotesManager notesManager;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoregaugeManager scoreGaugeManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    private float buttonPressTime = 0;
    private int buttonPressCount = 0;
    private readonly float timeThreshold = 1f; // 1秒以内
    private readonly int pressThreshold = 3; // 3回押す

    private void Start()
    {
        retryButton = GetComponent<Button>();
        retryButton.onClick.AddListener(() => {
            buttonPressCount++;
            buttonPressTime = Time.time;
            if (buttonPressCount >= pressThreshold)
            {
                SceneManager.LoadScene("Homepart");
                GManager.instance.score = 0;
                GManager.instance.combo = 0;
                GManager.instance.perfect = 0;
                GManager.instance.miss = 0;
                GManager.instance.ratioScore = 0;

                if (notesManager != null)
                    notesManager.ResetNotesData();

                if (scoreGaugeManager != null)
                    scoreGaugeManager.ResetScoreGauge();

                if (messageObjects != null && messageObjects.Length > 0)
                {
                    foreach (GameObject messageObject in messageObjects)
                    {
                        if (messageObject != null)
                            Destroy(messageObject);
                    }
                }
            }
        });

        InvokeRepeating(nameof(CheckButtonPressCount), timeThreshold, timeThreshold);
    }

    private void CheckButtonPressCount()
    {
        if (Time.time - buttonPressTime >= timeThreshold)
        {
            if (buttonPressCount > 0 && buttonPressCount < pressThreshold)
            {
                RetryLevel();
            }
            buttonPressCount = 0;
        }
    }

    public void RetryLevel()
    {
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetGame()
    {
        GManager.instance.score = 0;
        GManager.instance.combo = 0;
        GManager.instance.perfect = 0;
        GManager.instance.miss = 0;
        GManager.instance.ratioScore = 0;

        if (notesManager != null)
            notesManager.ResetNotesData();

        if (scoreGaugeManager != null)
            scoreGaugeManager.ResetScoreGauge();

        if (messageObjects != null && messageObjects.Length > 0)
        {
            foreach (GameObject messageObject in messageObjects)
            {
                if (messageObject != null)
                    Destroy(messageObject);
            }
        }
    }
    // other methods as before
}
