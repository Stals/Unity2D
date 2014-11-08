using System;

using UnityEngine;
using System.Collections.Generic;

public class BoardObject{
	public int id;
	public Block block;

	public BoardObject(){
		id = 0;
	}

	public BoardObject(int _id){
		id = _id;
	}

	// TODo change to setting the Block class?
	public void setBlock(Block _block){
		block = _block;
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

	public void removeAt(int x, int y)
	{
		// TODO - move all blocks on to 1 lower

		// TODo dont forget to update x, y and obly change GO and what x, y they have inside.!!

		int height = rows.Count;
		for (; y < height - 1; ++y) {
			// swap
			Block dropingBlock = at (x, y + 1).block;
			at (x, y).setBlock( dropingBlock );
			at (x, y + 1).setBlock( null );
			if(dropingBlock != null){
				dropingBlock.setIDs(x, y);
			}
		}
	}
};


