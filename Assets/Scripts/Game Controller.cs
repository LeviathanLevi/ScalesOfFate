using LootLocker.Requests;
using Michsky.MUIP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject heavenlyUnits;
    [SerializeField]
    GameObject hellishUnits;

    public ScoreMenu scoreMenu;

    [SerializeField]
    GameObject unitSelection;

    [SerializeField]
    PlayerController playerController;

    private int gameRound = 0;

    public bool roundStarted = false;

    [SerializeField]
    GameObject round2Enemies;
    [SerializeField]
    GameObject round3Enemies;

    [SerializeField]
    ButtonManager startButton;

    [SerializeField]
    GameObject projectiles;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    GameObject[] disableTheseForGameOverScreen;

    public delegate void OnRoundStart();
    public static event OnRoundStart onRoundStart;

    public delegate void OnRoundEnd();
    public static event OnRoundEnd onRoundEnd;

    [SerializeField]
    private SliderManager slider;

    [SerializeField]
    private GameObject settingsButton;

    public VoiceOvers voiceOvers;

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }

    public void StartRound()
    {
        if (scoreMenu.heavenlyWeight != 0 && scoreMenu.hellishWeight != 0)
        {
            voiceOvers.PlayClip(2);
            startButton.isInteractable = false;
            roundStarted = true;
            gameRound++;
            settingsButton.SetActive(false);
            scoreMenu.ToggleBackgrounds();
            playerController.allowUnitHoveringAndDeletion = false;
            unitSelection.SetActive(false);

            foreach (var unit in heavenlyUnits.GetComponentsInChildren<UnitController>())
            {
                unit.active = true;
            }
            foreach (var unit in hellishUnits.GetComponentsInChildren<UnitController>())
            {
                unit.active = true;
            }

            onRoundStart?.Invoke();
        }
        
    }

    public void EndRound()
    {
        if (roundStarted)
        {
            Time.timeScale = 0;

            List<GameObject> children = new List<GameObject>();

            foreach (Transform child in projectiles.transform)
            {
                children.Add(child.gameObject);
            }

            foreach (GameObject child in children)
            {
                Destroy(child);
            }

            //Reset units:
            foreach (UnitController unit in heavenlyUnits.GetComponentsInChildren<UnitController>())
            {
                unit.ResetUnit();
            }
            foreach (UnitController unit in hellishUnits.GetComponentsInChildren<UnitController>())
            {
                unit.ResetUnit();
            }

            Time.timeScale = 1;

            

            //old update score placement
            scoreMenu.ToggleBackgrounds();

            foreach (var unit in heavenlyUnits.GetComponentsInChildren<UnitController>())
            {
                unit.active = false;
            }
            foreach (var unit in hellishUnits.GetComponentsInChildren<UnitController>())
            {
                unit.active = false;
            }

            if (gameRound + 1 == 2)
            {
                int round2EnemyCount = round2Enemies.transform.childCount;
                //spawn in round 2 enemies
                for (int i = round2EnemyCount - 1; i >= 0; --i)
                {
                    Transform child = round2Enemies.transform.GetChild(i);
                    child.SetParent(hellishUnits.transform, true);
                }
            }
            else if (gameRound + 1 == 3)
            {
                int round3EnemyCount = round3Enemies.transform.childCount;
                //spawn in round 3 enemies
                for (int i = round3EnemyCount - 1; i >= 0; --i)
                {
                    Transform child = round3Enemies.transform.GetChild(i);
                    child.SetParent(hellishUnits.transform, true);
                }
            }
            else if (gameRound +1 == 4)
            {
                playerController.movementActive = false;
                playerController.ResetCameraPosition();

                foreach(GameObject screen in disableTheseForGameOverScreen)
                {
                    screen.SetActive(false);
                }

                gameOverScreen.SetActive(true);
            }

            //update score and scale animation:
            scoreMenu.scoreKeeper.UpdateScores(scoreMenu.heavenlyWeight, scoreMenu.hellishWeight, gameRound);

            if (gameRound != 3)
            {
                startButton.isInteractable = true;
                playerController.allowUnitHoveringAndDeletion = true;
            }

            //settingsButton.SetActive(true);
            roundStarted = false;


            onRoundEnd?.Invoke();
        }
    }

    public void EngGameTest()
    {
        string memberID = "huh";
        string leaderboardID = "18968";
        int score = 5678;

        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }
            else
            {
                Debug.Log("failed: " + response.errorData);
            }
        });

        gameOverScreen.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateVolume()
    {
        GameResources.gameVolume = slider.mainSlider.value;
    }
}
