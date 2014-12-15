using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StatusPageIo.Api.Models.Metrics
{
    [DataContract]
    public class Metric
    {
        [DataMember(Name = "application_id")]
        public string ApplicationId { get; set; }

        [DataMember(Name = "application_name")]
        public string ApplicationName { get; set; }

        [DataMember(Name = "backfill_percentage")]
        public double BackfillPercentage { get; set; }

        [DataMember(Name = "backfilled")]
        public bool Backfilled { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "decimal_places")]
        public int DecimalPlaces { get; set; }

        [DataMember(Name = "display")]
        public bool Display { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "last_fetched_at")]
        public DateTime LastFetchedAt { get; set; }

        [DataMember(Name = "metric_identifier")]
        public string MetricIdentifier { get; set; }

        [DataMember(Name = "metrics_provider_id")]
        public string MetricsProviderId { get; set; }

        [DataMember(Name = "most_recent_data_at")]
        public DateTime MostRecentDataAt { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "suffix")]
        public string Suffix { get; set; }

        [DataMember(Name = "tooltip_description")]
        public string TooltipDescription { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "y_axis_max")]
        public double? YAxisMax { get; set; }

        [DataMember(Name = "y_axis_min")]
        public double YAxisMin { get; set; }
    }

}
