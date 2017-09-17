
public static class SerchTimer
{
    static int sec, min;

    public static void Searching()
    {
        sec += 1;
        if (sec == 60)
        {
            sec = 0;
            min++;
        }
    }

    public static string DefaultState()
    {
        sec = 0;
        min = 0;
        return "00:00";
    }

    public static string Time()
    {
        string time = min.ToString("00") + ":" + sec.ToString("00");

        return time;
    }
}
