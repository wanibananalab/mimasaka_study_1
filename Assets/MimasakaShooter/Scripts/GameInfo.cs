using System.Collections.Generic;
using UnityEngine;

namespace MimasakaShooter
{
    public enum GameDirection
    {
        Left,
        Front,
        Right
    }

    public enum GameAttackType
    {
        PlayerNormalShot
    }

    public class GameInfo
    {

        public bool IsLeft { get; private set; }
        public bool IsRight { get; private set; }
        public bool IsFront { get; private set; }
        public bool IsBack { get; private set; }
        public bool IsShot { get; private set; }

        public float TimeScale { get; private set; }

        private Dictionary<int, IGameUnit> Entries { get; } = new Dictionary<int, IGameUnit>();

        public void Apply()
        {
            IsLeft = GameInput.IsLeft();
            IsRight = GameInput.IsRight();
            IsFront = GameInput.IsFront();
            IsBack = GameInput.IsBack();
            IsShot = GameInput.IsShot();

            TimeScale = Time.timeScale;
        }

        public IGameUnit GetUnit(int id)
        {
            return Entries.ContainsKey(id) ? Entries[id] : null;
        }

        public void Register(IGameUnit unit)
        {
            Entries.Add(unit.ID, unit);
        }

        public void Reject(IGameUnit unit)
        {
            Entries.Remove(unit.ID);
        }
    }
}