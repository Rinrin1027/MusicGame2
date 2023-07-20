using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButtonScript : MonoBehaviour
{
    public Button homeButton; // ホームボタン
    public NotesManager notesManager; // ノートマネージャーをInspectorから設定してください
    public ScoregaugeManager scoreGaugeManager; // スコアゲージマネージャーをInspectorから設定してください
    public GameObject[] messageObjects; // メッセージオブジェクトもInspectorから設定してください

    void Start()
    {
        // ホームボタンのクリックイベントにResetGameAndGoHomeメソッドを設定
        homeButton.onClick.AddListener(ResetGameAndGoHome);
    }

    void ResetGameAndGoHome()
    {
        // ゲームの各パラメータをリセット
        GManager.instance.score = 0;
        GManager.instance.combo = 0;
        GManager.instance.perfect = 0;
        GManager.instance.great = 0;
        GManager.instance.bad = 0;
        GManager.instance.miss = 0;
        GManager.instance.ratioScore = 0;

        if (notesManager != null)
            notesManager.ResetNotesData(); // ノートデータをリセット

        if (scoreGaugeManager != null)
            scoreGaugeManager.ResetScoreGauge(); // スコアゲージをリセット

        // メッセージオブジェクトが存在すれば破棄
        if (messageObjects != null && messageObjects.Length > 0)
        {
            foreach (GameObject messageObject in messageObjects)
            {
                if (messageObject != null)
                    Destroy(messageObject);
            }
        }

        // Homepartシーンに遷移
        SceneManager.LoadScene("Homepart");
    }
}