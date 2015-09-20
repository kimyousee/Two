using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {

    public bool topActive;
    public bool bottActive;
    public bool rightActive;
    public bool leftActive;
    private GameController gameCtr;

    private GameObject top;
    private GameObject bott;
    private GameObject left;
    private GameObject right;

    // Use this for initialization
    void Start () {
        gameCtr = GameObject.Find("GameController").GetComponent<GameController>();
        topActive = false;
        bottActive = true;
        rightActive = true;
        leftActive = true;
        top = transform.Find("top").gameObject;
        bott = transform.Find("bott").gameObject;
        left = transform.Find("left").gameObject;
        right = transform.Find("right").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        top.SetActive(topActive);
        bott.SetActive(bottActive);
        left.SetActive(leftActive);
        right.SetActive(rightActive);
        //top.GetComponent<Collider>().enabled = (!bottActive && rightActive && leftActive);
        //bott.GetComponent<Collider>().enabled = (!topActive && rightActive && leftActive);
        //right.GetComponent<Collider>().enabled = (!leftActive && topActive && bottActive);
        //left.GetComponent<Collider>().enabled = (!rightActive && topActive && bottActive);
	}

    public void setAllActive ()
    {
        topActive = true;
        bottActive = true;
        rightActive = true;
        leftActive = true;
    }
    //void checkBorderIntersections()
    //{
    //    int layer = 10; // Blocks layer
    //    foreach()

    //}
}
