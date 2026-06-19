
using System;
using UnityEditor;
using UnityEngine;
namespace Eclipse.Audio
{
    public enum PlayerType
    {
        Dark,
        Light
    }
    public static class AudioEvents
    {


        // Player Events
        public static event Action<PlayerType> OnJump;
        public static void Jump(PlayerType player) => OnJump?.Invoke(player);
        public static event Action<PlayerType> OnJumpVoice;
        public static void JumpVoice(PlayerType player) => OnJumpVoice?.Invoke(player);
        public static event Action<PlayerType> OnDashVoice;
        public static void DashVoice(PlayerType player) => OnDashVoice?.Invoke(player);
        public static event Action<PlayerType> OnPainVoice;
        public static void PainVoice(PlayerType player) => OnPainVoice?.Invoke(player);
        public static event Action<PlayerType> OnDeathVoice;
        public static void DeathVoice(PlayerType player) => OnDeathVoice?.Invoke(player);

        public static event Action<PlayerType> OnDash;
        public static void Dash(PlayerType player) => OnDash?.Invoke(player);

        public static event Action OnLightBeamStart;
        public static void LightBeamStart() => OnLightBeamStart?.Invoke();

        public static event Action OnLightBeamEnd;
        public static void LightBeamEnd() => OnLightBeamEnd?.Invoke();

        public static event Action OnShieldEquip;
        public static void ShieldEquip() => OnShieldEquip?.Invoke();

        public static event Action OnShieldStow;
        public static void ShieldStow() => OnShieldStow?.Invoke();
        public static event Action OnOrbPickup;
        public static void PickupOrb() => OnOrbPickup?.Invoke();
        public static event Action OnShieldPickup;
        public static void PickupShield() => OnOrbPickup?.Invoke();
        public static event Action OnGateOpen;
        public static void OpenGate() => OnGateOpen?.Invoke();
        public static event Action OnGateClose;
        public static void CloseGate() => OnGateClose?.Invoke();
        public static event Action OnDoorOpen;
        public static void OpenDoor() => OnDoorOpen?.Invoke();
        public static event Action OnButtonActivate;
        public static void ActivateButton() => OnButtonActivate?.Invoke();
        public static event Action OnButtonDeactivate;
        public static void DeactivateButton() => OnButtonDeactivate?.Invoke();
        public static event Action OnThornDamage;
        public static void TakeThornDamage() => OnThornDamage?.Invoke();
        public static event Action OnSpearDamage;
        public static void TakeSpearDamage() => OnSpearDamage?.Invoke();
        public static event Action<bool> LightLoopPitch;
        public static void ChangeLightLoopPitch(bool increase) => LightLoopPitch.Invoke(increase);



        //Player Footsteps
        public static event Action<DarkElements> OnDarkFootstep;
        public static event Action<LightElements> OnLightFootstep;

        public static void DarkFootstep(DarkElements element) => OnDarkFootstep?.Invoke(element);
        public static void LightFootstep(LightElements element) => OnLightFootstep?.Invoke(element);
        
        //Player Landing

        public static event Action OnDarkLandStone;
        public static event Action OnLightLandStone;

        public static void DarkLandStone() => OnDarkLandStone?.Invoke();
        public static void LightLandStone() => OnLightLandStone?.Invoke();

        public static event Action OnDarkLandWood;
        public static event Action OnLightLandWood;

        public static void DarkLandWood() => OnDarkLandWood?.Invoke();
        public static void LightLandWood() => OnLightLandWood?.Invoke();

        public static event Action OnDarkLandMetal;
        public static event Action OnLightLandMetal;

        public static void DarkLandMetal() => OnDarkLandMetal?.Invoke();
        public static void LightLandMetal() => OnLightLandMetal?.Invoke();
    }
}
