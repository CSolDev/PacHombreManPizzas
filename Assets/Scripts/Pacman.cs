using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour {

    public float speed = 0.4f;
    public Vector2 destination = Vector2.zero;

	void Start () 
    {
        destination = this.transform.position;
	}
	
	void FixedUpdate () 
    {

        if (GameManager.sharedInstance.gameStarted && !GameManager.sharedInstance.gamePaused)
        {
            GetComponent<AudioSource>().volume = 0.5f;
            Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);

            if (distanceToDestination < 2.0f)
            {

                if (Input.GetKey(KeyCode.UpArrow) && CanMoveTo(Vector2.up))
                {
                    destination = (Vector2)this.transform.position + Vector2.up;
                }

                if (Input.GetKey(KeyCode.RightArrow) && CanMoveTo(Vector2.right))
                {
                    destination = (Vector2)this.transform.position + Vector2.right;
                }

                if (Input.GetKey(KeyCode.DownArrow) && CanMoveTo(Vector2.down))
                {
                    destination = (Vector2)this.transform.position + Vector2.down;
                }

                if (Input.GetKey(KeyCode.LeftArrow) && CanMoveTo(Vector2.left))
                {
                    destination = (Vector2)this.transform.position + Vector2.left;
                }
            }


            Vector2 dir = destination - (Vector2)this.transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        } else{
            GetComponent<AudioSource>().volume = 0.0f;
        }
	}

    //True si podemos avanzar - false si no puede avanzar
    bool CanMoveTo(Vector2 dir)
    {
        Vector2 pacmanPos = this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pacmanPos+dir, pacmanPos);
        Debug.DrawLine(pacmanPos, pacmanPos+dir);
        Collider2D pacmanCollider = GetComponent<Collider2D>();
        Collider2D hitCollider = hit.collider;

        if(hitCollider == pacmanCollider){
            return true;
        }else{
            return false;
        }
    }
}
