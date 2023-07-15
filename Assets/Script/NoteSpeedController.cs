using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteSpeedController : MonoBehaviour
{
    public Button upButton;
    public Button downButton;

    private GManager gameManager;

    private void Start()
    {
        gameManager = GManager.instance;  // Get the current instance of GManager
        upButton.onClick.AddListener(IncreaseNoteSpeed);
        downButton.onClick.AddListener(DecreaseNoteSpeed);
    }

    private void IncreaseNoteSpeed()
    {
        if (gameManager != null && gameManager.noteSpeed < 22.0f)  // Max limit set here
        {
            gameManager.noteSpeed += 0.5f;
        }
    }

    private void DecreaseNoteSpeed()
    {
        if (gameManager != null && gameManager.noteSpeed > 1.0f)  // Min limit set here
        {
            gameManager.noteSpeed -= 0.5f;
        }
    }
}
