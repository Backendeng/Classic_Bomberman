using UnityEngine;

[CreateAssetMenu(fileName = "_PlayerStatsData", menuName = "Data/Player")]
public class D_PlayerStats : ScriptableObject
{
    public int health = 3;
    public int maxHealth = 5;
    public int movementSpeed = 10;
    public int maxMovementSpeed = 14;
    public int numberOfBombs = 1;
    public int maxNumberOfBombs = 8;
}