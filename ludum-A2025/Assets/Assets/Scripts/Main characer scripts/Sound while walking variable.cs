using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundwhilewalking : MonoBehaviour

{
    [Header("Audio")]
    public AudioSource footstepAudioSource; // AudioSource component to play footstep sounds
    public AudioClip footstepClip;           // Footstep sound clip
    public float stepDelay = 0.5f;           // Delay between steps

    private float stepTimer;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (footstepAudioSource == null)
        {
            Debug.LogError("Assign an AudioSource component to footstepAudioSource.");
        }
        stepTimer = 0f;
    }

    void Update()
    {
        // Only play footsteps when player is moving on the ground
        if (characterController != null && characterController.isGrounded)
        {
            Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);

            if (horizontalVelocity.magnitude > 0.1f) // Player is walking or running
            {
                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    PlayFootstepSound();
                    stepTimer = stepDelay;
                }
            }
            else
            {
                stepTimer = 0f; // Reset timer if player stops
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer if not grounded
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepClip != null && footstepAudioSource != null)
        {
            footstepAudioSource.PlayOneShot(footstepClip);
        }
    }
}
