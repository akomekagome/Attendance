using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toukou;

namespace Toukou
{
    public class Constants : MonoBehaviour{
        
        public static float DEAD_POS_Y { get; private set; }
        public static float CAMERA_CAN_MOVEPOS_Y { get; private set; }
        public static float FIELD_CREATEPOS_y { get; private set; }
        public const float CAMERA_CAN_MOVEPOS_X_COMPLEMENT = 4f;
        public const float ENEMY_CANMOVEPOS_X_COMPLEMENT = 3f;
        public const float PLAYER_MOVELIMIT_X_COMPLEMENT = 0.5f;
        public const float EAGLE_MOVELIMIT_Y_COMPLEMENT = 1f;
        public const float NOTICE_POS_X_COMPLEMENT = 0.1f;
        public const float G = 9.8f;
        public const float LEVELCHANGE_INTERVAL = 2.0f;
        public const float SCORE_MAGNIFIACAITON = 20f;
        public const float BULLET_INTERVAL_TIME = 2f;
        public const float FROG_WAIT_TIME = 1.5f;

        private void Awake()
        {
            var min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            CAMERA_CAN_MOVEPOS_Y = min.y + 7f;
            DEAD_POS_Y = min.y - 1f;
            FIELD_CREATEPOS_y = min.y + 0.96f;
        }
    }
}

