using UnityEngine;

public class FPS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
