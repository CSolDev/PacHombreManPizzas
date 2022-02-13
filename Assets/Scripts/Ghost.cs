using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour 
{

    public Transform[] waypoints;
    int currentWaypoint = 0;
    public float speed = 0.3f;
    public bool shouldWaitHome = false;

	private void Update()
	{
        if(GameManager.sharedInstance.invincibleTime>0)
        {
            GetComponent<Animator>().SetBool("PacmanInvincible", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("PacmanInvincible", false);
        }
	}

	private void FixedUpdate()
	{

        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.05f;

            if (!shouldWaitHome)
            {
                float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position,
                                                            (Vector2)waypoints[currentWaypoint].position);

                if (distanceToWaypoint < 0.1f)
                {
                    currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                    Vector2 newDirection = waypoints[currentWaypoint].position - this.transform.position;
                    GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                    GetComponent<Animator>().SetFloat("DirY", newDirection.y);
                }
                else
                {
                    Vector2 newPos = Vector2.MoveTowards(this.transform.position,
                                                         waypoints[currentWaypoint].position,
                                                         speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(newPos);
                }
            }
        }else{
            GetComponent<AudioSource>().volume = 0.0f;
        }
	}


	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
        if(otherCollider.tag == "Player"){
            if (GameManager.sharedInstance.invincibleTime <= 0)
            {
                GameManager.sharedInstance.gameStarted = false;
                Destroy(otherCollider.gameObject);
                StartCoroutine("RestartGame");

            } else {
                UIManager.sharedInstance.ScorePoints(1000);
                GameObject home = GameObject.Find("Ghost Home");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                this.shouldWaitHome = true;
                StartCoroutine("AwakeFromHome");
            }
        }
	}

    IEnumerator RestartGame(){
        yield return new WaitForSecondsRealtime(2.0f);
        GameManager.sharedInstance.RestartGame();
    }

    //aumento dificultad cada vez que despierta ghost
    IEnumerator AwakeFromHome(){
        yield return new WaitForSecondsRealtime(3.0f);
        this.shouldWaitHome = false;
        this.speed *= 1.2f;
    }
}
