using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

	public GameObject StartPlat;
	public GameObject EndPlat;

	public List<GameObject> Size1;

	public List<GameObject> Size2;

	public List<GameObject> Size3;

	public List<GameObject> Size4;

	public int Cols=12;
	public int Rows=6;

	private int[,] Matrix;


	enum PointerDir 
	{
		Up,
		Right,
		Down,
		Stop
	}


	// Use this for initialization
	void Start () {

		Matrix = new int[Rows, Cols];
		GenerateMatrix ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void GenerateMatrix(){
		int startj=0;
		int starti = Random.Range (0, Rows - 1);
		Vector2 pointer= new Vector2(starti,startj);



		//get dir to go if Stop end.

		//checboundaries
		//get a random x1 x2 x3 or x4 


		//when matrix generated it should be instantiating platforms 
	}

	private void /*PointerDir*/ DecideDir(Vector2 Pointer){
	
		//if( Pointer.y +1 == Cols
	

	}
}
