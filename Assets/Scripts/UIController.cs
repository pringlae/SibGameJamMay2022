using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Text[] keyNames = new Text[0];
    public Text[] keyDescriptions = new Text[0];
    public GameObject drezinaButtons;
    public Image drezinaButtonUp, drezinaButtonDown;
    public Text coinsText, timeText;
    public Image clockArrow;

    private Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    public enum InfoButtonsState
    {
        Empty, UseItem, MovingDrezina, DrezinaIdle, OffDrezina, NearDrezina
    }

    public int Coins { get; private set; } = 0;

    public InfoButtonsState infoButtonsState { get; private set; } = InfoButtonsState.Empty;

    void Awake()
    {
        Instance = this;
    }

    public void SetInfoButtonsState(InfoButtonsState state)
    {
        if (infoButtonsState == state) return;

        infoButtonsState = state;
        Debug.Log("State changed: " + state);
        switch (infoButtonsState)
        {
            case InfoButtonsState.Empty:
                ActivateButtons(0);
                break;
            case InfoButtonsState.UseItem:
                ActivateButtons(1);
                keyNames[0].text = "E";
                keyDescriptions[0].text = "Использовать";
                break;
            case InfoButtonsState.MovingDrezina:
                ActivateButtons(1);
                keyNames[0].text = "Space";
                keyDescriptions[0].text = "Тормоз";
                break;
            case InfoButtonsState.DrezinaIdle:
                ActivateButtons(1);
                keyNames[0].text = "Space";
                keyDescriptions[0].text = "Слезть";
                break;
            case InfoButtonsState.OffDrezina:
                ActivateButtons(0);
                break;
            case InfoButtonsState.NearDrezina:
                ActivateButtons(1);
                keyNames[0].text = "Space";
                keyDescriptions[0].text = "Залезть";
                break;
        }
    }

    public void SetMovementState(bool visible, bool topKey = true)
    {
        if (!visible)
        {
            if (drezinaButtons.activeSelf)
                drezinaButtons.SetActive(false);
            return;
        }

        if (!drezinaButtons.activeSelf)
            drezinaButtons.SetActive(true);

        if (topKey)
        {
            drezinaButtonUp.color = Color.white;
            drezinaButtonDown.color = disabledColor;
        }
        else
        {
            drezinaButtonDown.color = Color.white;
            drezinaButtonUp.color = disabledColor;
        }
    }

    private void ActivateButtons(int count)
    {
        for (int i = 0; i < keyNames.Length; i++)
        {
            keyNames[i].transform.parent.gameObject.SetActive(i < count);
        }
    }

    public void AddCoins(int coins)
    {
        Coins += coins;
        coinsText.text = Coins.ToString();
    }

    public void SetTime(int dayIndex, int dayHour)
    {
        timeText.text = "День " + dayIndex + ", " + dayHour + ":00";
        clockArrow.transform.eulerAngles = new Vector3(0, 0, 270 - 180 / 6 * dayHour);
    }
}
