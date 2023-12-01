using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;
    public GameController gameController;

    [SerializeField]
    GameObject heavenlyUnits;
    int numberOfHeavenlyUnits = 0;
    [SerializeField]
    GameObject hellishUnits;
    int numberOfHellishUnits = 0;

    public int heavenlyWeight = 0;
    public TextMeshProUGUI heavenlyWeightText;

    public int hellishWeight = 0;
    public TextMeshProUGUI hellishWeightText;

    [SerializeField]
    GameObject playingBackground;
    [SerializeField]
    GameObject roundBackground;

    bool playingActiveBackground = false;

    public VoiceOvers voiceOvers;

    // Start is called before the first frame update
    void Start()
    {
        playingBackground.SetActive(false);
        roundBackground.SetActive(true);

        heavenlyWeightText.text = "";
        hellishWeightText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameController.roundStarted);

        if (numberOfHeavenlyUnits != heavenlyUnits.transform.childCount)
        {
            int weightSum = 0;
            numberOfHeavenlyUnits = heavenlyUnits.transform.childCount;
            for (int i = 0; i < numberOfHeavenlyUnits; i++)
            {
                weightSum += heavenlyUnits.transform.GetChild(i).GetComponent<UnitController>().weight;
            }
            heavenlyWeight = weightSum;
            heavenlyWeightText.text = heavenlyWeight.ToString() + " LB";

            if (weightSum == 0 && gameController.roundStarted == true)
            {
                voiceOvers.PlayClip(3);
                StartCoroutine(EndRound());
            }
        }

        if (numberOfHellishUnits != hellishUnits.transform.childCount)
        {
            int weightSum = 0;
            numberOfHellishUnits = hellishUnits.transform.childCount;
            for (int i = 0; i < numberOfHellishUnits; i++)
            {
                weightSum += hellishUnits.transform.GetChild(i).GetComponent<UnitController>().weight;
            }
            hellishWeight = weightSum;
            hellishWeightText.text = hellishWeight.ToString() + " LB";

            if (weightSum == 0 && gameController.roundStarted == true)
            {
                voiceOvers.PlayClip(3);
                StartCoroutine(EndRound());
            }
        }
    }

    public void ToggleBackgrounds()
    {
        playingActiveBackground = !playingActiveBackground;
        playingBackground.SetActive(playingActiveBackground);
        roundBackground.SetActive(!playingActiveBackground);
    }

    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(1f);

        gameController.EndRound();
    }
}
