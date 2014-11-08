using System;

using UnityEngine;
using System.Collections.Generic;

public class BoardObject{
	public int id;
	public GameObject go;

	public BoardObject(){
		id = 0;
	}

	public BoardObject(int _id){
		id = _id;
	}

	// TODo change to setting the Block class?
	public void setGO(GameObject _go){
		go = _go;
	}
}

public class BoardRow{
	public List<BoardObject> objects;

	public BoardRow(int width){
		objects = new List<BoardObject> (width);
		for (int i = 0; i < width; ++i) {
			objects.Add(new BoardObject());
		}
	}
}

public class Board
{
	public List<BoardRow> rows;

    public Board(int width, int height)
    {
		rows = new List<BoardRow> (height);
		for (int i = 0; i < height; ++i) {
			rows.Add(new BoardRow(width));
		}
    }

	public BoardRow rowAt(int id){
		return rows [id];
	}

	public BoardObject at(int x, int y){
		return rows [y].objects [x];
	}
};


