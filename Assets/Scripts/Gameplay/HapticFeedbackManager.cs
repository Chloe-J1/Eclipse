using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Eclipse
{
    public class HapticFeedbackManager : MonobehaviourSingleton<HapticFeedbackManager>
    {
        Tween m_rumbleTween;
        Gamepad m_gamepad;
        Tween m_shakeTween;

        public void ShakeScreen(Camera camera)
        {
            const float shakeDuration = 1.0f;
            const float shakeStrength = 0.5f;
            m_shakeTween = camera?.DOShakePosition(shakeDuration, shakeStrength).SetLink(camera.gameObject); // Link makes it stop shaking when camera gets destroyed
        }
        public void Rumble(Gamepad gamepad)
        {
            //const float duration = 0.5f;
            //const float motorSpeed = 0.25f;
            //m_gamepad = gamepad;
            //gamepad.SetMotorSpeeds(motorSpeed, motorSpeed);

            //m_rumbleTween = DOVirtual.DelayedCall(duration, () => { gamepad.SetMotorSpeeds(0, 0); }); // Stop vibration after some time
        }

        void OnDestroy()
        {
            if (m_gamepad != null)
                m_gamepad.SetMotorSpeeds(0, 0);

            m_rumbleTween.Kill();
            m_shakeTween.Kill();
        }
    }

}
