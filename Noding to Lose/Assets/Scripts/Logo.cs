﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{

	void Start ()
    {
        Invoke("NextScene", 4.1f);
    }
	
	void NextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
