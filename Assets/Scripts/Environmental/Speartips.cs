using Eclipse.Audio;
using UnityEngine;
using UnityEngine.Audio;
namespace Eclipse
{
    public class Speartips : MonoBehaviour, IDamageable, IScreenShake
    {
        const int m_nrOfDamage = 1;

        public int Attack()
        {
            AudioEvents.TakeSpearDamage();
            return m_nrOfDamage;
        }
    }
}
    
