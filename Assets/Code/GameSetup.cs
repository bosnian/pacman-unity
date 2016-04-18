using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {

	public Camera mainCamera;

	public Transform playerPrefab;
	public Transform wallPrefab;
	public Transform enemyPrefab;
	public Transform pointPrefab;
	public Transform scoreGUI;
	public Transform livesGUI;
	public Transform helpText;

	private int offSetX = 0;
	private int offSetY = 0;

	void Start() {

		GameStats.loadData ();
		GameStats.scoreText = scoreGUI.GetComponent<GUIText> ();
		GameStats.livesText = livesGUI.GetComponent<GUIText> ();

		offSetX = GameStats.map [0].Length / 2;
		offSetY = GameStats.map.Length / 2;
		// create planes based on matrix
		var w = GameStats.map.Length;
		var h = GameStats.map [1].Length;
		GameStats.mapOfObjects = new List<List<GameObject>> ();
		for (int y = 0; y < GameStats.map.Length; y++) {
			GameStats.mapOfObjects.Add (new List<GameObject> ());
			for (int x = 0; x < GameStats.map[1].Length; x++) {
				Transform t;
				switch (GameStats.map[y][x]){
				case GameStats.playerLegend:
					t = (Transform)Instantiate (playerPrefab, new Vector3 (-offSetX + x, offSetY - y, 0), Quaternion.identity);
					GameStats.mapOfObjects [y].Add (t.gameObject);
					GameStats.playerRef = t.gameObject;
					break;
				case GameStats.wallLegend:
					t = (Transform)Instantiate (wallPrefab, new Vector3 (-offSetX + x, offSetY - y, 0), Quaternion.identity);
					GameStats.mapOfObjects [y].Add (t.gameObject);
					break;
				case GameStats.pointLegend:
					t = (Transform)Instantiate (pointPrefab, new Vector3 (-offSetX + x, offSetY - y, 0), Quaternion.identity);
					GameStats.mapOfObjects [y].Add (t.gameObject);
					break;
				case GameStats.enemyLegend:
					t = (Transform)Instantiate (enemyPrefab, new Vector3 (-offSetX + x, offSetY - y, 0), Quaternion.identity);
					GameStats.mapOfObjects [y].Add (t.gameObject);
					Enemy e = t.GetComponent<Enemy> ();
					e.pos.y = y;
					e.pos.x = x;
					break;
				}
			}
		}        
	}
		
}