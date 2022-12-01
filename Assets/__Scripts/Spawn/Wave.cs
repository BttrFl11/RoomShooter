namespace EnemySpawn
{
    [System.Serializable]
    public struct Wave
    {
        public Enemy EnemyPrefab;
        public int EnemyCount;
        public float SpawnRate;
        public float StartDelay;
    }
}