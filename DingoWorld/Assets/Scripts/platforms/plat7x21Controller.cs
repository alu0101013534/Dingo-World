using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat7x21Controller : MonoBehaviour {

    public float maxDisplacement = 10;
    public float maxSpeed = 3.5f;
    private List<Slider> sliders;

    public class Slider
    {
        private float maxDisplacement;
        private Transform body;
        private int direction;
        private float speed;

        public Slider(Transform _body, float maxSpeed, float _maxDisplacement)
        {
            maxDisplacement = _maxDisplacement;
            body = _body;
            speed = Random.Range(1f, maxSpeed);
            direction = (Random.Range(0f, 1f) > 0.5f) ? +1 : -1;
        }

        public void Update(float deltaTime)
        {
            body.Translate(new Vector3(direction * deltaTime * speed, 0f, 0f));
            if (body.localPosition.x > maxDisplacement)
            {
                float correction = -2 * (body.localPosition.x - maxDisplacement);
                body.Translate(new Vector3(correction, 0f, 0f));
                direction *= -1;
            }
            else if (body.localPosition.x < -maxDisplacement)
            {
                float correction = -2 * (body.localPosition.x + maxDisplacement);
                body.Translate(new Vector3(correction, 0f, 0f));
                direction *= -1;
            }
        }
    }

	void Start () {
        sliders = new List<Slider>();

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Cube"))
            {
                sliders.Add(new Slider(child, maxSpeed, maxDisplacement));
            }
        }
	}
	
	void Update () {
		foreach (Slider slider in sliders)
        {
            slider.Update(Time.deltaTime);
        }
	}
}
