using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameWorld gameWorld;
        
        private GameInfo GameInfo { get; set; }

        private void Start()
        {
            Application.targetFrameRate = 60;

            GameInfo = new GameInfo();
            gameWorld.Initialize(GameInfo);
        }
        
        private void Update()
        {
            gameWorld.OnLoop();
        }

        private void LateUpdate()
        {
            gameWorld.OnLateLoop();
        }
    }

}

