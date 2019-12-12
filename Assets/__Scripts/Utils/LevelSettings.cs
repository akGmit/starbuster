using UnityEngine;
/// <summary>
/// A game object script which will hold for easy access a specific level settings.
/// </summary>
public class LevelSettings : MonoBehaviour
{
    public string Name;

    #region Level enemy settings
    public float EnemySpeed;
    public int EnemyScoreValue;
    public int EnemyCollisionDamage;
    public int EnemyShotDamage;
    public float EnemyBulletSpeed;
    public float EnemyFiringRate;
    public int EnemyStrength;
    #endregion

    public float LevelTime;
    public int LevelNumber;
    public int PowerUpSpawnRate;

    // Spawning settings
    public float SpawnRate;
    public float SpawnDelay;
}
