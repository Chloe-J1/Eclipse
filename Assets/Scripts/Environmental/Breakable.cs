using UnityEngine;
using UnityEngine.Audio;

namespace Eclipse
{
    public class Breakable : MonoBehaviour
    {
        private AudioSource m_audioSource;
        private int m_audioClipMaxLength = 5;
        private VATController anim_controller;

        private void Start()
        {
            m_audioSource = GetComponent<AudioSource>();

            anim_controller = GetComponent<VATController>();



            //set render bounds to 100 if Object uses VAT
            if (anim_controller != null) 
            {
                Renderer renderer = GetComponent<Renderer>();
                Bounds bounds = renderer.bounds;
                bounds.Expand(100f);
                renderer.localBounds = bounds;
            }


        }

        public void Break()
        {

            m_audioSource.Play();



            if (anim_controller != null)
            {
                anim_controller.enabled = true;

                foreach (var col in GetComponents<Collider>())
                    { col.enabled = false; }
                Invoke(nameof(DisableRenderer), 3f);
            }
            else 
            {
                DisableRenderer(); 
            }

            foreach (var col in GetComponents<Collider>())

                { col.enabled = false; }
                   
            Destroy(gameObject, m_audioClipMaxLength);
          
        }


        private void DisableRenderer()
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

}
