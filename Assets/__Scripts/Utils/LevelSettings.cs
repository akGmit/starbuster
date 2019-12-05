/// <summary>
/// An instance of this class will hold various level entities settings.
/// </summary>
public class LevelSettings
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

    public readonly float LevelTime;

    // Spawning settings
    public float SpawnRate;
    public float SpawnDelay;

    public LevelSettings(string name, float enemySpeed, int enemyScoreValue, 
        int enemyCollisionDamage, int enemyShotDamage, float enemyBulletSpeed, 
        float enemyFiringRate, int enemyStrength, float spawnRate, float spawnDelay, float levelTime)
    {
        Name = name;
        EnemySpeed = enemySpeed;
        EnemyScoreValue = enemyScoreValue;
        EnemyCollisionDamage = enemyCollisionDamage;
        EnemyShotDamage = enemyShotDamage;
        EnemyBulletSpeed = enemyBulletSpeed;
        EnemyFiringRate = enemyFiringRate;
        EnemyStrength = enemyStrength;
        SpawnRate = spawnRate;
        SpawnDelay = spawnDelay;
        LevelTime = levelTime;
    }
}
