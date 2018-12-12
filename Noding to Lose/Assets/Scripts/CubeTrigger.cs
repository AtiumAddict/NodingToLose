using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{

    private GameObject cubeManager; 
    //private Rigidbody rb;
    private GameObject gameMaster;
    private Generator generator;
    private GameMaster gm;
    private GameObject score;
    private Transform indManager;

    AudioManager audioManager;

    void Awake ()
    {
        cubeManager = GameObject.Find("CubeManager");
        gameMaster = GameObject.Find("GameMaster");
        indManager = GameObject.Find("IndManager").transform;

        gm = gameMaster.GetComponent<GameMaster>();
        generator = indManager.GetComponent<Generator>();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent == indManager)
        {
            generator.Generate(7);
            generator.indCubes.Remove(other.gameObject);
        }

        //rb = cubeManager.GetComponent<Rigidbody>();

        //rb.velocity = Vector3.zero;                                                                                

        cubeManager.transform.position = GetNearestPointOnGridGen(cubeManager.transform.position);  // Snap the Cube Manager to the grid.

        if (other.CompareTag("Cube") && other.transform.parent == indManager)
        {
            // If the 2 cubes have the same color, destroy them.
            if (other.gameObject.GetComponent<Renderer>().material.name != GetComponent<Renderer>().material.name)  
            {
                other.gameObject.transform.parent = null;
                // Make the independent cube a child of the Cube Manager
                other.gameObject.transform.parent = cubeManager.transform;
                // Snap the cube to the grid
                other.gameObject.transform.position = GetNearestPointOnGridGen(other.transform.position);   
                audioManager.PlaySound("connection");
                if (other.transform.position == transform.position)
                {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                score = GameObject.Find("Score.Text");
                audioManager.PlaySound("colorMatch");
                Destroy(other.gameObject);
                Destroy(gameObject);
                gm.score++;
                score.GetComponent<Animator>().Play("ScoreChangingAnim");
            }
        }

        if (other.CompareTag("Border") && this.transform.parent != null)
        {
            Debug.Log("At Border");
            audioManager.PlaySound("explosion");
            gm.EndGame();
        }
    }

    // Calculate the nearest round Vector3 in the grid.
    public Vector3 GetNearestPointOnGridGen(Vector3 position)                                                      
    {
        int xRound = Mathf.RoundToInt(position.x);
        int yRound = Mathf.RoundToInt(position.y);
        int zRound = Mathf.RoundToInt(position.z);

        Vector3 result = new Vector3(xRound, yRound, zRound);

        return result;
    }                                                    

    
}
