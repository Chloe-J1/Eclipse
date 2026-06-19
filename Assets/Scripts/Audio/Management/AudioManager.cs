
using UnityEngine;
namespace Eclipse.Audio
{

    public class AudioManager : MonoBehaviour
    {
        [Header("Dark Player Audio")]
        [SerializeField] private MultiLayerAudioDark _darkAudio;
        [SerializeField] private MultiLayerAudioDark _darkFootsteps;
        [SerializeField] private AudioSource _darkOneShotSource;
        [SerializeField] private AudioSource _darkFootstepSource;
        [SerializeField] private AudioSource _darkLoopSource;

        [Header("Light Player Audio")]
        [SerializeField] private MultiLayerAudioLight _lightAudio;
        [SerializeField] private MultiLayerAudioLight _lightFootsteps;
        [SerializeField] private AudioSource _lightOneShotSource;
        [SerializeField] private AudioSource _lightFootstepSource;
        [SerializeField] private AudioSource _lightLoopSource;

        [Header("Shared Audio")]
        [SerializeField] private MultiLayerAudioShared _sharedAudio;
        [SerializeField] private AudioSource _sharedAudioSource;


        private void OnEnable()
        {
            AudioEvents.OnJump += HandleJump;
            AudioEvents.OnDarkFootstep += HandleDarkFootstep;
            AudioEvents.OnLightFootstep += HandleLightFootstep;
            AudioEvents.OnDarkLandStone += HandleDarkLandStone;
            AudioEvents.OnLightLandStone += HandleLightLandStone;
            AudioEvents.OnDarkLandWood += HandleDarkLandWood;
            AudioEvents.OnLightLandWood += HandleLightLandWood;
            AudioEvents.OnDarkLandMetal += HandleDarkLandMetal;
            AudioEvents.OnLightLandMetal += HandleLightLandMetal;
            AudioEvents.OnDash += HandleDash;
            AudioEvents.OnLightBeamStart += HandleLightBeamStart;
            AudioEvents.OnLightBeamEnd += HandleLightBeamEnd;
            AudioEvents.OnShieldEquip += HandleShieldEquip;
            AudioEvents.OnShieldStow += HandleShieldStow;
            AudioEvents.OnOrbPickup += HandleOrbPickup;
            AudioEvents.OnShieldPickup += HandleShieldPickup;
            AudioEvents.OnGateOpen += HandleGateOpen;
            AudioEvents.OnGateClose += HandleGateClose;
            AudioEvents.OnButtonActivate += HandleButtonActivate;
            AudioEvents.OnButtonDeactivate += HandleButtonDeactivate;
            AudioEvents.OnThornDamage += HandleThornDamage;
            AudioEvents.OnSpearDamage += HandleSpearDamage;
            AudioEvents.LightLoopPitch += HandleLightLoopChange;
            AudioEvents.OnDoorOpen += HandleDoorOpen;
            AudioEvents.OnJumpVoice += HandleJumpVoice;
            AudioEvents.OnDashVoice += HandleDashVoice;
            AudioEvents.OnPainVoice += HandlePainVoice;
            AudioEvents.OnDeathVoice += HandleDeathVoice;
        }
        
        private void OnDisable()
        {
            AudioEvents.OnJump -= HandleJump;
            AudioEvents.OnDarkFootstep -= HandleDarkFootstep;
            AudioEvents.OnLightFootstep -= HandleLightFootstep;
            AudioEvents.OnDash -= HandleDash;
            AudioEvents.OnLightBeamStart -= HandleLightBeamStart;
            AudioEvents.OnLightBeamEnd -= HandleLightBeamEnd;
            AudioEvents.OnShieldEquip -= HandleShieldEquip;
            AudioEvents.OnShieldStow -= HandleShieldStow;
            AudioEvents.OnOrbPickup -= HandleOrbPickup;
            AudioEvents.OnShieldPickup -= HandleShieldPickup;
            AudioEvents.OnGateOpen -= HandleGateOpen;
            AudioEvents.OnGateClose -= HandleGateClose;
            AudioEvents.OnButtonActivate -= HandleButtonActivate;
            AudioEvents.OnButtonDeactivate -= HandleButtonDeactivate;
            AudioEvents.OnThornDamage -= HandleThornDamage;
            AudioEvents.OnSpearDamage -= HandleSpearDamage;
            AudioEvents.OnDarkLandStone -= HandleDarkLandStone;
            AudioEvents.OnLightLandStone -= HandleLightLandStone;
            AudioEvents.OnDarkLandWood -= HandleDarkLandWood;
            AudioEvents.OnLightLandWood -= HandleLightLandWood;
            AudioEvents.OnDarkLandMetal -= HandleDarkLandMetal;
            AudioEvents.OnLightLandMetal -= HandleLightLandMetal;
            AudioEvents.LightLoopPitch -= HandleLightLoopChange;
            AudioEvents.OnDoorOpen -= HandleDoorOpen;
            AudioEvents.OnJumpVoice -= HandleJumpVoice;
            AudioEvents.OnDashVoice -= HandleDashVoice;
            AudioEvents.OnPainVoice -= HandlePainVoice;
            AudioEvents.OnDeathVoice -= HandleDeathVoice;



        }

