using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI; // needed when accessing text elements
using System.Text;
using Nethereum.Signer;
using Nethereum.Util;
using UnityEngine;
using Web3Unity.Scripts.Library.Web3Wallet;

// This script has been moved from the VerifyExmaple.cs example in the EVM scripts folder to show you how to make additional changes
public class VerifyMenu : MonoBehaviour
{
    public GameObject SuccessPopup;
    public Text responseText;
    public string message = "hello";

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