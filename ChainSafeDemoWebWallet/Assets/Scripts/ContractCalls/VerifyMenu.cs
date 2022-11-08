using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI; // needed when accessing text elements
using System.Text;
using Nethereum.Signer;
using Nethereum.Util;
using UnityEngine;

// This script has been moved from the VerifyExmaple.cs example in the EVM scripts folder to show you how to make additional changes
public class VerifyMenu : MonoBehaviour
{
    public GameObject SuccessPopup;
    public Text responseText;
    public string message = "hello";

    // async public void OnHashMessage()
    // {
    //     try
    //     {
    //         string hashedMessage = await Web3GL.Sha3(message);
    //         Debug.Log("Hashed Message: " + hashedMessage);
    //         string signHashed = await Web3GL.Sign(hashedMessage);
    //         Debug.Log("Signed Hashed: " + signHashed);
    //         ParseSignatureFunction(signHashed);
    //         Task<string> verify = EVM.Verify(hashedMessage, signHashed);
    //         string verifyAddress = await verify;
    //         Debug.Log("Verify Address: " + verifyAddress);
    //         responseText.text = "Verify Address: " + verifyAddress;
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e, this);
    //     }
    // }
    // public void ParseSignatureFunction(string sig)
    // {
    //     string signature = sig;
    //     string r = signature.Substring(0, 66);
    //     Debug.Log("R:" + r);
    //     string s = "0x" + signature.Substring(66, 64);
    //     Debug.Log("S: " + s);
    //     int v = int.Parse(signature.Substring(130, 2), System.Globalization.NumberStyles.HexNumber);
    //     Debug.Log("V: " + v);
    // }

    async public void UserSign()
    {
        string signature = await Web3Wallet.Sign(message);
        //verification
        SignVerifySignature(signature, message);
    }
    
    public void SignVerifySignature(string signatureString, string originalMessage)
    {
        string msg = "\x19" + "Ethereum Signed Message:\n" + originalMessage.Length + originalMessage;
        byte[] msgHash = new Sha3Keccack().CalculateHash(Encoding.UTF8.GetBytes(msg));
        EthECDSASignature signature = MessageSigner.ExtractEcdsaSignature(signatureString);
        EthECKey key = EthECKey.RecoverFromSignature(signature, msgHash);

        bool isValid = key.Verify(msgHash, signature);
        Debug.Log("Address Returned: " + key.GetPublicAddress());
        Debug.Log("Is Valid: " + isValid);
        responseText.text = "Verify Address: " + key.GetPublicAddress();
    }
}