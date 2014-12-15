using System;
using System.Runtime.Serialization;

namespace StatusPageIo.Api.Models.Subscribers
{
    [DataContract]
    public class Subscriber
    {
        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "phone_country")]
        public string PhoneCountry { get; set; }

        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}
