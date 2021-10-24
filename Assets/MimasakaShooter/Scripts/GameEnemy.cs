using System;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameEnemy : MonoBehaviour, IGameUnit
    {
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private Collider2D collider;

        private Camera CurrentCamera { get; set; }
        public bool IsHide { get; private set; }
        private GameInfo GameInfo { get; set; }

        public int ID => gameObject.GetInstanceID();

        private Sprite[] Crashs { get; set; }

        public void Initialize(GameInfo gameInfo, GameEnemyInfo enemyInfo, Camera camera)
        {
            GameInfo = gameInfo;
            gameInfo.Register(this);
            CurrentCamera = camera;
            ApplySprite(enemyInfo);
            Crashs = GameAsset.GetNormalCrashes();
        }

        private void ApplySprite(GameEnemyInfo enemyInfo)
        {
            renderer.sprite = GameAsset.GetEnemySprite(enemyInfo);
        }

        public void Hit()
        {
            Crash();
        }

        private void Crash()
        {
            collider.enabled = false;
            GameEventManager.Create()
                .Timer(0.05f, () =>
                {
                    renderer.sprite = Crashs[0];
                }, () =>{})
                .Timer(0.05f, () =>
                {
                    renderer.sprite = Crashs[1];
                }, () =>{})
                .Timer(0.05f, () =>
                {
                    renderer.sprite = Crashs[2];
                }, () =>{})
                .Timer(0.05f, () =>
                {
                    renderer.sprite = Crashs[3];
                }, () =>{})
                .Finish(() =>
                {
                    IsHide = true;
                })
                .Play();
        }

        public void OnLateLoop()
        {
        }
    }
}