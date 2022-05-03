using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text[] keyNames = new Text[0];
    public Text[] keyDescriptions = new Text[0];
    public GameObject drezinaButtons;
    public Image drezinaButtonUp, drezinaButtonDown;

    private Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    public enum InfoButtonsState
    {
        Empty, MovingDrezina, DrezinaIdle, OffDrezina, NearDrezina
    }

    public InfoButtonsState infoButtonsState { get; private set; } = InfoButtonsState.Empty;

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
}