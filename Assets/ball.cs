using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {


    public float ballXSpeed, ballYSpeed;
    public float xface, yface;
    private float time;
    public static ball Instance;

    public void gameOver()
    {
        Destroy(gameObject);
    } 

    void Reset(float x)
    {
        xface = x;
        yface = 0.1f;
        ballXSpeed = 1;
        ballYSpeed = 1;
        time = 0;
        gameObject.transform.position = new Vector3(0, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player")
        {
            xface *= -1;
            Vector2 dir = new Vector2(gameObject.transform.position.x - col.transform.position.x, gameObject.transform.position.y - col.transform.position.y);
            dir.Normalize();
            ballYSpeed = dir.y * -2;
            //Debug.Log("collisin player");
        } else if (col.tag == "upside" || col.tag == "downside")
        {
            yface *= -1;
            //Debug.Log("collisin side");
        } else if(col.tag == "redside")         // If ball hits redside, reset and ball move to blueside
        {
            background.Instance.AddScore("blue");
            Reset(0.1f);
        } else if(col.tag == "blueside")        // If ball hits blueside, reset and ball move to redside
        {
            background.Instance.AddScore("red");
            Reset(-0.1f);
        }

    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
        Reset(0.1f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        time += Time.deltaTime;
        gameObject.transform.position += new Vector3(ballXSpeed * xface * (1 + time / 10), ballYSpeed * yface * ( 1 + time / 10), 0);
        gameObject.transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
}
