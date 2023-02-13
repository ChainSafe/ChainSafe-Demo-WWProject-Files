using Web3Unity.Scripts.Library.Ethers.Contracts;
using Newtonsoft.Json;
using UnityEngine.UI; // needed when accessing text elements
using UnityEngine;
using Web3Unity.Scripts.Library.Ethers.Providers;
using Web3Unity.Scripts.Library.Web3Wallet;

public class ContractMenu : MonoBehaviour
{
    // This script has been moved from the CustomCallExample.cs example in the EVM scripts folder to show you how to make additional changes
    public string chain = "ethereum";
    // set network mainnet, testnet
    public string network = "goerli";
    // abi in json format
    public string contractAbi = "[ { \"inputs\": [ { \"internalType\": \"uint8\", \"name\": \"_myArg\", \"type\": \"uint8\" } ], \"name\": \"addTotal\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"myTotal\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
    // address of contract
    public string contractAddress = "0x741C3F3146304Aaf5200317cbEc0265aB728FE07";
    public GameObject SuccessPopup;
    public Text responseText;

    /*
        //Solidity Contract
        // SPDX-License-Identifier: MIT
        pragma solidity ^0.8.0;

        contract AddTotal {
            uint256 public myTotal = 0;

            function addTotal(uint8 _myArg) public {
                myTotal = myTotal + _myArg;
            }
        }
    */

    async public void CheckVariable()
    {
        string method = "myTotal";
        // you can use this to create a provider for hardcoding and parse this instead of rpc get instance
        //var provider = new JsonRpcProvider(PlayerPrefs.GetString("RPC"));
        var contract = new Contract(contractAbi, contractAddress, RPC.GetInstance.Provider());
        Debug.Log("Contract: " + contract);
        var calldata = await contract.Call(method, new object[]
        {
            // if you need to add parameters you can do so, a call with no args is blank
            // arg1,
            // arg2
        });
        Debug.Log("Contract Data: " + calldata[0]);
        // display response in game
        print("Contract Variable Total: " + calldata[0]);
        responseText.text = "Contract Variable Total: " + calldata[0];
        SuccessPopup.SetActive(true);
    }

    async public void AddOneToVariable()
    {
        string method = "addTotal";
        string amount = "1";
        string chainId = "5"; // goerli
        var contract = new Contract(contractAbi, contractAddress);
        Debug.Log("Contract: " + contract);
        var calldata = contract.Calldata(method, new object[]
        {
            // values need to be converted to integers currently for webwallet builds
            int.Parse(amount)
        });
        Debug.Log("Contract Data: " + calldata);
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, contractAddress, "0", calldata, "", "");
        // display response in game
        print("Please check the contract variable again in a few seconds once the chain has processed the request!");
        responseText.text = "Contract Variable Total: " + response;
        SuccessPopup.SetActive(true);
    }
}
