using System;
using System.Runtime.Serialization;
using StatusPageIo.Api.Attributes;

namespace StatusPageIo.Api.Models.Pages
{
    [DataContract]
    public class Page
    {
        [MutableProperty]
        [DataMember(Name = "allow_email_subscribers")]
        public bool AllowEmailSubscribers { get; set; }

        [MutableProperty]
        [DataMember(Name = "allow_incident_subscribers")]
        public bool AllowIncidentSubscribers { get; set; }

        [MutableProperty]
        [DataMember(Name = "allow_page_subscribers")]
        public bool AllowPageSubscribers { get; set; }

        [MutableProperty]
        [DataMember(Name = "allow_sms_subscribers")]
        public bool AllowSmsSubscribers { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_body_background_color")]
        public string CssBodyBackgroundColor { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_font_color")]
        public string CssFontColor { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_greens")]
        public string CssGreens { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_light_font_color")]
        public string CssLightFontColor { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_oranges")]
        public string CssOranges { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_reds")]
        public string CssReds { get; set; }

        [MutableProperty]
        [DataMember(Name = "css_yellows")]
        public string CssYellows { get; set; }

        [MutableProperty]
        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        [DataMember(Name = "hero_cover")]
        public HeroCover HeroCover { get; set; }

        [DataMember(Name = "hidden_from_search")]
        public bool HiddenFromSearch { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [MutableProperty]
        [DataMember(Name = "layout")]
        public string Layout { get; set; }

        /// <summary>
        /// *MUTABLE* 
        /// </summary>
        [MutableProperty]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [MutableProperty]
        [DataMember(Name = "notifications_from_email")]
        public string NotificationsFromEmail { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [MutableProperty]
        [DataMember(Name = "subdomain")]
        public string Subdomain { get; set; }

        [DataMember(Name = "time_zone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "transactional_logo")]
        public TransactionalLogo TransactionalLogo { get; set; }

        [DataMember(Name = "twitter_username")]
        public string TwitterUsername { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [MutableProperty]
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }

}
