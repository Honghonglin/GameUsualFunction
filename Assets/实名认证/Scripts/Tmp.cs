using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Tmp : MonoBehaviour
{
    private const string host = "https://idenauthen.market.alicloudapi.com";
    private const string path = "/idenAuthentication";
    private const string method = "POST";
    //APPcode
    private const string appcode = "你的AppCode";
    public InputField NameText;
    public InputField IdNo;
    //实名认证  简易认证（不安全）
    public void Confirm()
    {
        string querys = "";
        string bodys = string.Format("idNo={0}&name={1}",IdNo.text,NameText.text);
        string url = host + path;
        HttpWebRequest httpRequest = null;
        HttpWebResponse httpResponse = null;
        if (0 < querys.Length)
        {
            url = url + "?" + querys;
        }
        if (host.Contains("https://"))
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
        }
        else
        {
            httpRequest = (HttpWebRequest)WebRequest.Create(url);
        }
        httpRequest.Method = method;
        httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
        httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        if (0 < bodys.Length)
        {
            byte[] data = Encoding.UTF8.GetBytes(bodys);
            using (Stream stream = httpRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }
        try
        {
            httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        }
        catch (WebException ex)
        {
            httpResponse = (HttpWebResponse)ex.Response;
        }
        Debug.Log(httpResponse.StatusCode);
        Debug.Log(httpResponse.Method);
        Debug.Log(httpResponse.Headers);
        Stream st = httpResponse.GetResponseStream();
        StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
        Debug.Log(reader.ReadToEnd());
    }
    public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }
}
