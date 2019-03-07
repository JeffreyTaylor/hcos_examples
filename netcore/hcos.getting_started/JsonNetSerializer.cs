using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace hcos.getting_started
{
    /// <summary>
    /// Special Circumstances ISerializer to be used during RestSharp request/response for serializing object into Json.
    /// </summary>
    public class JsonNetSerializer : ISerializer
    {
        /// <summary>
        /// Setting serialization content type to Json.
        /// </summary>
        string _ContentType = "application/json";
        string ISerializer.ContentType
        {
            get
            {
                return _ContentType;
            }
            set
            {
                _ContentType = value;
            }
        }

        /// <summary>
        /// Json Serialize object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Json serialization</returns>
        string ISerializer.Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }
    }
}
