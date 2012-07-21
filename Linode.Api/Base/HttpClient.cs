using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Linode.Api.Base
{
    public class HttpClient<T>
    {
        private Request _request;
        private Action<Response<T>> _responseAction;

        public HttpClient(Request request, Action<Response<T>> responseAction)
        {
            _request = request;
            _responseAction = responseAction;
        }

        public void InvokeGet()
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(_request.GetRequestUrl());
            request.BeginGetResponse(new AsyncCallback(ResponseCallback), request);
        }

        private void ResponseCallback(IAsyncResult asyncResult)
        {
            var request = asyncResult.AsyncState as HttpWebRequest;
            var response = request.EndGetResponse(asyncResult);

            string resultString = null;

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                resultString = sr.ReadToEnd();
            }
            DeserialiseResponse(resultString);
        }

        private void DeserialiseResponse(string json)
        {
            try
            {
                var ret = JsonConvert.DeserializeObject<Response<T>>(json);
                if(_responseAction != null)
                    _responseAction.Invoke(ret);
            }
            catch(Exception e)
            {
                var ret =  new Response<T>
                {
                    Action = _request.Action,
                    Errors = new Error[1] 
                    { 
                        new Error 
                        { 
                            Code = 10000, 
                            Message = "Exception while Deserializing Object: " + e.Message 
                        }
                    }
                };

                if (_responseAction != null)
                    _responseAction.Invoke(ret);
            }
        }
    }
}
