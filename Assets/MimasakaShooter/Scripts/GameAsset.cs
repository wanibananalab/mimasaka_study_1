using System;
using System.Linq;
using UnityEngine;

namespace MimasakaShooter
{
    public static class GameAsset
    {
        private const string GameAssetBasePath = "Sprites/stgpics_basic";

        private static T GetAsset<T>(int v) where T : UnityEngine.Object
        {
            return Resources.Load<T>(string.Intern($"{GameAssetBasePath}_{v}"));
        }

        public static Sprite GetPlayerSprite(GameDirection direction)
        {
            switch (direction)
            {
                case GameDirection.Left:
                    return GetAsset<Sprite>(0);
                case GameDirection.Front:
                    return GetAsset<Sprite>(1);
                case GameDirection.Right:
                    return GetAsset<Sprite>(2);
                default:
                    throw new NotImplementedException();
            }
        }

        public static Sprite GetEnemySprite(GameEnemyInfo enemyInfo)
        {
            var id = 0;
            switch (enemyInfo.ID)
            {
                case 1:
                    id = 127;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return GetAsset<Sprite>(id);
        }

        public static Sprite GetAttackSprite(GameAttackType type)
        {
            switch (type)
            {
                case GameAttackType.PlayerNormalShot:
                    return GetAsset<Sprite>(11);
                default:
                    throw new NotImplementedException();
            }
        }

        public static GameEnemy GetEnemy()
        {
            return Resources.Load<GameEnemy>(string.Intern("Prefabs/Enemy"));
        }

        public static GameAttack GetAttack(GameAttackType type)
        {
            switch (type)
            {
                case GameAttackType.PlayerNormalShot:
                    return Resources.Load<GameAttack>(string.Intern("Prefabs/PlayerNormalShot"));
                default:
                    throw new NotImplementedException();
            }
        }

        public static Sprite[] GetNormalCrashes()
        {
            return new int[] {534, 535, 536, 537}.Select(GetAsset<Sprite>).ToArray();
        }
    }
}