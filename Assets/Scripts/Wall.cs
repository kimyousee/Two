using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    public SpawnController spawnCtr;
    public GameController gameCtr;

    // Use this for initialization
    void Start()
    {
        spawnCtr = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<SpawnController>();
        gameCtr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("In wall coll");
        Debug.Log("Collided with " + coll.transform.name);
        
    }
}
