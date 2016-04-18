using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public struct BaseObject {
	public int x;
	public int y;

	public BaseObject(BaseObject t){
		x = t.x;
		y = t.y;
	}

	public bool isCloseTo(BaseObject t){
		Debug.Log (x + " - " + t.x);
		if (Mathf.Abs (x - t.x) < 1 && Mathf.Abs (y - t.y) < 1)
			return true;
		Debug.Log ("FALSE");
		return false;
	}

	public static BaseObject operator +(BaseObject a, BaseObject b){
		var t = new BaseObject (a);
		t.x = t.x +  b.x;
		t.y = t.y + b.y;
		return t;
	}
}
	
public class GameStats : MonoBehaviour {

	public static string[][] map;
	public static BaseObject player;
	public static Vector3 playerPos;
	public static BaseObject defaultPlayerPosition;
	public static BaseObject portalRight;
	public static BaseObject portalLeft;
	public static List<BaseObject> walls = new List<BaseObject>();
	public static List<BaseObject> points = new List<BaseObject>();
	public static List<BaseObject> enemies = new List<BaseObject>();
	public static List<List<GameObject>> mapOfObjects;
	public static GUIText scoreText;
	public static GUIText livesText;
	public static GameObject playerRef;

	public static bool isFinished = false;

	public static int score = 0;
	public static int totalPoints = 0;
	public static int lives = 3;

	public const string pointLegend = "0";
	public const string wallLegend = "1";
	public const string playerLegend = "P";
	public const string enemyLegend = "E";

	void Awake(){
		DontDestroyOnLoad (this);
	}

	public static void loadData(){

		isFinished = false;
		score = 0;
		totalPoints = 0;
		lives = 3;
		walls = new List<BaseObject> ();
		points = new List<BaseObject> ();
		enemies = new List<BaseObject> ();

		map = readMapFromFile ("Assets/test.txt");

		for (int y = 0; y < GameStats.map.Length; y++) {
			for (int x = 0; x < GameStats.map[1].Length; x++) {
				
				if (GameStats.map [y] [x] == GameStats.wallLegend) {
					var tmp = new BaseObject ();
					tmp.x = x;
					tmp.y = y;
					walls.Add (tmp);
				} else if (GameStats.map [y] [x] == GameStats.playerLegend) {
					defaultPlayerPosition.x = x;
					defaultPlayerPosition.y = y;
					player.x = x;
					player.y = y;
				} else if (GameStats.map [y] [x] == GameStats.pointLegend) {
					var tmp = new BaseObject ();
					tmp.x = x;
					tmp.y = y;
					points.Add (tmp);
				} else if (GameStats.map [y] [x] == "R") {
					portalRight.y = y;
					portalRight.x = x + 1;
				} else if (GameStats.map [y] [x] == "L") {
					portalLeft.y = y;
					portalLeft.x = x - 1;
				} else if (GameStats.map [y] [x] == GameStats.enemyLegend) {
					var tmp = new BaseObject ();
					tmp.x = x;
					tmp.y = y;
					enemies.Add (tmp);
				}
			}
		} 

		totalPoints = points.Count;
	}

	static string[][] readMapFromFile(string fileName){
		
		string text = @"1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1
1 E 0 0 0 0 0 0 1 0 0 0 0 0 0 E 1
1 0 1 1 0 1 1 0 1 0 1 1 0 1 1 0 1
1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1
1 0 1 1 0 1 0 1 1 1 0 1 0 1 1 0 1
1 0 0 0 0 1 0 0 1 0 0 1 0 0 0 0 1
1 1 1 1 0 1 1 0 1 0 1 1 0 1 1 1 1
0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
1 1 1 1 0 1 1 1 0 1 1 1 0 1 1 1 1
1 0 0 0 0 1 E 0 0 0 E 1 0 0 0 0 1
1 0 1 1 0 1 1 1 0 1 1 1 0 1 1 0 1
1 0 0 0 0 0 0 0 0 0 0 0 0 0 1 0 1
1 0 1 1 0 1 1 0 1 1 0 1 1 0 1 0 1
1 P 0 0 0 0 0 0 0 0 0 0 0 0 0 E 1
1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1";
//		File.ReadAllText(fileName);

		string[] lines = Regex.Split(text, "\n");
		int rows = lines.Length;

		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++)  {
			string[] stringsOfLine = Regex.Split(lines[i], " ");
			levelBase[i] = stringsOfLine;
		}
		return levelBase;
	}

	public static bool checkIfMoveAvailableForPlayer(BaseObject t){

		for (int i = 0; i < walls.Count; i++) {
			if ( t.x == walls[i].x && t.y == walls[i].y) {
				return false;
			}
		}
		return true;
	}

	public static bool checkIfMoveAvailable(BaseObject t){
		if( t.x == GameStats.map[1].Length || t.y == GameStats.map.Length || t.x  == -1 || t.y == -1)
			return false;
		
		for (int i = 0; i < walls.Count; i++) {
			if ( t.x == walls[i].x && t.y == walls[i].y) {
				return false;
			}
		}
		return true;
	}

	public static bool checkIfMoveAvailable(int x, int y){

		if( x == GameStats.map[1].Length || y == GameStats.map.Length || x  == -1 || y == -1)
			return false;

		for (int i = 0; i < walls.Count; i++) {
			if ( x == walls[i].x && y == walls[i].y) {
				return false;
			}
		}
		return true;
	}

	public static void checkIfPointsAvailable(BaseObject t){
		for (int i = 0; i < points.Count; i++) {
			if ( t.x == points[i].x && t.y == points[i].y) {
				Destroy(mapOfObjects[t.y][t.x]);
				score++;

				scoreText.text = score.ToString ("D3");
			}
		}

		if (score == totalPoints) {
			isFinished = true;
			scoreText.text = "Winn!";
			Time.timeScale = 0;
		}
	}

	public static void removeLife(){
		if(lives > 0)
			lives--;
		livesText.text = "Lives: " + lives.ToString ();
		var p = GameStats.playerRef.GetComponent<Player> ();

		player = defaultPlayerPosition;
		p.transform.position = new Vector3 (-(GameStats.map [0].Length / 2) + defaultPlayerPosition.x, (GameStats.map.Length / 2) - defaultPlayerPosition.y, 0);
		p.target = p.transform.position;

		if (lives == 0) {
			isFinished = true;
			scoreText.text = "Game Over!";
			Time.timeScale = 0;
		}
	}

	public static void doMove(BaseObject t){
		checkIfMoveAvailable (t);
	}
}
