using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string firstPlayerInterest;
    private static string secondPlayerInterest;

    public static void SetFirstPlayerInterest(string interest)
    {
        firstPlayerInterest = interest;
    }

    public static string GetFirstPlayerInterest()
    {
        return firstPlayerInterest;
    }

    public static void SetSecondPlayerInterest(string interest)
    {
        secondPlayerInterest = interest;
    }

    public static string GetSecondPlayerInterest()
    {
        return secondPlayerInterest;
    }
}
