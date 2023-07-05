using UnityEngine;
using UnityEngine.UI;

public class Light : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private int num = 0;
    private Renderer rend;
    private float alfa = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!(rend.material.color.a <= 0))
        {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.r, rend.material.color.r, alfa);
        }

        // キーボード入力の監視
        if (num == 1 && Input.GetKeyDown(KeyCode.D))
        {
            colorChange();
        }
        else if (num == 2 && Input.GetKeyDown(KeyCode.F))
        {
            colorChange();
        }
        else if (num == 3 && Input.GetKeyDown(KeyCode.J))
        {
            colorChange();
        }
        else if (num == 4 && Input.GetKeyDown(KeyCode.K))
        {
            colorChange();
        }

        alfa -= Speed * Time.deltaTime;
    }

    public void colorChange()
    {
        alfa = 0.3f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
    }
}
