using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    // ノーツのスピードを設定
    float noteSpeed = 16f;
    bool gameStarted = false;

    void Start()
    {
        noteSpeed = GManager.instance.noteSpeed;
        StartCoroutine(StartGameDelay());
    }

    IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(5f); // 5秒待機
        gameStarted = true;
    }

    void Update()
    {
        if (gameStarted)
        {
            // ゲームが開始されている場合に処理を実行
            transform.position -= transform.forward * Time.deltaTime * noteSpeed;
        }
    }
}
