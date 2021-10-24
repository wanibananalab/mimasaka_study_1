using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameEventManager : MonoBehaviour
    {
        private static GameEventManager Instance { get; set; }

        private static GameEventManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new GameObject("gem").AddComponent<GameEventManager>();
            }

            return Instance;
        }

        public static GameEvent Create()
        {
            var instance = new GameEvent();
            GetInstance().Entry(instance);
            return instance;
        }

        public List<GameEvent> Events { get; } = new List<GameEvent>();

        private void Entry(GameEvent gameEvent)
        {
            Events.Add(gameEvent);
        }

        private void Update()
        {
            foreach (var gameEvent in Events)
            {
                if (!gameEvent.IsPlaying) continue;
                gameEvent.OnLoop();
            }

            Events.RemoveAll(x => x.IsFinished);
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }

    public class GameEvent
    {
        public bool IsPlaying { get; private set; }
        public bool IsFinished { get; private set; }

        private List<GameWaitEvent> Events { get; } = new List<GameWaitEvent>();

        private System.Action OnFinished { get; set; }

        private int Index { get; set; }

        public GameEvent()
        {
            IsPlaying = false;
            IsFinished = false;
            Index = 0;
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public GameEvent Timer(float time, System.Action start, System.Action end)
        {
            var timer = new GameTimer(time);

            Events.Add(new GameWaitEvent(() =>
            {
                start();
                return false;
            }));

            Events.Add(new GameWaitEvent(() =>
            {
                timer.Add(Time.deltaTime);
                if (timer.IsFinished)
                {
                    end();
                }

                return !timer.IsFinished;
            }));
            return this;
        }

        public GameEvent Finish(System.Action e)
        {
            OnFinished += e;
            return this;
        }

        public void OnLoop()
        {
            if (Events.Count == 0)
            {
                Finished();
                return;
            }

            var e = Events[Index];
            e.OnLoop();
            if (e.IsKeeping) return;
            Index++;

            if (Events.Count <= Index)
            {
                Finished();
            }
        }

        private void Finished()
        {
            IsFinished = true;
            OnFinished?.Invoke();
        }
    }

    public class GameWaitEvent
    {
        public bool IsKeeping { get; private set; }

        private System.Func<bool> Event { get; }

        public void OnLoop()
        {
            IsKeeping = Event();
        }

        public GameWaitEvent(System.Func<bool> e)
        {
            Event = e;
        }
    }
}