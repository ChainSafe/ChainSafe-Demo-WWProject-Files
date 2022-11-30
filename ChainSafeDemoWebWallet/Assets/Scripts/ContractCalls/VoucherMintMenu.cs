using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEditor;
using UnityEngine.UI; // needed when accessing text elements
using UnityEngine;

public class VoucherMintMenu : MonoBehaviour
{
    // This script has been moved from the MintWebGL721.cs example in the Minter scripts folder to show you how to make additional changes
    public GameObject SuccessPopup;
    public Text responseText;
    // used for speed bonus on successful mint
    public GameObject Player;
    public GameObject SpeedClouds;
    // abi for reddem contract in json format
    // set chain: ethereum, moonbeam, polygon etc
    public string chain = "ethereum";
    // chain id
    public string chainId = "5";
    // set network mainnet, testnet
    public string network = "goerli";
    // address of nft you want to mint
    public string nftAddress = "0x2c1867bc3026178a47a677513746dcc6822a137a";
    // type
    string type = "1155";

    public async void VoucherMintNFT()
    {
        var voucherResponse1155 = await EVM.Get1155Voucher();
        CreateRedeemVoucherModel.CreateVoucher1155 voucher1155 = new CreateRedeemVoucherModel.CreateVoucher1155();
        voucher1155.tokenId = voucherResponse1155.tokenId;
        voucher1155.minPrice = voucherResponse1155.minPrice;
        voucher1155.signer = voucherResponse1155.signer;
        voucher1155.receiver = voucherResponse1155.receiver;
        voucher1155.amount = voucherResponse1155.amount;
        voucher1155.nonce = voucherResponse1155.nonce;
        voucher1155.signature = voucherResponse1155.signature;
        string voucherArgs = JsonUtility.ToJson(voucher1155);

        // connects to user's browser wallet to call a transaction
        RedeemVoucherTxModel.Response voucherResponse = await EVM.CreateRedeemTransaction(chain, network, voucherArgs, type, nftAddress, voucherResponse1155.receiver);
        string response = await Web3Wallet.SendTransaction(chainId, voucherResponse.tx.to, voucherResponse.tx.value.ToString(), voucherResponse.tx.data, voucherResponse.tx.gasLimit, voucherResponse.tx.gasPrice);
        print("Response: " + response);
        SpeedClouds.SetActive(true);
        Player.GetComponent<PlayerController>().speed = 35;
        responseText.text = "NFT Minted!";
    }
}