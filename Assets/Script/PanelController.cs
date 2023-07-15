using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private Button manyuButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject manyuPanel;

    private void Start()
    {
        // 最初はパネルを非表示にする
        manyuPanel.SetActive(false);

        // ManyuButtonが押されたときの処理をセット
        manyuButton.onClick.AddListener(OpenPanel);

        // CloseButtonが押されたときの処理をセット
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void OpenPanel()
    {
        manyuPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        manyuPanel.SetActive(false);
    }
}
