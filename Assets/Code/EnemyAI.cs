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
