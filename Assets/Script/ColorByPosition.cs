using UnityEngine;

public class ColorByPosition : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float xPosition = transform.position.x;

        if (Mathf.Approximately(xPosition, 1.5f) || Mathf.Approximately(xPosition, -1.5f))
        {
            // x座標が1.5または-1.5の場合、薄いピンクに設定
            rend.material.color = new Color(1f, 0.75f, 0.8f, 1f);
        }
        else if (Mathf.Approximately(xPosition, -0.5f) || Mathf.Approximately(xPosition, 0.5f))
        {
            // x座標が-0.5または0.5の場合、薄い青に設定
            rend.material.color = new Color(0.75f, 0.8f, 1f, 1f);
        }
    }
}
