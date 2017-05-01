using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using System.Security.Cryptography;

public class Best : MonoBehaviour {
	public Text bestText;
	private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };  
	string path;
	/// <summary>  
	/// DES加密字符串  
	/// </summary>  
	/// <param name="encryptString">待加密的字符串</param>  
	/// <param name="encryptKey">加密密钥,要求为8位</param>  
	/// <returns>加密成功返回加密后的字符串，失败返回源串</returns>  
	public static string EncryptDES(string encryptString, string encryptKey)  
	{  
		try  
		{  
			byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));  
			byte[] rgbIV = Keys;  
			byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);  
			DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();  
			MemoryStream mStream = new MemoryStream();  
			CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);  
			cStream.Write(inputByteArray, 0, inputByteArray.Length);  
			cStream.FlushFinalBlock();  
			cStream.Close();  
			return Convert.ToBase64String(mStream.ToArray());  
		}  
		catch  
		{  
			return encryptString;  
		}  
	}  

	/// <summary>  
	/// DES解密字符串  
	/// </summary>  
	/// <param name="decryptString">待解密的字符串</param>  
	/// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>  
	/// <returns>解密成功返回解密后的字符串，失败返源串</returns>  
	public static string DecryptDES(string decryptString, string decryptKey)  
	{  
		try  
		{  
			byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));  
			byte[] rgbIV = Keys;  
			byte[] inputByteArray = Convert.FromBase64String(decryptString);  
			DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();  
			MemoryStream mStream = new MemoryStream();  
			CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);  
			cStream.Write(inputByteArray, 0, inputByteArray.Length);  
			cStream.FlushFinalBlock();  
			cStream.Close();  
			return Encoding.UTF8.GetString(mStream.ToArray());  
		}  
		catch  
		{  
			Debug.Log("catch");  
			return decryptString;  
		}  
	}  
	// Use this for initialization
	void Start () {
		path=Application.persistentDataPath+"\\";
		StreamWriter sw;
		FileInfo t = new FileInfo(path+"game.dat");
		if (!t.Exists) {//if file exits,read and display;
			sw = t.CreateText ();
			string tmp = "000";
			string tmpDES = EncryptDES (tmp, "asdasfsfa");
			bestText.text = tmp;
			sw.WriteLine (tmpDES);
			sw.Close ();
			sw.Dispose ();
		} else {
			StreamReader sr = null;
			try{
				sr=File.OpenText(path+"game.dat");	
			}catch(Exception e){
				Debug.Log("cant open file");
			}
			string tmpDES = sr.ReadLine ();
			string tmp = DecryptDES (tmpDES, "asdasfsfa");
			bestText.text = tmp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
