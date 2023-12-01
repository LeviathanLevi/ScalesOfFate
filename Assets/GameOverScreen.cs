using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameController controller;

    public GameObject[] leaderboardPlacements;

    public TMP_InputField gameInputField;

    public GameObject submissionScreen;
    public GameObject resultsScreen;

    public VoiceOvers voiceOvers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        voiceOvers.PlayClip(4);
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        string leaderboardKey = "ScalesOfFateLeaderboard";
        int count = 10;

        LootLockerSDKManager.GetScoreList(leaderboardKey, count, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");

                for (int i = 0; i < 10; ++i)
                {
                    leaderboardPlacements[i].transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = response.items[i].member_id;
                    leaderboardPlacements[i].transform.Find("Score").gameObject.GetComponent<TextMeshProUGUI>().text = response.items[i].score.ToString();
                }
            }
            else
            {
                Debug.Log("failed: " + response.errorData);
            }
        });
    }

    public void SubmitScore()
    {
        string scoreName = gameInputField.text.Replace(" ", "");

        if (scoreName.Length > 3)
        {
            string memberID = scoreName;
            string leaderboardKey = "ScalesOfFateLeaderboard";
            int score = Int32.Parse(controller.scoreMenu.scoreKeeper.totalScore.text);

            LootLockerSDKManager.SubmitScore(memberID, score, leaderboardKey, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Successful");

                    LootLockerSDKManager.GetMemberRank(leaderboardKey, memberID, (response) =>
                    {
                        if (response.statusCode == 200)
                        {
                            Debug.Log("Successful");
                            Debug.Log(response);
                            resultsScreen.GetComponentInChildren<TextMeshProUGUI>().text = "You rank #" + response.rank + " with a score of " + response.score + " points! Thanks for playing!";
                            UpdateLeaderboard();
                        }
                        else
                        {
                            Debug.Log("failed: " + response.errorData);
                        }
                    });

                    submissionScreen.SetActive(false);
                    resultsScreen.SetActive(true);
                }
                else
                {
                    Debug.Log("failed: " + response.errorData);
                }
            });
        }
        else
        {
            gameInputField.text = "Name too short";
        }

        UpdateLeaderboard();
    }
}
