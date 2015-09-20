using UnityEngine;
using System.Collections;

public class ExitBorder : MonoBehaviour {

    private GameController gameCtr ;

    void Awake()
    {
        gameCtr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        gameCtr.blockMisplay = true;
    }
}
