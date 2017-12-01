using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat5x8Controller : MonoBehaviour {

    private Transform ballCannons;
    private List<Ball> balls;
    public float maxLifeTime = 15f;
    public float period = 3f;
    private float elapsedTime = 0;
    
    public class Ball
    {
        public float lifeTime = 0;
        public GameObject obj;
        public Ball(GameObject _obj)
        {
            obj = _obj;
        }
    }

	void Start () {
        balls = new List<Ball>();

        foreach (Transform item in transform)
        {
            if (item.name.Contains("Balls"))
            {
                ballCannons = item;
            }
        }
	}
	
	void Update () {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > period)
        {
            elapsedTime = 0;
            foreach (Transform ballCannon in ballCannons)
            {
                var ball = Object.Instantiate(ballCannon.gameObject);
                ball.transform.parent = transform;
                ball.transform.position = ballCannon.position;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<Collider>().enabled = true;
                balls.Add(new Ball(ball));
            }
        }

        for (int i = balls.Count - 1; i >= 0; i--)
        {
            balls[i].lifeTime += Time.deltaTime;
            if (balls[i].lifeTime > maxLifeTime)
            {
                Destroy(balls[i].obj);
                balls.RemoveAt(i);
            }
        }
	}
}
