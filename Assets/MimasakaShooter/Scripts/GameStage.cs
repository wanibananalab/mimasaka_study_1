using UnityEngine;

namespace MimasakaShooter
{
    public class GameStage
    {
        public GameEnemyInfo[] Enemies;

        public static GameStage GetMock()
        {
            var instance = new GameStage();
            instance.Enemies = new[]
            {
                new GameEnemyInfo(1, new Vector3(0.5f, 2.5f)),
                new GameEnemyInfo(1, new Vector3(1.0f, 3.0f)),
                new GameEnemyInfo(1, new Vector3(1.5f, 3.5f)),
                new GameEnemyInfo(1, new Vector3(2.0f, 4.0f)),
            };
            return instance;
        }
    }
}