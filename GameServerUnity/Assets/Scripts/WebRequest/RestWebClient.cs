using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestWebClient : Singleton<RestWebClient>
{
    

    private const string defaultContentType = "application/json";
    public IEnumerator HttpGet(string url, System.Action<Response> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                });
            }

            if (webRequest.isDone)
            {
                string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                Debug.Log("Data: " + data);
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = data
                });
            }
        }
    }

    public IEnumerator HttpDelete(string url, System.Action<Response> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }

            if (webRequest.isDone)
            {
                callback(new Response
                {
                    StatusCode = webRequest.responseCode
                });
            }
        }
    }

    public IEnumerator HttpPost(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, body))
        {
            if (headers != null)
            {
                foreach (RequestHeader header in headers)
                {
                    webRequest.SetRequestHeader(header.Key, header.Value);
                }
            }

            webRequest.uploadHandler.contentType = defaultContentType;
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error
                });
            }

            if (webRequest.isDone)
            {
                string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = data
                });
            }
        }
    }

    public IEnumerator HttpPut(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(url, body))
        {
            if (headers != null)
            {
                foreach (RequestHeader header in headers)
                {
                    webRequest.SetRequestHeader(header.Key, header.Value);
                }
            }

            webRequest.uploadHandler.contentType = defaultContentType;
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                });
            }

            if (webRequest.isDone)
            {
                string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Data = data
                });
            }
        }
    }

    public IEnumerator HttpHead(string url, System.Action<Response> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Head(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                });
            }

            if (webRequest.isDone)
            {
                var responseHeaders = webRequest.GetResponseHeaders();
                callback(new Response
                {
                    StatusCode = webRequest.responseCode,
                    Error = webRequest.error,
                    Headers = responseHeaders
                });
            }
        }
    }
}

public class RequestHeader
{
    public string Key { get; set; }

    public string Value { get; set; }
}

public class Response
{
    public long StatusCode { get; set; }

    public string Error { get; set; }

    public string Data { get; set; }

    public Dictionary<string, string> Headers { get; set; }
}

public class Singleton<T> : MonoBehaviour
      where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0)
                    _instance = objs[0];
                if (objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = string.Format("_{0}", typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}