using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private BuildingDatabaseSO _database;
    private int _selectedObjectIndex = -1;

    [SerializeField]
    private PreviewSystem _previewSystem;


    private Vector3 _LastDetectedPosition = Vector3.zero;
    private void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        _selectedObjectIndex = _database.buildingData.FindIndex(data => data.ID == ID);
        if (_selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        _previewSystem.StartShowingPlacementPreview(_database.buildingData[_selectedObjectIndex].Prefab);
        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (_inputManager.IsPointerOverUI())
        {
            return;
        }

        if (_previewSystem.Validity)
        {
            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
            GameObject newObject = Instantiate(_database.buildingData[_selectedObjectIndex].Prefab);
            newObject.transform.position = mousePosition;
            _previewSystem.UpdatePosition(mousePosition);
        }
    }

    private void StopPlacement()
    {
        _previewSystem.StopShowingPreview();
        _selectedObjectIndex = -1;
        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;
        _LastDetectedPosition = Vector3.zero;
    }


    // Update is called once per frame
    void Update()
    {
        if(_selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        if (_LastDetectedPosition != mousePosition)
        {
            _previewSystem.UpdatePosition(mousePosition);
            _LastDetectedPosition = mousePosition;
        }

        



    }





}
