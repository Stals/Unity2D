using UnityEngine;
using System.Collections;

/// <summary>
/// Game Jolt API Helper Scores methods.
/// </summary>
public class GJHScoresMethods
{
	/// <summary>
	/// The leaderboard window.
	/// </summary>
	GJHScoresWindow window = null;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="GJHScoresMethods"/> class.
	/// </summary>
	public GJHScoresMethods ()
	{
		window = new GJHScoresWindow ();
	}
	
	/// <summary>
	/// Shows the leaderboards window.
	/// </summary>
	public void ShowLeaderboards ()
	{
		window.Show ();
	}
	
	/// <summary>
	/// Dismisses the leaderboards window.
	/// </summary>
	public void DismissLeaderboards ()
	{
		window.Dismiss ();
	}
}