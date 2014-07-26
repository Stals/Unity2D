using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour {

	public GameObject[] availableRooms;
	
	public List<GameObject> currentRooms;
	
	private float screenWidthInPoints;

	// Use this for initialization
	void Start () {
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate () 
	{    
		GenerateRoomIfRequired();
	}

	// http://stackoverflow.com/questions/11949463/how-to-get-size-of-parent-game-object
	float getRoomWidth(GameObject room)
	{
		// First find a center for your bounds.
		Vector3 center = Vector3.zero;
		
		foreach (Transform child in room.transform)
		{
			center += child.gameObject.renderer.bounds.center;   
		}
		center /= room.transform.childCount; //center is average center of children
		
		//Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
		Bounds bounds = new Bounds(center,Vector3.zero); 
		
		foreach (Transform child in room.transform)
		{
			bounds.Encapsulate(child.gameObject.renderer.bounds);   
		}

		return bounds.size.x;

		/*Bounds bounds = new Bounds ();
		foreach (Transform child in room.transform)
		{
			bounds.Encapsulate(child.gameObject.renderer.bounds);
		}

		return bounds.size.x;*/
	}

	void AddRoom(float farhtestRoomEndX)
	{
		int randomRoomIndex = Random.Range(0, availableRooms.Length);
		GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

		float roomWidth = getRoomWidth (room);
		float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
		room.transform.position = new Vector3(roomCenter, 0, 0);

		currentRooms.Add(room);         
	}

	void GenerateRoomIfRequired()
	{
		//1
		List<GameObject> roomsToRemove = new List<GameObject>();
		
		//2
		bool addRooms = true;        
		
		//3
		float playerX = transform.position.x;
		
		//4
		float removeRoomX = playerX - screenWidthInPoints;        
		
		//5
		float addRoomX = playerX + screenWidthInPoints;
		
		//6
		float farthestRoomEndX = 0;
		
		foreach(var room in currentRooms)
		{
			//7
			float roomWidth = getRoomWidth(room);
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);    
			float roomEndX = roomStartX + roomWidth;                            
			
			//8
			if (roomStartX > addRoomX)
				addRooms = false;
			
			//9
			if (roomEndX < removeRoomX)
				roomsToRemove.Add(room);
			
			//10
			farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
		}
		
		//11
		foreach(var room in roomsToRemove)
		{
			currentRooms.Remove(room);
			Destroy(room);            
		}
		
		//12
		if (addRooms)
			AddRoom(farthestRoomEndX);
	}

}
