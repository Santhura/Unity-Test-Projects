using UnityEngine;
using System.Collections;

public class ChopperAI : BaseAI {

    private float angle;
    private float lowestPoint;
    private float originalHighPoint;

	// Use this for initialization
	void Start () {
        speed = 1;
        maxSpeed = 30;
        originalHighPoint = transform.position.y;
        lowestPoint = transform.position.y - .5f;

	}
	
	// Update is called once per frame
	void Update () {
       Movement();
	}

    private void Movement()
    {
        if (Vector2.Distance(targetPos.position, transform.position) > 3)
        {
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            if(transform.position.y > lowestPoint)
                gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);

            if(Vector2.Distance(targetPos.position, transform.position) > 2)
            {

            }
        }
    }
}