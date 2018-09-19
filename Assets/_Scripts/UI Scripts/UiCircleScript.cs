using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCircleScript : MonoBehaviour {

    //Image circleUi;

    public float rotationSpeed = 35f;

    public Transform playerObject;
    public Transform target;

    void Update() {
        //target.transform.position = playerObject.transform.position;
        target.transform.position = new Vector3(playerObject.transform.position.x,
            playerObject.transform.position.y + 0.2f, playerObject.transform.position.z);


        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }

    //void Start() {
    //    circleUi = GetComponent<Image>();
    //    var tempColor = circleUi.color;
    //    tempColor.a = 0.65f;
    //    circleUi.color = tempColor;
    //}

} //class



































