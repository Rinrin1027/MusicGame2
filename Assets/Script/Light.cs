using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int num = 0;
    private Renderer rend;
    private float alpha = 0f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!(rend.material.color.a <= 0f))
        {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alpha);
        }

        if (num == 1 && (Input.GetKeyDown(KeyCode.D) || IsTouched()))
        {
            colorChange();
        }
        else if (num == 2 && (Input.GetKeyDown(KeyCode.F) || IsTouched()))
        {
            colorChange();
        }
        else if (num == 3 && (Input.GetKeyDown(KeyCode.J) || IsTouched()))
        {
            colorChange();
        }
        else if (num == 4 && (Input.GetKeyDown(KeyCode.K) || IsTouched()))
        {
            colorChange();
        }

        alpha -= speed * Time.deltaTime;
    }

    public void colorChange()
    {
        alpha = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alpha);
    }

    private bool IsTouched()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // タッチ座標をスクリーン座標に変換
                    Vector2 touchPosition = touch.position;

                    // Raycastを使用してタッチ位置をワールド座標に変換
                    Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject && num == hit.collider.GetComponent<Light>().num)
                        {
                            // タッチ位置がこのオブジェクトに当たり、numが一致する場合、このタッチは有効とみなす
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
