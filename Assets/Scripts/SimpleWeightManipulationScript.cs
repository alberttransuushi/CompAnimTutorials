using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeightManipulationScript : MonoBehaviour
{
    public Animator animator;
    public int layerIndex = 1; // The index of the layer you want to modify
    public float weightChangeSpeed = 0.1f; // Speed at which to change the weight

    private void Update()
    {
        // Increase the layer weight
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float newWeight = Mathf.Clamp(animator.GetLayerWeight(layerIndex) + weightChangeSpeed * Time.deltaTime, 0f, 1f);
            animator.SetLayerWeight(layerIndex, newWeight);
        }

        // Decrease the layer weight
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float newWeight = Mathf.Clamp(animator.GetLayerWeight(layerIndex) - weightChangeSpeed * Time.deltaTime, 0f, 1f);
            animator.SetLayerWeight(layerIndex, newWeight);
        }
    }
}