        private void HandleJump(PlayerType playerType)
        {
            if (playerType == PlayerType.Dark)
            {
                _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.jump);
            }
            else
            {
                _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.jump);
            }
        }

        private void HandleJumpVoice(PlayerType playerType)
        {
            if (playerType == PlayerType.Dark)
            {
                _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.jumpVoice);
            }
            else
            {
                _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.jumpVoice);
            }
        }
        private void HandleDashVoice(PlayerType playerType)
        {
            if (playerType == PlayerType.Dark)
            {
                _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.dashVoice);
            }
            else
            {
                _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.dashVoice);
            }
        }
        private void HandlePainVoice(PlayerType playerType)
        {
            if (playerType == PlayerType.Dark)
            {
                _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.painVoice);
            }
            else
            {
                _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.painVoice);
            }
        }
        private void HandleDeathVoice(PlayerType playerType)
        {
            if (playerType == PlayerType.Dark)
            {
                _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.deathVoice);
            }
            else
            {
                _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.deathVoice);
            }
        }
        private void HandleDarkFootstep(DarkElements element)
        {
            _darkFootsteps.PlayContainerElement(_darkFootstepSource, element);
        }

        private void HandleLightFootstep(LightElements element)
        {
            _lightFootsteps.PlayContainerElement(_lightFootstepSource, element);
        }

        private void HandleDarkLandStone()
        {
            _darkAudio.PlayContainerElement(_darkOneShotSource,DarkElements.stoneLand);
        }

        private void HandleLightLandStone()
        {
            _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.stoneLand);
          
        }
        private void HandleDarkLandWood()
        {
            _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.woodLand);
        }

        private void HandleLightLandWood()
        {
            _lightAudio.PlayContainerElement(_lightFootstepSource, LightElements.woodLand);
        }

        private void HandleDarkLandMetal()
        {
            _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.metalLand);
        }

        private void HandleLightLandMetal()
        {
            _lightAudio.PlayContainerElement(_lightFootstepSource, LightElements.metalLand);
        }

        private void HandleDash(PlayerType playertype)
        {
            if(playertype == PlayerType.Light)
            {
                _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.dash);
            }
            else
            {
                _darkAudio.PlayContainerElement(_lightOneShotSource, DarkElements.dash);
            }
            
        }

        private void HandleLightBeamStart()
        {
            _lightAudio.PlayContainerElement(_lightLoopSource, LightElements.lightBeamLoop,true);
        }

        private void HandleLightBeamEnd()
        {
            _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.lightBeamEnd);
            _lightAudio.FadeOutAndStop(_lightLoopSource, this);
        }

        private void HandleShieldEquip()
        {
            _darkAudio.PlayContainerElement(_darkLoopSource, DarkElements.shieldLoop, true);
        }

        private void HandleShieldStow()
        {
            _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.shieldStow);
            _darkAudio.FadeOutAndStop(_darkLoopSource, this);
        }

        private void HandleOrbPickup()
        {
            _lightAudio.PlayContainerElement(_lightOneShotSource, LightElements.orbPickup);
        }
        private void HandleShieldPickup()
        {
            _darkAudio.PlayContainerElement(_darkOneShotSource, DarkElements.shieldPickup);
        }

        private void HandleGateOpen()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.gateOpen);
        }
        private void HandleDoorOpen()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.doorOpen);
        }
        private void HandleGateClose()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.gateClose);
        }
        private void HandleButtonActivate()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.buttonActivate);
        }
        private void HandleButtonDeactivate()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.buttonDeactivate);
        }
        private void HandleThornDamage()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.thornDamage);
        }
        private void HandleSpearDamage()
        {
            _sharedAudio.PlayContainerElement(_sharedAudioSource, SharedElements.spearDamage);
        }
        private void HandleLightLoopChange(bool increase)
        {
            if(increase)
            {
                _lightLoopSource.pitch = 1.1f;
            }
            else
            {
                _lightLoopSource.pitch = 1.0f;
            }
        }
    }
}

