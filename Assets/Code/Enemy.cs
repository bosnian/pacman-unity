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
	float delay = 1.0f;
	float stepTime = 0.5f;
	public BaseObject direction = new BaseObject {
		x = 1,
		y = 0
	};

	void Start () {

		tar = this.transform.position;
		InvokeRepeating("moveEnemy", delay, stepTime);
	}
		

	void moveEnemy(){

		EnemyAI e = new EnemyAI ();
		direction = e.getDirection (pos, direction,lastH,lastV);
		moveBy (direction);
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

	void moveBy(BaseObject m){

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

	bool areTwoPointsInRange(Vector3 first, Vector3 second){

		if (Mathf.Abs (first.x - second.x) < 0.5 && Mathf.Abs (first.y - second.y)<0.5)
			return true;

		return false;
	}
}
	
