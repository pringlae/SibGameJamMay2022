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
    private int dayHour, dayIndex;
    public BoxCollider2D stationCollider, houseCollider;
    public UIController ui;
    public BearMovement bear;
    public Vector3 bearAPos, bearBPos;
    public DrezinawithMisha drezina;
    public Vector3 drezinaAPos, drezinaBPos;

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
        dayHour = hourRange.x;
        UpdateTimeText();

        Day currentDay = days[i];
        stationCollider.enabled = currentDay.InvertDirection;
        houseCollider.enabled = !currentDay.InvertDirection;
        bear.transform.position = currentDay.InvertDirection ? bearBPos : bearAPos;
        drezina.transform.position = currentDay.InvertDirection ? drezinaBPos : drezinaAPos;
        drezina.MovingRight = !currentDay.InvertDirection;
        drezina.Reset();
    }

    public void StartTime()
    {
        if (timeCounting) return;

        hourTimer = hourLength;
        timeCounting = true;
    }

    void Update()
    {
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
        Debug.Log("hi");
        animator.SetTrigger("dayFinish");
        StartCoroutine(NextDay());
    }

    private IEnumerator NextDay()
    {
        yield return new WaitForSeconds(2);
        StartDay(dayIndex);
    }

    private void UpdateTimeText()
    {
        ui.SetTime(dayIndex, dayHour);
    }
    
}
