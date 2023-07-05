using UnityEngine;
using TMPro;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] private NotesManager notesManager;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    private void FixedUpdate()
    {
        // ゲームが開始されていない場合は何もしない
        if (!GManager.instance.Start)
            return;

        // キーボードの入力を処理する
        ProcessKeyboardInput();

        // ノーツのタイミングをチェックし、ミス判定を行う
        if (notesManager.NotesTime.Count > 0 && Time.time > notesManager.NotesTime[0] + 0.2f + GManager.instance.StartTime)
            HandleMiss();
    }

    private void ProcessKeyboardInput()
    {
        // 各レーンのキーボード入力を処理する
        if (Input.GetKeyDown(KeyCode.D))
        {
            ProcessInput(KeyCode.D, 0);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ProcessInput(KeyCode.F, 1);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            ProcessInput(KeyCode.J, 2);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ProcessInput(KeyCode.K, 3);
        }
    }

    public void ProcessInput(KeyCode keyCode, int laneIndex)
    {
        if (Input.GetKeyDown(keyCode))
        {
            // レーンに対応するオフセットを取得する
            int numOffset = FindLaneOffset(laneIndex);

            if (numOffset >= 0)
            {
                float timeLag = GetTimeLag(numOffset);

                // ノーツに対する判定を行う
                audioSource.PlayOneShot(hitSound);

                if (timeLag <= 0.1f)
                    HandleJudgement(0, numOffset); // パーフェクト判定
                else if (timeLag <= 0.15f)
                    HandleJudgement(1, numOffset); // グッド判定
                else if (timeLag <= 0.2f)
                    HandleJudgement(2, numOffset); // ノーマル判定
            }
        }
    }

    private int FindLaneOffset(int laneIndex)
    {
        // レーンに対応するオフセットを検索する
        int offset = -1;

        for (int i = 0; i < notesManager.LaneNum.Count; i++)
        {
            if (notesManager.LaneNum[i] == laneIndex)
            {
                offset = i;
                break;
            }
        }

        return offset;
    }

    private float GetTimeLag(int numOffset)
    {
        // 現在の時間とノーツの時間の差を取得する
        return Mathf.Abs(Time.time - (notesManager.NotesTime[numOffset] + GManager.instance.StartTime));
    }

    private void HandleJudgement(int judgeIndex, int numOffset)
    {
        // 判定を表示し、スコアやコンボを更新する
        Message(judgeIndex, numOffset);
        IncrementScore(judgeIndex);
        IncrementCombo();
        DeleteData(numOffset);
        UpdateUI();
    }

    private void HandleMiss()
    {
        // ミス判定を表示し、スコアやコンボをリセットする
        Message(3, 0);
        GManager.instance.miss++;
        GManager.instance.combo = 0;
        DeleteData(0);
        UpdateUI();
    }

    private void IncrementScore(int judgeIndex)
    {
        // スコアを増加させる
        switch (judgeIndex)
        {
            case 0:
                GManager.instance.ratioScore += 5;
                break;
            case 1:
                GManager.instance.ratioScore += 3;
                break;
            case 2:
                GManager.instance.ratioScore += 1;
                break;
        }

        // スコアを計算する
        GManager.instance.score = (int)Mathf.Round(1000000f * Mathf.Floor(GManager.instance.ratioScore / GManager.instance.maxScore * 1000000f) / 1000000f);
    }

    private void IncrementCombo()
    {
        // コンボ数を増加させる
        GManager.instance.perfect++;
        GManager.instance.combo++;
    }

    private void DeleteData(int numOffset)
    {
        // 処理済みのノーツデータを削除する
        notesManager.NotesTime.RemoveAt(numOffset);
        notesManager.LaneNum.RemoveAt(numOffset);
        notesManager.NoteType.RemoveAt(numOffset);
    }

    private void UpdateUI()
    {
        // UI を更新する
        comboText.text = GManager.instance.combo.ToString();
        scoreText.text = GManager.instance.score.ToString();
    }

    private void Message(int judge, int numOffset)
    {
        // 判定メッセージを表示する
        if (numOffset >= 0 && numOffset < notesManager.LaneNum.Count)
        {
            Instantiate(MessageObj[judge], new Vector3(notesManager.LaneNum[numOffset] - 1.3f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
        }
    }
}
