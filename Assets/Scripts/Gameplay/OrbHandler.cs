using Eclipse;
using Eclipse.Audio;
using System;
using System.Collections;
using UnityEngine;

namespace Eclipse
{
    public class OrbHandler : MonoBehaviour
    {
        public LayerMask m_lightLayer;
        public EventHandler m_pickedUp;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && 1 << collision.gameObject.layer == m_lightLayer.value)
            {
                collision.gameObject.GetComponent<PlayerMovement>().abilityIsActive = true;
                AudioEvents.PickupOrb();
                m_pickedUp.Invoke(this, EventArgs.Empty);

                //enable in playerscript the ability to cast lightbeam
                StartCoroutine(DestroyOnDelay());
            }
        }

        IEnumerator DestroyOnDelay()
        {
            yield return new WaitForSeconds(.01f);
            Destroy(this.gameObject);
        }
    }

}
