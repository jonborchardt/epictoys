using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;

    private void Update(){
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(targetPosition, transform.position) > stoppingDistance){
            Vector3 moveDirection = (targetPosition-transform.position).normalized;
            float moveSpeed = 4;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        if(Input.GetKeyDown(KeyCode.T)) {
            Move(new Vector3(4,0,4));
        }
    }

    void Move(Vector3 targetPosition){
        this.targetPosition = targetPosition;
    }
}
