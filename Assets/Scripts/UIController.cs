using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Text[] keyDescriptions = new Text[0];
    public GameObject drezinaButtons;
    public Image drezinaButtonUp, drezinaButtonDown;
    public Text coinsText, timeText;
    public Image clockArrow;
    public GameObject panelInTime, panelOutofTime;

    private Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    public int Coins { get; private set; } = 0;

    public enum InfoButton
    {
        Brake, GetOff, GetOn, Take, Water, Put, Give, PutAway, None
    }

    [System.Serializable]
    public class KeyDescription
    {
        public InfoButton infoButton;
        public string description;
    }
    public List<KeyDescription> descriptions = new List<KeyDescription>();

    void Awake()
    {
        Instance = this;
    }

    public void SetInfoButtonsState(InfoButton type, bool enabled = true)
    {
        if (type == InfoButton.None)
        {
            keyDescriptions[0].transform.parent.gameObject.SetActive(false);
            keyDescriptions[1].transform.parent.gameObject.SetActive(false);
            return;
        }
        KeyDescription desc = null;
        foreach (var d in descriptions)
            if (d.infoButton == type)
            {
                desc = d;
                break;
            }

        switch (type)
        {
            case InfoButton.Brake:
            case InfoButton.GetOff:
            case InfoButton.GetOn:
                keyDescriptions[0].transform.parent.gameObject.SetActive(enabled);
                keyDescriptions[0].text = desc.description;
                break;
            case InfoButton.Take:
            case InfoButton.Water:
            case InfoButton.Put:
            case InfoButton.Give:
            case InfoButton.PutAway:
                keyDescriptions[1].transform.parent.gameObject.SetActive(enabled);
                keyDescriptions[1].text = desc.description;
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
