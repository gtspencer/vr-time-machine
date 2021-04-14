using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using System.Data.Sqlite;

public class TimeMachine : MonoBehaviour
{
    public Text yearText;
    public Text monthText;
    public Text dayText;
    public ImageUI imageUI;

    [NonSerialized] public int year;
    [NonSerialized] public int month;
    [NonSerialized] public int day;

    DBHelper dbHelper;

    // Start is called before the first frame update
    void Start()
    {
        DateTime now = DateTime.Now;
        year = now.Year;
        month = now.Month;
        day = now.Day;
        UpdateUI();
        dbHelper = new DBHelper();
        foreach (KeyValuePair<int, int> kv in DBHelper.GetTotalsForYears())
        {
            Debug.LogError(kv.Key + " " + kv.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GoPressed()
    {
        DateTime time = new DateTime(year, month, day);
        // DateTime time = new DateTime(2019, 5, 4);
        List<ImageFile> files = DBHelper.GetFilesFromDate(time);

        UnityEngine.Debug.LogError("found files: " + files.Count);
        imageUI.ShowNewImages(files);
    }

    public void PreviousYear()
    {
        year--;
        UpdateUI();
    }

    public void NextYear()
    {
        year++;
        if (year > DateTime.Now.Year)
            year = DateTime.Now.Year;
        UpdateUI();
    }

    public void PreviousMonth()
    {
        month--;
        if (month <= 0)
            month = 1;
        UpdateUI();
    }

    public void NextMonth()
    {
        month++;
        if (month > 12)
            month = 12;
        UpdateUI();
    }

    public void PreviousDay()
    {
        day--;
        if (day <= 0)
            day = 1;
        UpdateUI();
    }

    public void NextDay()
    {
        day++;
        if (day > DateTime.DaysInMonth(year, month))
            day = DateTime.DaysInMonth(year, month);
        UpdateUI();
    }

    public void UpdateUI()
    {
        yearText.text = year.ToString();
        monthText.text = month.ToString();
        dayText.text = day.ToString();
    }

    
}
