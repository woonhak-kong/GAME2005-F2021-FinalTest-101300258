using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{

    public Button StartButton;
    public Button QuitButton;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }


    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

}
