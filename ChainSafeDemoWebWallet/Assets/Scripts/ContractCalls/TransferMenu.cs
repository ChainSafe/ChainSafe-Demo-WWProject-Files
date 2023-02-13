using System;
using Web3Unity.Scripts.Library.Ethers.Contracts;
using UnityEngine;
using UnityEngine.UI; // needed when accessing text elements
using Newtonsoft.Json;
using Web3Unity.Scripts.Library.Web3Wallet; // used for json serialization with args

public class TransferMenu: MonoBehaviour
{
    // This script has been moved from the WebGLTransfer20.cs example in the WebGL scripts folder to show you how to make additional changes
    public GameObject SuccessPopup;
    public Text responseText;
    public string chainId = "5"; // goerli
    public string contractAddress = "0xed7f68Ed23bB75841ab1448A95fa19aA46e9EA3E";
    public string toAccount = "0xdA064B1Cef52e19caFF22ae2Cc1A4e8873B8bAB0";
    public string amount = "1000000000000000000";
    public string contractAbi = "[ { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"_to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"_value\", \"type\": \"uint256\" } ], \"name\": \"mint\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"_to\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"_value\", \"type\": \"uint256\" } ], \"name\": \"transfer\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"success\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"constructor\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"_from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"_to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"_value\", \"type\": \"uint256\" } ], \"name\": \"Transfer\", \"type\": \"event\" }, { \"inputs\": [], \"name\": \"_decimal\", \"outputs\": [ { \"internalType\": \"uint8\", \"name\": \"\", \"type\": \"uint8\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"_name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"_symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"_totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"address\", \"name\": \"_owner\", \"type\": \"address\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"balance\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"decimals\", \"outputs\": [ { \"internalType\": \"uint8\", \"name\": \"\", \"type\": \"uint8\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
    
    async public void Transfer()
    {
        // smart contract method to call
        string method = "transfer";
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to send a transaction
        try {
            // connects to user's browser wallet to call a transaction
            var contract = new Contract(contractAbi, contractAddress);
            Debug.Log("Contract: " + contract);
            var calldata = contract.Calldata(method, new object[]
            {
                toAccount,
                amount
            });
            Debug.Log("Contract Data: " + calldata);
            // send transaction
            string response = await Web3Wallet.SendTransaction(chainId, contractAddress, value, calldata, gasLimit, gasPrice);
            Debug.Log(response);
            responseText.text = response;
            SuccessPopup.SetActive(true);
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }

    async public void Mint()
    {
        string account = PlayerPrefs.GetString("Account");
        // smart contract method to call
        string method = "mint";
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to send a transaction
        try {
            var contract = new Contract(contractAbi, contractAddress);
            Debug.Log("Contract: " + contract);
            var calldata = contract.Calldata(method, new object[]
            {
                account,
                amount
            });
            Debug.Log("Contract Data: " + calldata);
            string response = await Web3Wallet.SendTransaction(chainId, contractAddress, value, calldata, gasLimit, gasPrice);
            Debug.Log(response);
            responseText.text = response;
            SuccessPopup.SetActive(true);
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }
}