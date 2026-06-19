using Eclipse.Audio;
using UnityEngine;

namespace Eclipse
{
    public class ShieldItem : MonoBehaviour
    {
        public LayerMask _targetLayer;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && 1 << collision.gameObject.layer == _targetLayer.value)
            {

                collision.gameObject.GetComponent<PlayerMovement>().abilityIsActive = true;

                //enable shield ability
                AudioEvents.PickupShield();

                Destroy(this.gameObject);
            }
        }
    }
}

