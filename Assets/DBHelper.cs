using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using UnityEngine;

public class DBHelper : MonoBehaviour
{
    static string url;
    static IDbConnection dbconn;
    static string tableName = "Files";
    

    void Awake()
    {
        url = "URI=file:" + Application.dataPath + "/database.db";
        CreateConnection();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CreateConnection()
    {
        dbconn = (IDbConnection)new SqliteConnection(url);
        dbconn.Open();
    }

    public static List<ImageFile> GetFilesFromDate(DateTime date)
    {
        List<ImageFile> files = new List<ImageFile>();
        IDbCommand dbcmd = dbconn.CreateCommand();
        // SQLiteDataReader sqlite_datareader;
        string command = $"SELECT * FROM {tableName} " +
            $"WHERE year = {date.Year} " +
            $"AND month = {date.Month} " +
            $"AND day = {date.Day}";
        dbcmd.CommandText = command;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string name = reader.GetString(0);
            string url = reader.GetString(1);
            int year = reader.GetInt32(2);
            int month = reader.GetInt32(3);
            int day = reader.GetInt32(4);
            long time = reader.GetInt32(5);

            files.Add(new ImageFile(name, url, year, month, day, time));
        }
        return files;
    }

    public static Dictionary<int, int> GetTotalsForYears()
    {
        int minYear = 0;
        int maxYear = DateTime.Now.Year;
        IDbCommand dbcmd = dbconn.CreateCommand();
        string command = $"SELECT MIN(year) FROM {tableName}";
        dbcmd.CommandText = command;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            minYear = reader.GetInt32(0);
        }
        IDbCommand dbcmd1 = dbconn.CreateCommand();
        string command1 = $"SELECT MAX(year) FROM {tableName}";
        dbcmd1.CommandText = command1;
        IDataReader reader1 = dbcmd1.ExecuteReader();
        while (reader1.Read())
        {
            maxYear = reader1.GetInt32(0);
        }
        Dictionary<int, int> yearValues = new Dictionary<int, int>();
        for (int i = minYear; i <= maxYear; i++)
        {
            IDbCommand dbcmd2 = dbconn.CreateCommand();
            string command2 = $"SELECT COUNT(*) FROM {tableName} WHERE year = {i}";
            dbcmd2.CommandText = command2;
            IDataReader reader2 = dbcmd2.ExecuteReader();
            while (reader2.Read())
            {
                if (yearValues.ContainsKey(i))
                    yearValues[i] = reader2.GetInt32(0);
                else
                {
                    yearValues.Add(i, reader2.GetInt32(0));
                }
            }
        }
        return yearValues;
    }

    public static Dictionary<int, int> GetTotalForMonths(int year)
    {
        Dictionary<int, int> monthsDict = new Dictionary<int, int>();

        for (int i = 1; i <= 12; i++)
        {
            IDbCommand dbcmd = dbconn.CreateCommand();
            string command = $"SELECT COUNT(*) FROM {tableName} WHERE year = {year} AND month={i}";
            dbcmd.CommandText = command;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                monthsDict.Add(i, reader.GetInt32(0));
            }
        }

        return monthsDict;
    }

    public static Dictionary<int, int> GetTotalForDays(int year, int month)
    {
        Dictionary<int, int> daysDict = new Dictionary<int, int>();
        
        for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
        {
            IDbCommand dbcmd = dbconn.CreateCommand();
            string command = $"SELECT COUNT(*) FROM {tableName} WHERE year = {year} AND month = {i} AND day = {i}";
            dbcmd.CommandText = command;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                daysDict.Add(i, reader.GetInt32(0));
            }
        }

        return daysDict;
    }
}
