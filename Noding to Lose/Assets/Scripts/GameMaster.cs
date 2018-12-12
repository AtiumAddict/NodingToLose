using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
    public GameObject center;
    public GameObject CubeManager;
    private Rigidbody rb;
    public int score = 0;
    public Text scoreText;
    public Text highScore;
    private bool paused;
    public Camera cam;

    public GameObject explosionPrefab;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject controlsPanel;

    AudioManager audioManager;

    private void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    void Start()
    {
        highScore.text = "HIGH SCORE:\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();

        audioManager = AudioManager.instance;
        rb = CubeManager.GetComponent<Rigidbody>();                                           // Set rb as the RigidBody of the CubeManager.
        Cursor.visible = false;
    }

    void Update()
    {
        scoreText.text = score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (controlsPanel.activeSelf == false && gameOverUI.activeSelf == false)
            {
                if (paused == true)
                {
                    Cursor.visible = false;
                    Time.timeScale = 1.0f;
                    cam.GetComponent<CameraMovement>().enabled = true;
                    pauseMenu.SetActive(false);
                    paused = false;
                }
                else
                {
                    Cursor.visible = true;
                    Time.timeScale = 0.0f;
                    cam.GetComponent<CameraMovement>().enabled = false;
                    pauseMenu.SetActive(true);
                    paused = true;
                }
            }
            
            if (paused == true || gameOverUI.activeSelf == true)
            {
                if (controlsPanel.activeSelf == true)
                {
                    controlsPanel.SetActive(false);
                }
            }
        }
        if (Input.GetKeyDown("t"))
        {
            StartCoroutine("PlayExplosionSound");
        }
    }

    public void EndGame()
    {
        CubeManager.GetComponent<CubeMovement>().enabled = false;
        Destroy(center.gameObject);
        rb.velocity = Vector3.zero;
        Invoke("End", 1.5f);
    }

    private void End()
    {
        Cursor.visible = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.text = "HIGH SCORE:\n" + score.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "HIGH SCORE:\n0";
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        cam.GetComponent<CameraMovement>().enabled = true;
        pauseMenu.SetActive(false);
        paused = false;
        Cursor.visible = false;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("APPLICATION QUIT!");
        Application.Quit();
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
    }

    public void Back()
    {
        controlsPanel.SetActive(false);
    }
}
