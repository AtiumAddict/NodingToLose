using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject gm;

    public float explosionRadius = 3f;

    AudioManager audioManager;

    public Transform explosionPrefab;

    void Awake()
    {
        gm = GameObject.Find("GameMaster");
        audioManager = AudioManager.instance;

    }

    void OnTriggerEnter(Collider other)                                                             // When the bomb collides with something, it explodes.
    {
        Explode(gameObject);                                        
    }

    void Explode(GameObject bomb)
    {
        Collider[] nearObjects = Physics.OverlapSphere(bomb.transform.position, explosionRadius);   // Create an array that includes all the colliders inside the explosion radius. 
        audioManager.PlaySound("explosion");
        Transform clone = Instantiate(explosionPrefab, transform) as Transform;
        //Destroy(clone.gameObject, 3f);

        foreach (Collider collider in nearObjects)                                                  // For each collider inside the radius, do the following.
        {
            if (collider.tag == "CentralCube")
            {
                gm.GetComponent<GameMaster>().EndGame();
            }

            else if (collider.tag == "Cube")
            {
                Destroy(collider.gameObject);                                                       // If it is a normal cube, destroy it.
            }
        }
        GetComponent<Renderer>().enabled = false;
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
        //Destroy(bomb.gameObject, 3.1f);                                                                   // Destroy the exploding bomb.
    }

}
