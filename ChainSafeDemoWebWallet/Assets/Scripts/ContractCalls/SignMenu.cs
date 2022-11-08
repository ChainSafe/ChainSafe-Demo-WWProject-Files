using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // needed when accessing text elements
using UnityEngine;

public class SignMenu : MonoBehaviour
{
    // This script has been moved from the WebGLSignMessageExmaple.cs example in the WebGL scripts folder to show you how to make additional changes
    public GameObject SuccessPopup;
    public Text responseText;
    public string message = "This is a test message to sign";

    async public void OnSignMessage()
    {
        try {
            string response = await Web3Wallet.Sign(message);
            print(response);
            responseText.text = "Signature: " + response;
            SuccessPopup.SetActive(true);
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }
}