using System;

[Serializable]
public class ImageFile
{
    public string name;
    public string url;
    public int year;
    public int month;
    public int day;
    public long time;

    public ImageFile()
    {

    }
    public ImageFile(string name, string url, int year, int month, int day, long time)
    {
        this.name = name;
        this.url = url;
        this.year = year;
        this.month = month;
        this.day = day;
        this.time = time;
    }
}
