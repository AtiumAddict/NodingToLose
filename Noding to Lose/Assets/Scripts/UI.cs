using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    
    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    public void Play()
    {
        SceneManager.LoadScene("Level01");
    }

    public void Quit()
    {
        Debug.Log("APPLICATION QUIT!");
        Application.Quit();
    }


}
