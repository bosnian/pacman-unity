using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Enemy : MonoBehaviour {

	public BaseObject pos;
	public BaseObject prevPos;
	public Vector3 tar;
	public BaseObject lastH = new BaseObject {
		x = 1,
		y = 0
	};
	public BaseObject lastV = new BaseObject {
		x = 0,
		y = 1
	};
	float delay = 3.0f;
	float stepTime = 0.5f;
	public BaseObject direction = new BaseObject {
		x = 1,
		y = 0
	};
	// Use this for initialization
	void Start () {

		tar = this.transform.position;
		InvokeRepeating("moveEnemy", delay, stepTime);
	}
		

	void moveEnemy(){

		EnemyAI e = new EnemyAI ();
		direction = e.getDirection (pos, direction,lastH,lastV);
		moveBy (direction);
	}
		


	void move () {
//		direction = direction % 4;
//		switch (direction) {
//		case 0:
//			moveBy (1,0);
//			break;
//		case 1:
//			moveBy (0,1);
//			break;
//		case 2:
//			moveBy (-1,0);
//			break;
//		case 3:
//			moveBy (0,-1);
//			break;
//		}
	}

	List<int> getAvailableDirections(){
		List<int> dirs = new List<int> ();
		for (int i = 0; i < 4; i++) {
//			if (i == direction)
//				continue;

			if(checkDirection(i)){
				dirs.Add (i);
			}
		}
		return dirs;
	}

	bool checkDirection(int it){
		BaseObject tmpP;
		tmpP.x = pos.x;
		tmpP.y = pos.y;

		switch (it) {
		case 0:
			tmpP.x++;
			break;
		case 1:
			tmpP.y++;
			break;
		case 2:
			tmpP.x--;
			break;
		case 3:
			tmpP.y--;
			break;
		}
			
		if (GameStats.checkIfMoveAvailable (tmpP)) {
			return true;
		}
		return false;
	}

	void moveBy(int x, int y){
//		BaseObject tmpP;
//		tmpP.x = pos.x+x;
//		tmpP.y = pos.y+y;

//		if (!GameStats.checkIfMoveAvailable (tmpP)) {
//			direction++;
//		
//			return;
//		}

		Vector3 position = this.transform.position;
		position.x += x;
		position.y += y;
		pos.x += x;
		pos.y += y;
		this.transform.position = position;
	}

	void moveBy(BaseObject m){

//		if (!GameStats.checkIfMoveAvailable (m)) {
//			Debug.Log ("GRESKAAAA");
//			return;
//		}
		Vector3 position = this.transform.position;
		position.x += m.x;
		position.y -= m.y;
		prevPos = pos;
		pos.x += m.x;
		pos.y += m.y;

		tar = position;
	}


	void Update(){
		Vector3 position = this.transform.position;

		float step = stepTime * Time.deltaTime * 4f;

		if (areTwoPointsInRange(position, GameStats.playerPos)) {
			GameStats.removeLife ();
		}


		this.transform.position = Vector3.MoveTowards (this.transform.position, tar, step);
	}

//	void FixedUpdate(){
//		Vector3 position = this.transform.position;
//		Vector3 tmp = tar - position;
//		var frames = Time.fixedDeltaTime / stepTime;
//
//		tmp *= frames;
//
//
//		this.transform.position = Vector3.MoveTowards (this.transform.position, position + tmp, frames);
//
//		var delta = tar - this.transform.position;
//		Debug.Log (delta.x + " - " + delta.y);
//
//	}


	bool areTwoPointsInRange(Vector3 first, Vector3 second){

		if (Mathf.Abs (first.x - second.x) < 0.5 && Mathf.Abs (first.y - second.y)<0.5)
			return true;

		return false;
	}
}
	