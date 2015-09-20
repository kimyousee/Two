using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

    public GameObject[] blocks;
    public Block current;
    public bool blockWasPlaced;
    public bool stopSpawn;

    public Transform greenBlock;
    public Transform redBlock;
    public Transform blueBlock;
    public Transform yellowBlock;

    private GameController gameCtr;
    private Border border;

    private Block spawnedBlock;
    private bool spawnBegan;

    Vector2 spawnPosition = new Vector2(1.0f, 3.0f);
    Vector2 spawnTop = new Vector2(1.0f, 4.0f);
    Vector2 spawnRight = new Vector2(6.0f, -1.0f);
    Vector2 spawnLeft = new Vector2(-5.0f, -1.0f);
    Vector2 spawnBott = new Vector2(1.0f, -5.0f);

    void Awake () {
        blockWasPlaced = false;
        spawnBegan = false;
        stopSpawn = true;
        gameCtr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        border = GameObject.FindGameObjectWithTag("Border").GetComponent<Border>();
        foreach( GameObject obj in GameObject.FindGameObjectsWithTag("Block"))
        {
            Block block = obj.GetComponent<Block>();
            if (obj.activeInHierarchy && !block.placed)
            {
                current = block;
                break;
            }
        }
        

	}
	
	void FixedUpdate () {
        if (!spawnBegan) return;
        if (stopSpawn) return;
        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Block"))
        //{
        //    Block block = obj.GetComponent<Block>();
        //    if (obj.activeInHierarchy && !block.placed)
        //    {
        //        current = block;
        //        break;
        //    }
        //}
        // When placed, spawn a new one
        if (blockWasPlaced)
        {
            blockWasPlaced = false;
            Debug.Log("placed");
            Vector2 spawnPlace = findSpawnPlace();

            string blockType = gameCtr.blocksToPlace[gameCtr.blockIndex];
            Debug.Log("Block type: " + blockType);
            
            Transform inst = null;
            switch (blockType)
            {
                case "blue": {  inst = Instantiate(blueBlock, spawnPlace, Quaternion.identity) as Transform; break; }
                case "red": {  inst = Instantiate(redBlock, spawnPlace, Quaternion.identity) as Transform; break; }
                case "yellow": {  inst = Instantiate(yellowBlock, spawnPlace, Quaternion.identity) as Transform; break; }
                case "green": {  inst = Instantiate(greenBlock, spawnPlace, Quaternion.identity) as Transform;  break; }
                default: break;
            }
            current = inst.gameObject.GetComponent<Block>();
            if (gameCtr.blockIndex < gameCtr.blocksToPlace.Length - 1) gameCtr.blockIndex++;
            else { stopSpawn = true; }
            blockWasPlaced = false;
            
        }
       
    }
    public void beginSpawn()
    {
        Vector2 spawnPlace = findSpawnPlace();
        string blockType = gameCtr.blocksToPlace[gameCtr.blockIndex];
        Transform inst = null;
        switch (blockType)
        {
            case "blue": { inst = Instantiate(blueBlock, spawnPlace, Quaternion.identity) as Transform; break; }
            case "red": { inst = Instantiate(redBlock, spawnPlace, Quaternion.identity) as Transform; break; }
            case "yellow": { inst = Instantiate(yellowBlock, spawnPlace, Quaternion.identity) as Transform; break; }
            case "green": { inst = Instantiate(greenBlock, spawnPlace, Quaternion.identity) as Transform; break; }
            default: break;
        }
        current = inst.gameObject.GetComponent<Block>();
        gameCtr.blockIndex++;
        spawnBegan = true;
        blockWasPlaced = false;
        stopSpawn = false;
    }

    Vector2 findSpawnPlace()
    {
        Vector2 spawnPlace = spawnTop;
        if (Input.GetButton("spawnLeft"))
        {
            spawnPlace = spawnLeft;
            gameCtr.gravity = "right";
            if (border.leftActive)
            {
                border.setAllActive();
                border.leftActive = false;
            }
        }
        else if (Input.GetButton("spawnRight"))
        {
            spawnPlace = spawnRight;
            if (border.rightActive)
            {
                gameCtr.gravity = "left";
                border.rightActive = false;
            }
        }
        else if (Input.GetButton("spawnUp"))
        {
            spawnPlace = spawnTop;
            if (border.topActive)
            {
                gameCtr.gravity = "down";
                border.topActive = false;
            }
        }
        else if (Input.GetButton("spawnDown"))
        {
            spawnPlace = spawnBott;
            if (border.bottActive)
            {
                gameCtr.gravity = "up";
                border.bottActive = false;
            }
        }
        return spawnPlace;
    }
}
