using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Note: первый бит ударный
public class Song{

	public Song(string _path, int _tempo, int _beatsPerBar, int _beat){
		path = _path;
		tempo = _tempo; // beats per minute
		beatsPerBar = _beatsPerBar;
		beat = _beat;}

	string path; // later

	public int tempo;
	public int beat;
	public int beatsPerBar;
}

public class Game  {
	private static Game instance;

	List<Song> songs;

	private Game() {}
	public static Game Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Game();
			}
			return instance;
		}
	}

	public void init(){
		setupSongs ();

	}

	private void setupSongs(){
		songs = new List<Song>();
		songs.Add(new Song("test", 120, 4, 4));
	}

	public Song getCurrentSong()
	{
		return songs[0];
	}
}
