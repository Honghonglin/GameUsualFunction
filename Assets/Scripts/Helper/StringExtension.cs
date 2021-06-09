using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class StringExtension
{
    //对字符串MD5加密
    public static string md5(this string source)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //转换为UTF8编码的字节数组
        byte[] data = Encoding.UTF8.GetBytes(source);
        //计算哈希值
        byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
        md5.Clear();

        string destString = "";
        for (int i = 0; i < md5Data.Length; i++)
        {
            //转为16进制的字符串
            Debug.Log(System.Convert.ToString(md5Data[i], 16));
            //长度为2,不够用0填充，左填充
            destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
        }
        destString = destString.PadLeft(32, '0');
        return destString;
    }
}
