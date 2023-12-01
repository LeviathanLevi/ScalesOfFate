using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI heavenScore1;
    [SerializeField]
    TextMeshProUGUI heavenScore2;
    [SerializeField]
    TextMeshProUGUI heavenScore3;
    [SerializeField]
    TextMeshProUGUI hellScore1;
    [SerializeField]
    TextMeshProUGUI hellScore2;
    [SerializeField]
    TextMeshProUGUI hellScore3;

    public TextMeshProUGUI totalScore;

    //Score tally animation:
    public GameObject heavenlyUnits;
    public GameObject hellishUnits;

    public Pivot pivot;

    [SerializeField]
    private GameObject settingsButton;

    [SerializeField]
    private GameObject unitSelection;

    // Start is called before the first frame update
    void Start()
    {
        heavenScore1.text = "";
        heavenScore2.text = "";
        heavenScore3.text = "";
        hellScore1.text = "";
        hellScore2.text = "";
        hellScore3.text = "";

        totalScore.text = "";

    }

    public void UpdateScores(int heavenScore, int hellScore, int round)
    {
        StartCoroutine(UpdateScoresWithAnim(heavenScore, hellScore, round));
    }

    IEnumerator UpdateScoresWithAnim(int heavenScore, int hellScore, int round)
    {
        settingsButton.SetActive(false);
        heavenlyUnits.SetActive(false);
        hellishUnits.SetActive(false);
        unitSelection.SetActive(false);

        if (heavenScore == 0)
        {
            pivot.HellLostRotatePivot();
        }
        else
        {
            pivot.HeavenLostRotatePivot();
        }

        if (round == 1)
        {
            if (heavenScore == 0)
            {
                //animate hell score going up:
                heavenScore1.text = heavenScore.ToString();
                float originalFontSize = hellScore1.fontSize;

                for (int i = 0; i < 20; ++i)
                {
                    hellScore1.fontSize = hellScore1.fontSize + 1;
                    hellScore1.text = ((hellScore / 20) * i).ToString();
                    yield return new WaitForSeconds(.1f);
                }

                hellScore1.text = hellScore.ToString();
                hellScore1.fontSize = originalFontSize;
            }
            else
            {
                //animate heaven score going up:
                hellScore1.text = hellScore.ToString();
                float originalFontSize = heavenScore1.fontSize;

                for (int i = 0; i < 20; ++i)
                {
                    heavenScore1.fontSize = heavenScore1.fontSize + 1;
                    heavenScore1.text = ((heavenScore / 20) * i).ToString();
                    yield return new WaitForSeconds(.1f);
                }

                heavenScore1.text = heavenScore.ToString();
                heavenScore1.fontSize = originalFontSize;
            }

            float totalScoreFontSize = totalScore.fontSize;

            for (int i = 0; i < 20; ++i)
            {
                totalScore.fontSize = totalScore.fontSize + 1;
                totalScore.text = ((Mathf.Abs(heavenScore - hellScore) / 20) * i).ToString();
                yield return new WaitForSeconds(.1f);
            }
            totalScore.fontSize = totalScoreFontSize;
            totalScore.text = Mathf.Abs(heavenScore - hellScore).ToString();
        }
        else if (round == 2)
        {
            //Round 2:

            if (heavenScore == 0)
            {
                //animate hell score going up:
                heavenScore2.text = heavenScore.ToString();
                float originalFontSize = hellScore2.fontSize;

                for (int i = 0; i < 20; ++i)
                {
                    hellScore2.fontSize = hellScore2.fontSize + 1;
                    hellScore2.text = ((hellScore / 20) * i).ToString();
                    yield return new WaitForSeconds(.1f);
                }

                hellScore2.text = hellScore.ToString();
                hellScore2.fontSize = originalFontSize;
            }
            else
            {
                //animate heaven score going up:
                hellScore2.text = hellScore.ToString();
                float originalFontSize = heavenScore2.fontSize;

                for (int i = 0; i < 20; ++i)
                {
                    heavenScore2.fontSize = heavenScore2.fontSize + 1;
                    heavenScore2.text = ((heavenScore / 20) * i).ToString();
                    yield return new WaitForSeconds(.1f);
                }

                heavenScore2.text = heavenScore.ToString();
                heavenScore2.fontSize = originalFontSize;
            }

            float totalScoreFontSize = totalScore.fontSize;

            for (int i = 0; i < 20; ++i)
            {
                totalScore.fontSize = totalScore.fontSize + 1;
                totalScore.text = ((Mathf.Abs((int.Parse(heavenScore1.text) + heavenScore) - (int.Parse(hellScore1.text) + hellScore)) / 20) * i).ToString();
                yield return new WaitForSeconds(.1f);
            }
            totalScore.fontSize = totalScoreFontSize;
            totalScore.text = Mathf.Abs((int.Parse(heavenScore1.text) + heavenScore) - (int.Parse(hellScore1.text) + hellScore)).ToString();
        }
        else if (round == 3)
        {
            //R3#########
            if (heavenScore == 0)
            {
                //animate hell score going up:
                heavenScore3.text = heavenScore.ToString();
                float originalFontSize = hellScore3.fontSize;

                for (int i = 0; i < 20; ++i)
                {
                    hellScore3.fontSize = hellScore3.fontSize + 1;
                    hellScore3.text = ((hellScore / 20) * i).ToString();
                    yield return new WaitForSeconds(.1f);
                }

                hellScore3.text = hellScore.ToString();
                hellScore3.fontSize = originalFontSize;
            }
            else
            {
                //animate heaven score going up:
                hellScore3.text = hellScore.ToString();
                float originalFontSize = heavenScore3.fontSize;

                for (int i = 0; i < 20; ++i)
                {
                    heavenScore3.fontSize = heavenScore3.fontSize + 1;
                    heavenScore3.text = ((heavenScore / 20) * i).ToString();
                    yield return new WaitForSeconds(.1f);
                }

                heavenScore3.text = heavenScore.ToString();
                heavenScore3.fontSize = originalFontSize;
            }

            float totalScoreFontSize = totalScore.fontSize;

            for (int i = 0; i < 20; ++i)
            {
                totalScore.fontSize = totalScore.fontSize + 1;
                totalScore.text = ((Mathf.Abs((int.Parse(heavenScore1.text) + int.Parse(heavenScore2.text) + heavenScore) - (int.Parse(hellScore1.text) + int.Parse(hellScore2.text) + hellScore)) / 20) * i).ToString();
                yield return new WaitForSeconds(.1f);
            }
            totalScore.fontSize = totalScoreFontSize;
            totalScore.text = Mathf.Abs((int.Parse(heavenScore1.text) + int.Parse(heavenScore2.text) + heavenScore) - (int.Parse(hellScore1.text) + int.Parse(hellScore2.text) + hellScore)).ToString();
        }

        yield return new WaitForSeconds(3);

        pivot.ResetPivot();

        heavenlyUnits.SetActive(true);
        hellishUnits.SetActive(true);

        settingsButton.SetActive(true);
        unitSelection.SetActive(true);

        yield return null;
    }
}
