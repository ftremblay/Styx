using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiOpacityScript : MonoBehaviour {

    Image circleUi;

    private float currentOpacityValue;

    private float inactivityCounter;

    private Animator anim;

    //private bool opacityCoroutinePlayed;

    void Awake() {
        currentOpacityValue = 1.0f;

        inactivityCounter = 0.0f;

        anim = GetComponent<Animator>();

        circleUi = GetComponent<Image>();
        var tempColor = circleUi.color;
        tempColor.a = currentOpacityValue;
        circleUi.color = tempColor;
    }

    void Update() {

        circleUi = GetComponent<Image>();
        var tempColor = circleUi.color;
        tempColor.a = currentOpacityValue;
        circleUi.color = tempColor;

        if (inactivityCounter < 5f) {
            if ((Input.anyKeyDown) || (Input.GetKeyDown(KeyCode.UpArrow))
                || (Input.GetButtonDown("Pass"))) {
                inactivityCounter = 0.0f;

            } else {
                inactivityCounter += Time.deltaTime;
            }

        } if (inactivityCounter >= 5f) {
            if (((Input.anyKeyDown) || (Input.GetKeyDown(KeyCode.UpArrow)) 
                || (Input.GetButtonDown("Pass")))) {

                inactivityCounter = 0.0f;

                anim.Play("UIIdleStart");

            } else {

                anim.Play("UIFadingOut");

            }
        }
    }

    //public IEnumerator OpacityUp(float lowOpacity, float newOpacity, float duration) {
    //    for (float t = 0f; t < duration; t += Time.deltaTime) {
    //        currentOpacityValue = Mathf.Lerp(0.1f, 1f, t / 0.2f);
    //        opacityCoroutinePlayed = false;
    //        yield return null;

    //    }

    //}

    //public IEnumerator OpacityDown(float highOpacity, float newOpacity, float duration) {
    //    for (float t = 0f; t < duration; t += Time.deltaTime) {
    //        currentOpacityValue = Mathf.Lerp(1, 0.1f, t / 1.5f);
    //        opacityCoroutinePlayed = true;
    //        yield return null;

    //    }

    //}

} //class
































