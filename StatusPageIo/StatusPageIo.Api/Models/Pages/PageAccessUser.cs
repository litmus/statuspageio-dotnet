using System;
using System.Runtime.Serialization;

namespace StatusPageIo.Api.Models.Pages
{
    [DataContract]
    public class PageAccessUser
    {

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "external_login")]
        public string ExternalLogin { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "page_id")]
        public string PageId { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }

}
