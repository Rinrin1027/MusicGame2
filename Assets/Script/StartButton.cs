using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Button componentの取得
        Button button = GetComponent<Button>();

        // ButtonのClickイベントにリスナーを追加
        button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        // "GamePart1"という名前のシーンに切り替える
        SceneManager.LoadScene("GamePart1");
    }
}
