using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizator : MonoBehaviour {
	void Start () {
		foreach (Transform item in transform)
        {
            if (item.name.Contains("Random"))
            {
                item.gameObject.SetActive(Random.Range(0, 2) == 1);
            }
        }
	}
}
