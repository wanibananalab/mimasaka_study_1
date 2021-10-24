using System;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameAttack : MonoBehaviour
    {
        [SerializeField] private Vector3 power;
        [SerializeField] private SpriteRenderer image;

        private GameInfo GameInfo { get; set; }
        private Camera TargetCamera { get; set; }
        public bool IsPlaying { get; private set; }

        private float WidthSize { get; set; }
        private float HeightSize { get; set; }

        public void Apply(Transform player, GameInfo gameInfo, Camera targetCamera)
        {
            transform.position = player.position;
            GameInfo = gameInfo;
            TargetCamera = targetCamera;

            WidthSize = image.sprite.rect.width / image.sprite.pixelsPerUnit;
            HeightSize = image.sprite.rect.height / image.sprite.pixelsPerUnit;
            IsPlaying = true;
            gameObject.SetActive(true);
        }

        public void OnLateLoop()
        {
            if (!IsPlaying)
            {
                return;
            }

            Move();
            if (!IsShow())
            {
                Hide();
            }
        }

        private void Hide()
        {
            IsPlaying = false;
            gameObject.SetActive(false);
        }

        private void Move()
        {
            transform.position += power / 100f * GameInfo.TimeScale;
        }

        private bool IsShow()
        {
            var centerPosition = new Vector3(TargetCamera.transform.position.x, TargetCamera.transform.position.y);
            var xLength = (TargetCamera.aspect * TargetCamera.orthographicSize);
            var yLength = TargetCamera.orthographicSize;

            var maxX = xLength + WidthSize / 2 + centerPosition.x;
            var minX = -xLength - WidthSize / 2f + centerPosition.x;
            var maxY = yLength + HeightSize / 2 + centerPosition.y;
            var minY = -yLength - HeightSize / 2f + centerPosition.y;

            if (transform.position.x > maxX) return false;
            if (transform.position.x < minX) return false;
            if (transform.position.y > maxY) return false;
            if (transform.position.y < minY) return false;

            return true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var unit = GameInfo.GetUnit(other.gameObject.GetInstanceID());
            if (unit == null) return;

            unit.Hit();
            Hide();
        }
    }
}