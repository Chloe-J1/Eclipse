using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public interface IScreenShake
    {
        void ShakeCamera(Camera camera, Gamepad gamepad)
        {
            HapticFeedbackManager.Instance.ShakeScreen(camera);
            HapticFeedbackManager.Instance.Rumble(gamepad);
        }
    }
}

