using UnityEngine;

namespace MimasakaShooter
{
    public class GameTimer
    {
        private float Time { get; set; }
        private float Length { get; }
        public float Rate { get; private set; }
        public bool IsFinished { get; private set; }

        public GameTimer(float length)
        {
            Length = length;
        }

        public void Add(float time)
        {
            Time += time;
            Rate = Mathf.Clamp01(Time/Length);
            IsFinished = Rate >= 1f;
        }
    }
}