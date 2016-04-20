using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	BaseObject right = new BaseObject {
		x = 1,
		y = 0
	};

	BaseObject up = new BaseObject {
		x = 0,
		y = -1
	};

	BaseObject left = new BaseObject {
		x = -1,
		y = 0
	};

	BaseObject down = new BaseObject {
		x = 0,
		y = 1
	};

	BaseObject direction = new BaseObject {
		x = 0,
		y = 0
	};

	public Vector3 target;

	float delay = 1.0f;
	float stepTime = 0.5f;

	void Start(){
		target = this.transform.position;
		InvokeRepeating("move", delay, stepTime);
	}

	void Update(){
		if (GameStats.map == null)
			return;

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			direction = left;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			direction = right;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			direction = up;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			direction = down;
		}


		if (Input.GetKeyDown (KeyCode.P) && !GameStats.isFinished) {
			if (Time.timeScale == 0)
				Time.timeScale = 1;
			else
				Time.timeScale = 0;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
			Time.timeScale = 1;
		}


		Vector3 position = this.transform.position;
		GameStats.playerPos = position;
		float step = stepTime * Time.deltaTime * 4f;

		this.transform.position = Vector3.MoveTowards (this.transform.position, target, step);
	}

	void move(){
		GameStats.checkIfPointsAvailable (GameStats.player);

		var tmpP = GameStats.player;

		if (direction.Equals (left)) {
			tmpP.x--;
			if (!GameStats.checkIfMoveAvailableForPlayer (tmpP))
				return;
			GameStats.player.x--;
			Vector3 position = this.transform.position;
			position.x--;

			target = position;
		}

		if (direction.Equals(right))
		{
			tmpP.x++;
			if (!GameStats.checkIfMoveAvailableForPlayer (tmpP))
				return;
			GameStats.player.x++;
			Vector3 position = this.transform.position;
			position.x++;

			target = position;
		}

		if (direction.Equals(up))
		{
			tmpP.y--;
			if (!GameStats.checkIfMoveAvailableForPlayer (tmpP))
				return;
			GameStats.player.y--;
			Vector3 position = this.transform.position;
			position.y++;
			target = position;
		}

		if (direction.Equals(down))
		{
			tmpP.y++;
			if (!GameStats.checkIfMoveAvailableForPlayer (tmpP))
				return;
			GameStats.player.y++;
			Vector3 position = this.transform.position;
			position.y--;

			target = position;
		}

		checkForPortals ();
	}

	void checkForPortals(){
		if (GameStats.player.x == GameStats.map[0].Length ) {
			Vector3 position = this.transform.position;
			position.x -= GameStats.player.x;
			GameStats.player.x = 0;
			this.transform.position = position;
		} else if (GameStats.player.x == -1 ) {
			Vector3 position = this.transform.position;
			position.x += GameStats.map[0].Length;
			GameStats.player.x = GameStats.map[0].Length-1;
			this.transform.position = position;
		} 
	}
}