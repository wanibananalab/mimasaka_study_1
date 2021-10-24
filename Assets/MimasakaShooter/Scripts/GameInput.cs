using UnityEngine;

namespace MimasakaShooter
{
    public static class GameInput
    {
        public static bool IsLeft()
        {
            return Input.GetKey(KeyCode.A);
        }
        
        public static bool IsRight()
        {
            return Input.GetKey(KeyCode.D);
        }
        
        public static bool IsFront()
        {
            return Input.GetKey(KeyCode.W);
        }
        
        public static bool IsBack()
        {
            return Input.GetKey(KeyCode.S);
        }

        public static bool IsShot()
        {
            return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Space);
        }

    }
}