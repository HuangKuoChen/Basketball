using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class background : MonoBehaviour {

    public GameObject red, blue;
    public GameObject ballObj;
    public GameObject overText;
    public GameObject startBtn, exitBtn;
    public Text redScoreText, blueScoreText;
    public int redScore, blueScore;
    public bool play;
    public static background Instance;

    public void AddScore(string which)
    {
        switch (which)
        {
            case "red":
                redScore++;
                redScoreText.text = "Score: " + redScore;
                Debug.Log("Red scored" + redScore);
                if (redScore == 10)
                    gameOver("Red");
                break;
            case "blue":
                blueScore++;
                blueScoreText.text = "Score: " + blueScore;
                Debug.Log("Blue scored" + blueScore);
                if (blueScore == 10)
                    gameOver("Blue");
                break;
        }
    }

    public void gameStart()
    {
        play = true;
        startBtn.SetActive(false);
        exitBtn.SetActive(false);
        overText.SetActive(false);
        Reset();
        Instantiate(ballObj, new Vector3(0, 0, 0), gameObject.transform.rotation);
    }

    public void gameOver(string which)
    {
        play = false;
        overText.SetActive(true);
        overText.GetComponent<Text>().text = which + "  Wins";
        startBtn.SetActive(true);
        exitBtn.SetActive(true);
        ball.Instance.gameOver();
    }

    public void Exit()
    {
        Application.Quit();
    }

    void Reset()
    {
        overText.SetActive(false);
        redScore = 0;
        redScoreText.text = "Score : " + redScore;
        blueScore = 0;
        blueScoreText.text = "Score : " + blueScore;
    }

    // Use this for initialization
    void Start () {
        startBtn.SetActive(true);
        exitBtn.SetActive(true);
        Instance = this;
        Input.multiTouchEnabled = true;
        play = false;
        Reset();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //if (Input.GetKey("a"))
        //    ball.Instance.gameOver();

        // If screen was touched.
        if (Input.touchCount > 0 && play)
        {
            // Detect which object was touched.
            foreach (Touch touch in Input.touches)
            {
                Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
                RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
                if (hit.transform.gameObject.tag == "redUp" && red.transform.position.y < 3)
                {
                    red.transform.position += new Vector3(0, 0.1f, 0);
                    Debug.Log("redUp clicked");
                }
                else if (hit.transform.gameObject.tag == "redDown" && red.transform.position.y > -3)
                {
                    red.transform.position -= new Vector3(0, 0.1f, 0);
                    Debug.Log("redDown clicked");
                }
                else if (hit.transform.gameObject.tag == "blueUp" && blue.transform.position.y < 3)
                {
                    blue.transform.position += new Vector3(0, 0.1f, 0);
                    Debug.Log("blueUp clicked");
                }
                else if (hit.transform.gameObject.tag == "blueDown" && blue.transform.position.y > -3)
                {
                    blue.transform.position -= new Vector3(0, 0.1f, 0);
                    Debug.Log("blueDown clicked");
                }
            }
            
        }
    }
}
