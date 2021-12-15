using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public Button activateButton;
    public Text infoLabel;

    public static bool IsStart { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (infoLabel.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("StartScene");
                IsStart = false;
            }
        }
    }

    public void OnClickActivate()
    {
        IsStart = true;
        activateButton.gameObject.SetActive(false);
        infoLabel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
