using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MimasakaShooter
{
    public class GamePositionSync : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        public void OnUpdate()
        {
            transform.position = new Vector3(
                target.transform.position.x,
                target.transform.position.y,
                transform.position.z
            );
        }
    }
}