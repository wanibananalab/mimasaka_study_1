using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MimasakaShooter
{
    public class GamePlayer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer image;
        [SerializeField] private Camera targetCamera;
        [SerializeField] private Rigidbody2D rigidbody;

        private Sprite LeftSprite { get; set; }
        private Sprite FrontSprite { get; set; }
        private Sprite RightSprite { get; set; }
        private GameInfo GameInfo { get; set; }
        private float AutoMovePower => 2f;
        private float MovePower => 4f;
        private float WidthSize { get; set; }
        private float HeightSize { get; set; }

        private List<GameAttack> GameAttacks { get; set; }
        private List<GameAttack> CacheGameAttacks { get; set; }

        private int ShotCount { get; set; }

        public void Initialize(GameInfo gameInfo)
        {
            GameAttacks = new List<GameAttack>();
            CacheGameAttacks = new List<GameAttack>();

            GameInfo = gameInfo;

            LeftSprite = GameAsset.GetPlayerSprite(GameDirection.Left);
            FrontSprite = GameAsset.GetPlayerSprite(GameDirection.Front);
            RightSprite = GameAsset.GetPlayerSprite(GameDirection.Right);

            transform.position = new Vector3(0, -(targetCamera.orthographicSize * 2 / 10 * 4), 0);

            WidthSize = FrontSprite.rect.width / FrontSprite.pixelsPerUnit;
            HeightSize = FrontSprite.rect.height / FrontSprite.pixelsPerUnit;
        }
        
        public void OnLateLoop()
        {
            foreach (var attack in GameAttacks)
            {
                attack.OnLateLoop();
            }

            if (GameAttacks.Any(x => !x.IsPlaying))
            {
                CacheGameAttacks.AddRange(GameAttacks.Where(x => !x.IsPlaying));
                GameAttacks.RemoveAll(x => !x.IsPlaying);
            }

            Shot();
            AutoMove();
            Move();
        }

        private void Shot()
        {
            if (!GameInfo.IsShot)
            {
                ShotCount = 0;
                return;
            }

            if (ShotCount == 0)
            {
                CreateShot();
            }
            ShotCount++;
            if (ShotCount >= 8)
            {
                ShotCount = 0;
            }
        }

        private void AutoMove()
        {
            targetCamera.transform.position += new Vector3(0, AutoMovePower / 100f * GameInfo.TimeScale, 0);
            transform.position += new Vector3(0, AutoMovePower / 100f * GameInfo.TimeScale, 0);
        }

        private void Move()
        {
            var direction = GameDirection.Front;

            if (GameInfo.IsLeft || GameInfo.IsRight || GameInfo.IsFront || GameInfo.IsBack)
            {
                var power = new Vector3();

                if (GameInfo.IsLeft && !GameInfo.IsRight)
                {
                    power += new Vector3(-MovePower, 0, 0);
                    direction = GameDirection.Left;
                }

                if (!GameInfo.IsLeft && GameInfo.IsRight)
                {
                    power += new Vector3(MovePower, 0, 0);
                    direction = GameDirection.Right;
                }

                if (GameInfo.IsFront && !GameInfo.IsBack)
                {
                    power += new Vector3(0, MovePower, 0);
                }

                if (!GameInfo.IsFront && GameInfo.IsBack)
                {
                    power += new Vector3(0, -MovePower, 0);
                }

                ExecuteMovePosition(transform.position, power / 100f);
            }

            switch (direction)
            {
                case GameDirection.Right:
                    image.sprite = RightSprite;
                    break;
                case GameDirection.Left:
                    image.sprite = LeftSprite;
                    break;
                default:
                    image.sprite = FrontSprite;
                    break;
            }
        }

        private void ExecuteMovePosition(Vector3 current, Vector3 movePower)
        {
            var result = current + movePower;

            var centerPosition = new Vector3(targetCamera.transform.position.x, targetCamera.transform.position.y);
            var xLength = (targetCamera.aspect * targetCamera.orthographicSize);
            var yLength = targetCamera.orthographicSize;

            var maxX = xLength - WidthSize / 2 + centerPosition.x;
            var minX = -xLength + WidthSize / 2f + centerPosition.x;
            var maxY = yLength - HeightSize / 2 + centerPosition.y;
            var minY = -yLength + HeightSize / 2f + centerPosition.y;

            if (result.x > maxX) result = new Vector3(maxX, result.y);
            if (result.x < minX) result = new Vector3(minX, result.y);
            if (result.y > maxY) result = new Vector3(result.x, maxY);
            if (result.y < minY) result = new Vector3(result.x, minY);

            transform.position = result;
        }

        private void CreateShot()
        {
            var instance = CacheGameAttacks.FirstOrDefault();
            if (instance != null)
            {
                CacheGameAttacks.Remove(instance);
            }
            else
            {
                instance = Instantiate(GameAsset.GetAttack(GameAttackType.PlayerNormalShot), transform.parent, true);
            }

            instance.Apply(transform, GameInfo, targetCamera);
            GameAttacks.Add(instance);
        }
    }
}