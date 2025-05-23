using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; 
    public LeaderBoard leaderBoard;
    public GameObject playerNameScreen;
    public TMP_InputField playerNameInputField;
    [SerializeField]private float timer = 0f;
    public bool timeStarted = false;
    public int gameTimeElapsedInSeconds;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }


    void Start()
    {
        StartCoroutine(LoginRoutine());
    }
    void Update()
    {
        if (!timeStarted)
        {
            playerNameScreen.SetActive(true);
            return;
        }
        else
        {
            timer += Time.deltaTime;
        }
       
    }
    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set Player Name");
                StartCoroutine(LoginRoutine());
            }
            else
            {
                Debug.Log("Could not set Player Name" + response.errorData.message);
                StartCoroutine(LoginRoutine());
            }
            
        });
        timeStarted = true;
    }
    public int GetTime()
    {
        gameTimeElapsedInSeconds = Mathf.FloorToInt(timer);
        //Debug.Log("Elapsed Seconds: " + gameTimeElapsedInSeconds);
        return gameTimeElapsedInSeconds;
    }
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player logged in ");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }

            else
            {
                Debug.Log("Could Not Start Session");
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    
}
