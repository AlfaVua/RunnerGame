using UnityEngine;

[CreateAssetMenu(menuName = "User Data", fileName = "User Save")]
public class UserData : ScriptableObject
{
    public int totalCoins = 0;
    public float maxScore = 0;
}