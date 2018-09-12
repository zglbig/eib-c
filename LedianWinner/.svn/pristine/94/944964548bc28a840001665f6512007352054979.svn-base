
using LitJson;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace org.zgl
{
    public class HttpSocketImpl : MonoBehaviour
    {
        private static HttpSocketImpl instance;
        public static HttpSocketImpl getInstance()
        {

            if (instance == null)
            {
                GameObject go = new GameObject("HttpManager");
                DontDestroyOnLoad(go);
                instance = go.AddComponent<HttpSocketImpl>();
            }
            return instance;

        }
        public HttpIoMessage sync(ClientHttpRequsetIoMessage body, short gameId)
        {
            HttpIoMessage resultMessage = HttpPostsSync("http://192.168.1.101:8090/handler", body, gameId);
            return resultMessage;
          
        }
        private void RequestHttpServer(string url, ClientHttpRequsetIoMessage body, HttpIoMessage callback, short gameId)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.ProtocolVersion = HttpVersion.Version11;
            //request.AutomaticDecompression = DecompressionMethods.None;
            //request.Method = "POST";
            //request.ContentType = "application/octet-stream; charset=UTF-8";
            //request.Timeout = 60 * 1000;
            //request.ReadWriteTimeout = 60 * 1000;
            //request.KeepAlive = true;
            //request.Proxy = null;
            //ByteArray byteArray = new ByteArray();
            //byteArray.WriteByte(19);
            //byteArray.WriteShort(1024);
            //byteArray.WriteInt(5);
            //byteArray.WriteShort(gameId);
            //byte[] bytes = ProtostuffUtils.ProtobufSerialize(body);
            //byteArray.WriteInt(bytes.Length);
            //byteArray.WriteBytes(bytes);
            //request.ContentLength = byteArray.Length;
            //request.GetRequestStream().Write(byteArray.Buffer, 0, byteArray.Length);
            //request.GetRequestStream().Close();
            //try
            //{

            //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //    Debug.Log(response.ContentLength);
            //    Stream stream = response.GetResponseStream();
            //    int length = (int)response.ContentLength;

            //    byte[] data = new byte[length];
            //    int mix = 0;
            //    while (mix < length)
            //    {
            //        int _len = stream.Read(data, mix, data.Length);
            //        mix += _len;
            //    }
            //    ByteArray byteArray1 = new ByteArray();
            //    byteArray.WriteBytes(data);
            //    int dataLength = byteArray.ReadInt();
            //    byte[] resultData = byteArray.ReadBytes();
            //    if (dataLength != resultData.Length)
            //    {
            //        return;
            //    }
            //    try
            //    {
            //        callback = ProtostuffUtils.ProtobufDeserialize<HttpIoMessage>(resultData);
            //    }
            //    catch (Exception e)
            //    {
            //        Debug.LogError(e.StackTrace);
            //    }

            //}
            //catch (Exception e)
            //{
            //    Debug.Log(e);
            //}

            //StartCoroutine(HttpPostsSync(url, body, callback, gameId));

        }

        public HttpIoMessage HttpPostsSync(string url, ClientHttpRequsetIoMessage body, short gameId)
        {

            Debug.Log("发送"+ body.interfaceName + "::" + body.methodName + "====" + body.args);
            var request = new UnityWebRequest(url, "POST");
            ByteArray byteArray = new ByteArray();
            byteArray.WriteByte(19);
            byteArray.WriteShort(1024);
            byteArray.WriteInt(5);
            byteArray.WriteShort(gameId);
            byte[] bytes = ProtostuffUtils.ProtobufSerialize(body);
            byteArray.WriteInt(bytes.Length);
            byteArray.WriteBytes(bytes);
            request.timeout = 5 * 1000;
            request.uploadHandler = new UploadHandlerRaw(byteArray.Buffer);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/octet-stream");
            request.SendWebRequest();
            while (true) {
                if (request.isDone) {
                    break;
                }
                if (request.isHttpError) {
                    break;
                }
                if (request.isNetworkError) {
                    break;
                }
            }
            if (request.responseCode == 200)
            {
                ByteArray byteArray1 = new ByteArray();
                byteArray1.WriteBytes(request.downloadHandler.data);
                int dataLength = byteArray1.ReadInt();
                byte[] resultData = byteArray1.ReadBytes();
                if (dataLength != resultData.Length)
                {
                    //yield return null;
                }
                try
                {
                    return ProtostuffUtils.ProtobufDeserialize<HttpIoMessage>(resultData);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.StackTrace);
                }
            }
            return null;
        }
    }
}
