using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimasakaShooter
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GamePlayer gamePlayer;

        private bool isLeft, isRight;

        private GameInfo gameInfo;
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            
            gameInfo = new GameInfo();
            gamePlayer.Initialize(gameInfo);
        }
        
        private void Update()
        {
            gameInfo.Apply();
        }

        private void LateUpdate()
        {
            gamePlayer.OnLateLoop();
        }
    }

}

