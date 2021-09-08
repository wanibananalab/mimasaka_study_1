using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MimasakaShooter
{
    public class GamePlayer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer image;

        private Sprite leftSprite, frontSprite, rightSprite;

        private GameInfo gameInfo;

        private float MovePower
        {
            get
            {
                return 7f;
            }
        }

        public void Initialize(GameInfo gameInfo)
        {
            this.gameInfo = gameInfo;

            leftSprite = Resources.Load<Sprite>(string.Intern("Sprites/stgpics_basic_0"));
            frontSprite = Resources.Load<Sprite>(string.Intern("Sprites/stgpics_basic_1"));
            rightSprite = Resources.Load<Sprite>(string.Intern("Sprites/stgpics_basic_2"));
        }

        public void OnLateLoop()
        {
            Move();
        }

        private void Move()
        {
            var direction = GameDirection.Front;

            if (gameInfo.IsLeft || gameInfo.IsRight || gameInfo.IsFront || gameInfo.IsBack)
            {

                var power = new Vector3();

                if (gameInfo.IsLeft && !gameInfo.IsRight)
                {
                    power += new Vector3(-MovePower, 0, 0);
                    direction = GameDirection.Left;
                }

                if (!gameInfo.IsLeft && gameInfo.IsRight)
                {
                    power += new Vector3(MovePower, 0, 0);
                    direction = GameDirection.Right;
                }

                if (gameInfo.IsFront && !gameInfo.IsBack)
                {
                    power += new Vector3(0, MovePower, 0);
                }

                if (!gameInfo.IsFront && gameInfo.IsBack)
                {
                    power += new Vector3(0, -MovePower, 0);
                }
                transform.position += power / 100f;
            }


            switch (direction)
            {
                case GameDirection.Right:
                    image.sprite = rightSprite;
                    break;
                case GameDirection.Left:
                    image.sprite = leftSprite;
                    break;
                default:
                    image.sprite = frontSprite;
                    break;
            }
        }
    }
}