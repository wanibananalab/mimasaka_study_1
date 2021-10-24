using UnityEngine;

namespace MimasakaShooter
{
    public class GameEnemyInfo
    {
        public int ID { get; }
        public Vector3 Position { get; }

        public GameEnemyInfo(int id, Vector3 position)
        {
            ID = id;
            Position = position;
        }
    }
}