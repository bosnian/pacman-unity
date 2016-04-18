using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI {

	public List<BaseObject> directions;

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

//	public EnemyAI(){
//		directions = new List<BaseObject> ();
//		directions.Add (right);
//		directions.Add (up);
//		directions.Add (left);
//		directions.Add (down);
//	}
//
//	private int min = 100000;
//	private int direction = 0;
//	private void calculate(int x, int y, int count,int direction){
//
//		if (count > 10)
//			return;
//
//		if ((x < 0 || y < 0) || (x >= GameStats.map [0].Length || y >= GameStats.map.Length))
//			return;
//
//		
//		if (x == GameStats.player.x && y == GameStats.player.y && count < min) {
//			min = count;
//			this.direction = direction;
//			return;
//		}
//
//		if (GameStats.checkIfMoveAvailable (x + 1, y)) {
//			if (count == 0)
//				direction = 0;
//			calculate (x + 1, y, count + 1,direction);
//		} 
//
//		if (GameStats.checkIfMoveAvailable (x, y + 1)) {
//			if (count == 0 )
//				direction = 1;
//			calculate (x, y + 1, count + 1,direction);
//		} 
//
//		if (GameStats.checkIfMoveAvailable (x - 1, y)) {
//			if (count == 0)
//				direction = 2;
//			calculate (x - 1, y, count + 1,direction);
//		} 
//
//		if (GameStats.checkIfMoveAvailable (x, y - 1)) {
//			if (count == 0)
//				direction = 3;
//			calculate (x, y - 1, count + 1,direction);
//		} 
//	}
//
//	public int getDirection(int x, int y){
//		min = 1000000;
//		calculate (x, y,0,0);
//		Debug.Log (min + "#");
//		return direction;
//	}

	public BaseObject getDirection(BaseObject currentPosition, BaseObject prevDirection,BaseObject lastH,BaseObject lastV ){

		var rnd = Random.Range (0, 3);

		if (prevDirection.Equals(right)) {

			if (rnd == 1) {
				if (GameStats.checkIfMoveAvailable (up + currentPosition)) {
					return up;
				}

				if (GameStats.checkIfMoveAvailable (down + currentPosition)) {
					return down;
				}
			} else {
				
				if (GameStats.checkIfMoveAvailable (down + currentPosition)) {
					return down;
				}

				if (GameStats.checkIfMoveAvailable (up + currentPosition)) {
					return up;
				}

			}



			if(GameStats.checkIfMoveAvailable(right+currentPosition)){
				return right;
			}

			if(GameStats.checkIfMoveAvailable(left+currentPosition)){
				return left;
			}
		}

		if (prevDirection.Equals(left)) {


			if (rnd == 1) {
				if (GameStats.checkIfMoveAvailable (up + currentPosition)) {
					return up;
				}

				if (GameStats.checkIfMoveAvailable (down + currentPosition)) {
					return down;
				}
			} else {

				if (GameStats.checkIfMoveAvailable (down + currentPosition)) {
					return down;
				}

				if (GameStats.checkIfMoveAvailable (up + currentPosition)) {
					return up;
				}
			}

			if(GameStats.checkIfMoveAvailable(left+currentPosition)){
				return left;
			}

			if(GameStats.checkIfMoveAvailable(right+currentPosition)){
				return right;
			}
		}

		if (prevDirection.Equals(up)) {

			if (rnd == 1) {
				if (GameStats.checkIfMoveAvailable (left + currentPosition)) {
					return left;
				}

				if (GameStats.checkIfMoveAvailable (right + currentPosition)) {
					return right;
				}

			} else {
				

				if(GameStats.checkIfMoveAvailable(right+currentPosition)){
					return right;
				}

				if(GameStats.checkIfMoveAvailable(left+currentPosition)){
					return left;
				}

			}
			if(GameStats.checkIfMoveAvailable(up+currentPosition)){
				return up;
			}

			if(GameStats.checkIfMoveAvailable(down+currentPosition)){
				return down;
			}
		}

		if (prevDirection.Equals(down)) {

			if (rnd == 1) {

				if(GameStats.checkIfMoveAvailable(right+currentPosition)){
					return right;
				}

				if(GameStats.checkIfMoveAvailable(left+currentPosition)){
					return left;
				}

			} else {
				if(GameStats.checkIfMoveAvailable(left+currentPosition)){
					return left;
				}

				if(GameStats.checkIfMoveAvailable(right+currentPosition)){
					return right;
				}
			}

			if(GameStats.checkIfMoveAvailable(down+currentPosition)){
				return down;
			}

			if(GameStats.checkIfMoveAvailable(up+currentPosition)){
				return up;
			}
		}

		return right;
	}
}
