using UnityEngine;
using System.Collections;

public class BoardGenerator {

	int objectTypes;
	int width;
	int height;

	public BoardGenerator(int _objectTypes, int _width, int _height){
		objectTypes = _objectTypes;
		width = _width;
		height = _height;
	}

	public Board generateBoard(){
		Board board = new Board (width, height);

		for (int y = 0; y < height; ++y) {
			for(int x = 0; x < width; ++x){
				board.at(x, y).id = getRandomObject();
			}
		}

		return board;
	}

	int getRandomObject()
	{
		// todo add weighted random later
		return Random.Range (0, objectTypes);
	}
}
