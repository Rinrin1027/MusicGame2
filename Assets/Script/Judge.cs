using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Judge : MonoBehaviour
{
    [SerializeField] private GameObject[] MessageObj;
    [SerializeField] private NotesManager notesManager;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
    [SerializeField] private GameObject light4;

    private Dictionary<int, KeyCode> touchLaneMapping; // タッチIDとレーンのマッピング

    private void Start()
    {
        touchLaneMapping = new Dictionary<int, KeyCode>();
    }

    private void FixedUpdate()
    {
        // ゲームが開始されていない場合は何もしない
        if (!GManager.instance.Start)
            return;

        // キーボードの入力を処理する
        ProcessKeyboardInput();

        // タッチの入力を処理する
        ProcessTouchInput();

        // ノーツのタイミングをチェックし、ミス判定を行う
        if (notesManager.NotesTime.Count > 0 && Time.time > notesManager.NotesTime[0] + 0.2f + GManager.instance.StartTime)
            HandleMiss();
    }

    private void ProcessKeyboardInput()
    {
        // 各レーンのキーボード入力を処理する
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKey(KeyCode.D) && IsTouchingObject(light1)))
        {
            ProcessInput(KeyCode.D, 0);
        }
        else if (Input.GetKeyDown(KeyCode.F) || (Input.GetKey(KeyCode.F) && IsTouchingObject(light2)))
        {
            ProcessInput(KeyCode.F, 1);
        }
        else if (Input.GetKeyDown(KeyCode.J) || (Input.GetKey(KeyCode.J) && IsTouchingObject(light3)))
        {
            ProcessInput(KeyCode.J, 2);
        }
        else if (Input.GetKeyDown(KeyCode.K) || (Input.GetKey(KeyCode.K) && IsTouchingObject(light4)))
        {
            ProcessInput(KeyCode.K, 3);
        }
    }

    private bool IsTouchingObject(GameObject targetObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject == targetObject && hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                closestObject = hit.collider.gameObject;
            }
        }

        return closestObject == targetObject;
    }

    private void ProcessTouchInput()
    {
        // レイキャストを使用してタッチ入力を処理する
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                if (IsTouchingObject(light1))
                {
                    ProcessInput(KeyCode.D, 0, touch.fingerId);
                }
                else if (IsTouchingObject(light2))
                {
                    ProcessInput(KeyCode.F, 1, touch.fingerId);
                }
                else if (IsTouchingObject(light3))
                {
                    ProcessInput(KeyCode.J, 2, touch.fingerId);
                }
                else if (IsTouchingObject(light4))
                {
                    ProcessInput(KeyCode.K, 3, touch.fingerId);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (touchLaneMapping.ContainsKey(touch.fingerId))
                {
                    KeyCode laneKeyCode = touchLaneMapping[touch.fingerId];
                    touchLaneMapping.Remove(touch.fingerId);

                    ProcessInput(laneKeyCode, -1, touch.fingerId);
                }
            }
        }
    }

    public void ProcessInput(KeyCode keyCode, int laneIndex, int touchId = -1)
    {
        // レーンに対応するオフセットを全て取得する
        List<int> offsets = FindLaneOffsets(laneIndex);

        // オフセットのリストが空であれば処理を中断する
        if (offsets.Count == 0)
            return;

        for (int i = offsets.Count - 1; i >= 0; i--)
        {
            int numOffset = offsets[i];
            float timeLag = GetTimeLag(numOffset);

            // ノーツに対する判定を行う
            if (timeLag <= 0.2f)
                HandleJudgement(0, numOffset); // パーフェクト判定
            else if (timeLag <= 0.3f)
                HandleJudgement(1, numOffset); // グッド判定
            else if (timeLag <= 0.4f)
                HandleJudgement(2, numOffset); // ノーマル判定
        }

        // ノーツに対応する音を再生する
        audioSource.PlayOneShot(hitSound);
    }



    private List<int> FindLaneOffsets(int laneIndex)
    {
        // レーンに対応するオフセットのリストを作成する
        List<int> offsets = new List<int>();

        for (int i = 0; i < notesManager.LaneNum.Count; i++)
        {
            if (notesManager.LaneNum[i] == laneIndex)
            {
                offsets.Add(i);
            }
        }

        return offsets;
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