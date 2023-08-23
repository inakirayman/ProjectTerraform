using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{

    private GameObject _previewObject;
    [SerializeField]
    private Material _previewMaterialPrefab;
    private Material _previewMaterialInstance;

    [SerializeField]
    private LayerMask _detectionLayers;

    public bool Validity { get; private set; }

void Start()
    {
        _previewMaterialInstance = new Material(_previewMaterialPrefab);

    }

    public void StartShowingPlacementPreview(GameObject prefab)
    {
        _previewObject = Instantiate(prefab);
        _previewObject.tag = "Preview";
        PreparePreview(_previewObject);
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = _previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        
        Destroy(_previewObject);
    }

    public void UpdatePosition(Vector3 position)
    {
        
        MovePreview(position);
        CheckValidity();
        ApplyFeedback(Validity);
    }

    private void CheckValidity()
    {

        Collider newObjectCollider = _previewObject.GetComponent<Collider>(); // Assuming the collider is attached to the root of the prefab

        if (newObjectCollider != null)
        {

            Vector3 boundsExtent = newObjectCollider.bounds.extents;


            newObjectCollider.enabled = false;

            Validity  = !Physics.CheckBox(newObjectCollider.bounds.center, boundsExtent, _previewObject.transform.rotation, _detectionLayers);

            newObjectCollider.enabled = true;
        }
        else
        {
            Debug.LogError("Collider not found on the prefab!");
        }
    }

    private void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        _previewMaterialInstance.color = c;
    }

    private void MovePreview(Vector3 position)
    {
        _previewObject.transform.position = position;
    }
}
