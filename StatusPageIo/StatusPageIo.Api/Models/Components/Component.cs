using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using StatusPageIo.Api.Attributes;
using StatusPageIo.Api.Converters;

namespace StatusPageIo.Api.Models.Components
{
    [DataContract]
    public class Component
    {
        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [MutableProperty]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }

        [MutableProperty]
        [DataMember(Name = "status")]
        [JsonConverter(typeof(PascalCaseEnumConverter))]
        public ComponentStatus Status { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
