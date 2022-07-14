using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChange+=UnitActionSystem_OnSelectedUnitChange;
        UpdateVisual();
    }

    void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty){
        UpdateVisual();
    }

    void UpdateVisual(){
        this.meshRenderer.enabled = UnitActionSystem.Instance.GetSelectedUnit() == unit;
    }
}
