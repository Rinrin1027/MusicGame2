using UnityEngine;
using UnityEngine.UI;

public class ScoregaugeManager : MonoBehaviour
{
    public Slider scoreGauge; // スライダーコンポーネント

    private float maxScore = 1000000f; // 最大スコア

    private void Start()
    {
        scoreGauge = GetComponent<Slider>(); // スライダーコンポーネントを取得
        scoreGauge.value = 0; // 初期値を0に設定
    }

    public void ResetScoreGauge()
    {
        scoreGauge.value = 0;
    }

    private void Update()
    {
        // スコアに応じてスライダーゲージを更新
        float normalizedScore = Mathf.Clamp01((float)GManager.instance.score / maxScore);
        scoreGauge.value = normalizedScore;
    }
}
