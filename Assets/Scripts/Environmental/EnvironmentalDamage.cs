using System.Collections;
using UnityEngine;

namespace Eclipse
{
    public class EnvironmentalDamage : MonoBehaviour
    {

        public enum DamageSource
        {
            thorns,
            ExplodingPlant,
            PoisonPool,
            SwingingAxes,
            FallingRocks,
            SpearTips

        }

        public DamageSource damageSource;

        [SerializeField] int launchForce = 15;

        bool canDamage = true;


        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Player") && canDamage)
            {

                StartCoroutine(DamagePlayer(collision));


                // Apply damage logic based on the damage source

                /*switch (damageSource)
                {
                    case DamageSource.thorns:
                        // Apply damage logic for thorns
                        StartCoroutine(DamagePlayer(collision));
                        break;
                    case DamageSource.ExplodingPlant:
                        // Apply damage logic for exploding plant
                        StartCoroutine(DamagePlayer(collision));
                        break;
                    case DamageSource.PoisonPool:
                        // Apply damage logic for poison pool
                        StartCoroutine(DamagePlayer(collision));
                        break;
                    case DamageSource.SwingingAxes:
                        // Apply damage logic for swinging axes
                        StartCoroutine(DamagePlayer(collision));
                        break;
                    case DamageSource.FallingRocks:
                        // Apply damage logic for falling rocks
                        StartCoroutine(DamagePlayer(collision));
                        break;
                    case DamageSource.SpearTips:
                        // Apply damage logic for spear tips
                        StartCoroutine(DamagePlayer(collision));
                        break;
                }*/

            }

            //put switch statement here to determine which damage source is being used and apply different damage values and logic based on the source



        }


        IEnumerator DamagePlayer(Collision col)
        {
            canDamage = false;
            //PlayerHealth.DamagePlayer();

            col.gameObject.GetComponent<Rigidbody>().AddForce(-col.GetContact(0).normal * launchForce, ForceMode.Impulse); // Adjust the force and direction as needed

            col.gameObject.GetComponent<PlayerMovement>().CanMove = false;


            Debug.Log(canDamage);

            yield return new WaitForSeconds(.5f); // Adjust the cooldown duration as needed

            col.gameObject.GetComponent<PlayerMovement>().CanMove = true;

            canDamage = true;
            Debug.Log(canDamage);

        }



    }
}

