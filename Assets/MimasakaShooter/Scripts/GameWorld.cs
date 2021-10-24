using System.Linq;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameWorld : MonoBehaviour
    {
        [SerializeField] private Camera currentCamera;
        [SerializeField] private GamePlayer gamePlayer;

        private GameInfo GameInfo { get; set; }
        private GameAssetCache<GameEnemy> EnemyCache { get; set; }

        public void Initialize(GameInfo gameInfo)
        {
            EnemyCache = new GameAssetCache<GameEnemy>(
                GameAsset.GetEnemy(),
                transform
            );
            GameInfo = gameInfo;
            gamePlayer.Initialize(gameInfo);
            
            // TODO: 仮
            var gameStage = GameStage.GetMock();

            foreach (var enemyInfo in gameStage.Enemies)
            {
                var enemy = EnemyCache.Create();
                enemy.Initialize(gameInfo, enemyInfo, currentCamera);
                enemy.transform.position = enemyInfo.Position;
            }
        }

        public void OnLoop()
        {
            GameInfo.Apply();
        }

        public void OnLateLoop()
        {
            gamePlayer.OnLateLoop();
            foreach (var enemy in EnemyCache.GetUses())
            {
                enemy.OnLateLoop();
            }

            EnemyCache.RemoveAll(EnemyCache.GetUses().Where(x => x.IsHide));
        }
    }
}