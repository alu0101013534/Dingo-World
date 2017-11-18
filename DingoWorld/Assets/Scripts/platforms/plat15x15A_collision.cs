using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat15x15A_collision : MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag + " ENTER");
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.tag + " EXIT");
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
