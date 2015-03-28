using UnityEngine;
using System.Collections;

public class GameJoltAPIManager : MonoBehaviour
{
    public int gameID = 20247;
    public string privateKey = "950cc59a8bd8314eacc06beebd64c0b5";
    public string userName;
    public string userToken;
    public GJTrophy trophies;
    public static GameJoltAPIManager _selfRef;

    public bool gameStarted = false;


    private GJScore[] _scores;
    private bool blockUserInput = true;
    private bool once = true;

    private GameJoltAPIManager() { }

    /*void Awake()
    {
        _selfRef = this;
        DontDestroyOnLoad(gameObject);
        if (once) GJAPI.Init(gameID, privateKey);
    }*/

    void Awake () {
         DontDestroyOnLoad ( gameObject );
         GJAPI.Init ( gameID, privateKey );
         GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);

    }
    void Start()
    {
        //GJAPI.Init(gameID, privateKey);

        //if (once) GetScore();
        //GJAPIHelper.Users.GetFromWeb(OnGetFromWeb);
    }

    // Callback
    void OnGetFromWeb(string name, string token)
    {
        userName = name;
        userToken = token;
        GJAPI.Users.Verify(name, token);
    }

    void OnEnable()
    {
        GJAPI.Users.VerifyCallback += OnVerifyUser;
    }

    void OnDisable()
    {
        GJAPI.Users.VerifyCallback -= OnVerifyUser;
    }

    void OnVerifyUser(bool success)
    {
        if (success)
        {
            GJAPIHelper.Users.ShowGreetingNotification();
            gameStarted = true;
        }
        else
        {
            Debug.Log("Um... Something went wrong.");
        }
    }

    public void AddTrophy(uint id)
    {
        GJAPI.Trophies.Get(id);
        GJAPI.Trophies.GetOneCallback += _selfRef.OnGetRequestFinished;
    }

    private void OnGetRequestFinished(GJTrophy trophy)
    {
        if (trophy != null && !trophy.Achieved)
        {
            GJAPIHelper.Trophies.ShowTrophyUnlockNotification(trophy.Id);
            GJAPI.Trophies.Add(trophy.Id);
        }
    }

    public void GenerateHighscores(double highscore)
    {
        GJAPI.Scores.Add(((int)highscore).ToString(), (uint)(highscore));
        //GJAPI.Scores.AddCallback += _selfRef.OnAddRequestFinished;
    }

    private void OnAddRequestFinished(bool success)
    {
        GetScore();
    }

    private void GetScore()
    {
        once = false;
        GJAPI.Scores.Get();
        GJAPI.Scores.GetMultipleCallback += _selfRef.GetScoreCallback;
        //if(gameStarted) GameManager.ShowHighscore = true;
    }

    private void GetScoreCallback(GJScore[] scores)
    {
        _scores = scores;
        blockUserInput = false;
    }

    void OnApplicationQuit()
    {
        foreach (var go in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            Destroy(go);
        }
    }
    public bool BlockUserInput
    {
        get { return _selfRef.blockUserInput; }
        set { _selfRef.blockUserInput = value; }
    }

    public GJScore[] Scores
    {
        get { return _selfRef._scores; }
        set { _selfRef._scores = value; }
    }
}