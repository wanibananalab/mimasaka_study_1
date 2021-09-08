using UnityEngine;

namespace MimasakaShooter
{

    public enum GameDirection
    {
        Left,
        Front,
        Right
    }
    
    public class GameInfo
    {
        private float deltaTime;
        private bool isLeft, isRight, isFront, isBack;

        public bool IsLeft => isLeft;
        public bool IsRight => isRight;
        public bool IsFront => isFront;
        public bool IsBack => isBack;

        public void Apply()
        {
            deltaTime = Time.time;

            isLeft = GameInput.IsLeft();
            isRight = GameInput.IsRight();
            isFront = GameInput.IsFront();
            isBack = GameInput.IsBack();
        }
    }
}