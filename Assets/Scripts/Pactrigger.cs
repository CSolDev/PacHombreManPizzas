using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pactrigger : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
        if(otherCollider.tag == "Player")
        {
            UIManager.sharedInstance.ScorePoints(50);
            Destroy(this.gameObject);
        }
	}
}
