using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaysController : MonoBehaviour
{
    public static DaysController Instance;

    [System.Serializable]
    public class Day
    {
        public bool InvertDirection;
        public string StartDayLetterText;
    }

    public List<Day> days = new List<Day>();

    public Animator animator;
    public float hourLength;
    public Vector2Int hourRange = new Vector2Int(10, 17);
    private float hourTimer;
    private bool timeCounting;
    public bool letterTaken;
    private int dayHour, dayIndex;
    private Station currentStation;
    public Station stationA, stationB;
    public BearMovement bear;
    public Vector3 bearAPos, bearBPos;
    public DrezinawithMisha drezina;
    public Vector3 drezinaAPos, drezinaBPos;
    public Animal[] animals;

    void Start()
    {
        Instance = this;
        StartDay(0);
    }

    public void StartDay(int i)
    {
        dayIndex = i + 1;
        animator.SetTrigger("dayStart");
        timeCounting = false;
        letterTaken = false;
        dayHour = hourRange.x;
        UpdateTimeText();

        Day currentDay = days[i];
        if (currentDay.InvertDirection)
        {
            currentStation = stationB;
            stationA.SetEndStation();
            stationB.SetStartStation();
        }
        else
        {
            currentStation = stationA;
            stationB.SetEndStation();
            stationA.SetStartStation();
        }
        bear.transform.position = currentDay.InvertDirection ? bearBPos : bearAPos;
        drezina.transform.position = currentDay.InvertDirection ? drezinaBPos : drezinaAPos;
        drezina.MovingRight = !currentDay.InvertDirection;
        drezina.Reset();
        drezina.enabled = false;
    }

    public void StartTime()
    {
        if (timeCounting) return;

        currentStation.DrezinaBubble.gameObject.SetActive(false);
        hourTimer = hourLength;
        timeCounting = true;
    }

    public void TakeLetter()
    {
        letterTaken = true;
        currentStation.OnLetterTaken();
        drezina.enabled = true;
    }

    void Update()
    {
        if (!letterTaken && currentStation.LetterBox.BearNearBy && Input.GetKeyDown(KeyCode.E))
        {
            TakeLetter();
            return;
        }
        if (timeCounting)
        {
            hourTimer -= Time.deltaTime;
            if (hourTimer < 0)
            {
                hourTimer += hourLength;
                dayHour++;
                UpdateTimeText();
                if (dayHour >= hourRange.y)
                    timeCounting = false;
            }
        }
    }

    public void EndDay()
    {
        animator.SetTrigger("dayFinish");
        foreach (var animal in animals)
            animal.OnDayEnd(dayIndex);
        StartCoroutine(NextDay());
    }

    private IEnumerator NextDay()
    {
        yield return new WaitForSeconds(2);
        StartDay(dayIndex);
    }

    private void UpdateTimeText()
    {
        UIController.Instance.SetTime(dayIndex, dayHour);
    }
    
}
