using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public string[] blocksToPlace;
    public int blockIndex;
    public string gravity;
    public float gravitySpeed;
    public bool blockMisplay;
    private SpawnController spawnCtr;
    private Border border;
    private bool result; // true = win, false = lose

	// Use this for initialization
	void Awake () {
        blockMisplay = false;
        gravity = "down";
        blockIndex = 0;
        blocksToPlace = new string[8] { "blue", "red", "yellow", "green", "blue", "blue", "red", "green" };
        gravitySpeed = 2f;
        spawnCtr = GameObject.Find("SpawnController").GetComponent<SpawnController>();
        border = GameObject.Find("Border").GetComponent<Border>();
        spawnCtr.beginSpawn();
	}
	
	// Update is called once per frame
	void Update () {
        // End of game
	    if ((spawnCtr.blockWasPlaced && spawnCtr.stopSpawn) || blockMisplay)
        {
            border.setAllActive();
            if (blockMisplay) {
                result = false;
                // display blockIndex = how far player got
            }
        }
	}


}
