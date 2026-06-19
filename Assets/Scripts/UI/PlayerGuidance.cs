using Eclipse;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGuidance : MonoBehaviour
{

    public string _input;
    Material _material;
    [SerializeField] private float fadeDuration = 1f;
    GameObject _player;


    private void Update()
    {

        if (_player == null) return;
        if (_player.GetComponentInParent<PlayerInput>().actions[_input].WasPressedThisFrame())
        {
            if (_material != null) return;
            StartCoroutine(FadeGuidance());
            Debug.Log("Player jumped");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
        }
    }

    IEnumerator FadeGuidance()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _material.SetInt("_IsActive", 1);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            _material.SetFloat("_Alpha", alpha);
            yield return null;
        }

        Destroy(gameObject);
    }



}
