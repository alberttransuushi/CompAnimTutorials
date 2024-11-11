using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManipulationTrigger : MonoBehaviour
{
    public Animator animator;
    public int layerIndex = 1;            // The layer index to modify
    public float transitionDuration = 1f; // Duration of the transition (in seconds)

    private Coroutine weightCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start increasing the layer weight when entering the trigger
            StartWeightChange(1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start decreasing the layer weight when exiting the trigger
            StartWeightChange(0f);
        }
    }

    private void StartWeightChange(float targetWeight)
    {
        // Stop any ongoing weight change coroutine to avoid conflicts
        if (weightCoroutine != null)
        {
            StopCoroutine(weightCoroutine);
        }

        // Start a new coroutine to change the weight over time
        weightCoroutine = StartCoroutine(ChangeLayerWeight(targetWeight));
    }

    private IEnumerator ChangeLayerWeight(float targetWeight)
    {
        float currentWeight = animator.GetLayerWeight(layerIndex);
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float newWeight = Mathf.Lerp(currentWeight, targetWeight, elapsedTime / transitionDuration);
            animator.SetLayerWeight(layerIndex, newWeight);
            yield return null;
        }

        // Ensure the layer weight is set to the exact target weight at the end
        animator.SetLayerWeight(layerIndex, targetWeight);
    }
}
