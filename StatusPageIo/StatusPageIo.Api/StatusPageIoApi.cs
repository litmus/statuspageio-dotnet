using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StatusPageIo.Api.Attributes;
using StatusPageIo.Api.Models.Components;
using StatusPageIo.Api.Models.Incidents;
using StatusPageIo.Api.Models.Metrics;
using StatusPageIo.Api.Models.Pages;
using StatusPageIo.Api.Models.Subscribers;

namespace StatusPageIo.Api
{
    public class StatusPageIoApi
    {
        private readonly string OAuthKey;

        private static readonly Uri BaseUri = new Uri("https://api.statuspage.io/v1/"); 

        public StatusPageIoApi(string oAuthKey)
        {
            OAuthKey = oAuthKey;
        }

        public async Task<Page> GetPageProfile(string pageId)
        {
            return await Get<Page>(string.Format("pages/{0}.json", pageId));
        }

        public async Task<Page> UpdatePageProfile(Page page)
        {
            return await Patch<Page>(string.Format("pages/{0}.json", page.Id), page);
        }

        public async Task<IEnumerable<Component>> GetComponents(string pageId)
        {
            return await Get<IEnumerable<Component>>(string.Format("pages/{0}/components.json", pageId));
        }

        public async Task<Component> UpdateComponent(string pageId, Component component)
        {
            return await Patch<Component>(string.Format("pages/{0}/components/{1}.json", pageId, component.Id), component);
        }

        public async Task<IEnumerable<Incident>> GetAllIncidents(string pageId)
        {
            return await Get<IEnumerable<Incident>>(string.Format("pages/{0}/incidents.json", pageId));
        }

        public async Task<IEnumerable<Incident>> GetUnresolvedIncidents(string pageId)
        {
            return await Get<IEnumerable<Incident>>(string.Format("pages/{0}/incidents/unresolved.json", pageId));
        }

        public async Task<IEnumerable<Incident>> GetScheduledIncidents(string pageId)
        {
            return await Get<IEnumerable<Incident>>(string.Format("pages/{0}/incidents/scheduled.json", pageId));
        }

        public async Task<Incident> CreateRealtimeIncident(string pageId, string name, List<string> componentIds = null, string message = null, IncidentStatus status = IncidentStatus.Investigating, bool wantsTwitterUpdate = false, IncidentImpact incidentImpactOverride = IncidentImpact.None)
        {
            var incidentParameters = new Dictionary<string, string>
            {
                {"incident[name]", name},
                {"incident[status]", status.ToString().ToLower()},
                {"incident[wants_twitter_update]", wantsTwitterUpdate ? "t" : "f"},
                {"incident[impact_override]", incidentImpactOverride.ToString().ToLower()}
            };

            if(!string.IsNullOrEmpty(message))
                incidentParameters.Add("incident[message]", message);

            if (componentIds != null)
            {
                foreach (var componentId in componentIds)
                {
                    incidentParameters.Add("incident[component_ids][]", componentId);
                }
            }

            return await Post<Incident>(string.Format("pages/{0}/incidents.json", pageId), incidentParameters);
        }

        public async Task<Incident> DeleteIncident(string pageId, string incidentId)
        {
            return await Delete<Incident>(string.Format("pages/{0}/incidents/{1}.json", pageId, incidentId));
        }

        public async Task<IEnumerable<Subscriber>> GetSubscribers(string pageId)
        {
            return await Get<IEnumerable<Subscriber>>(string.Format("pages/{0}/subscribers.json", pageId));
        }

        public async Task<IEnumerable<Subscriber>> GetSubscribersToIncident(string pageId, string incidentId)
        {
            return await Get<IEnumerable<Subscriber>>(string.Format("pages/{0}/incidents/{1}/subscribers.json", pageId, incidentId));
        }

        public async Task<Subscriber> CreateEmailSubscriber(string pageId, string email, string endpoint = null, bool skipConfirmationNotification = false)
        {
            if(string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Parameter 'email' cannot be null or empty");

            var subscriberParameters = new Dictionary<string, string>
            {
                {"subscriber[email]", email}
            };

            if (!string.IsNullOrEmpty(endpoint))
            {
                Uri myUri;
                if(Uri.TryCreate(endpoint, UriKind.Absolute, out myUri))
                {
                    subscriberParameters.Add("subscriber[endpoint]", endpoint);
                }
                else
                {
                    throw new UriFormatException("Parameter 'endpoint' must be a valid absolute URL"); 
                }
            }

            return await Post<Subscriber>(string.Format("pages/{0}/subscribers.json", pageId), subscriberParameters);
        }

        public async Task<Subscriber> CreateEmailSubscriberForIncident(string pageId, string incidentId, string email, string endpoint = null, bool skipConfirmationNotification = false)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("Parameter 'email' cannot be null or empty");

            var subscriberParameters = new Dictionary<string, string>
            {
                {"subscriber[email]", email}
            };

            if (!string.IsNullOrEmpty(endpoint))
            {
                Uri myUri;
                if (Uri.TryCreate(endpoint, UriKind.Absolute, out myUri))
                {
                    subscriberParameters.Add("subscriber[endpoint]", endpoint);
                }
                else
                {
                    throw new UriFormatException("Parameter 'endpoint' must be a valid absolute URL");
                }
            }

            return await Post<Subscriber>(string.Format("pages/{0}/incidents/{1}/subscribers.json", pageId, incidentId), subscriberParameters);
        }

        public async Task<Subscriber> CreatePhoneSubscriber(string pageId, string phoneNumber, string phoneCountry, bool skipConfirmationNotification = false)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("Parameter 'phoneNumber' cannot be null or empty");

            if (string.IsNullOrEmpty(phoneCountry))
                throw new ArgumentNullException("Parameter 'phoneCountry' cannot be null or empty");

            if(phoneCountry.Length != 2)
                throw new ArgumentException("Parameter 'phoneCountry' must be a two digit country code corresponding to the phone country");

            var subscriberParameters = new Dictionary<string, string>
            {
                {"subscriber[phone_number]", phoneNumber},
                {"subscriber[phone_country]", phoneCountry}
            };

            return await Post<Subscriber>(string.Format("pages/{0}/subscribers.json", pageId), subscriberParameters);
        }

        public async Task<Subscriber> CreatePhoneSubscriberForIncident(string pageId, string incidentId, string phoneNumber, string phoneCountry, bool skipConfirmationNotification = false)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("Parameter 'phoneNumber' cannot be null or empty");

            if (string.IsNullOrEmpty(phoneCountry))
                throw new ArgumentNullException("Parameter 'phoneCountry' cannot be null or empty");

            if (phoneCountry.Length != 2)
                throw new ArgumentException("Parameter 'phoneCountry' must be a two digit country code corresponding to the phone country");

            var subscriberParameters = new Dictionary<string, string>
            {
                {"subscriber[phone_number]", phoneNumber},
                {"subscriber[phone_country]", phoneCountry}
            };

            return await Post<Subscriber>(string.Format("pages/{0}/incidents/{1}/subscribers.json", pageId, incidentId), subscriberParameters);
        }

        public async Task<Subscriber> DeleteSubscriber(string pageId, string subscriberId)
        {
            return await Delete<Subscriber>(string.Format("pages/{0}/subscribers/{1}.json", pageId, subscriberId));
        }

        public async Task<Subscriber> DeleteSubscriberForIncident(string pageId, string subscriberId, string incidentId)
        {
            return await Delete<Subscriber>(string.Format("pages/{0}/incidents/{1}/subscribers/{2}.json", pageId, incidentId, subscriberId));
        }

        public async Task<IEnumerable<MetricProvider>> GetMetricProviders()
        {
            return await Get<IEnumerable<MetricProvider>>("metrics_providers.json");
        }

        public async Task<IEnumerable<LinkedMetricProvider>> GetMetricProvidersForPage(string pageId)
        {
            return await Get<IEnumerable<LinkedMetricProvider>>(string.Format("pages/{0}/metrics_providers.json", pageId));
        }

        public async Task<IEnumerable<Metric>> GetMetricsForMetricProvider(string pageId, string metricId)
        {
            return await Get<IEnumerable<Metric>>(string.Format("pages/{0}/metrics_providers/{1}/metrics.json", pageId, metricId));
        }

        public async Task<Metric> CreateMetric(string pageId, string metricProviderId, string name, string suffix, string tooltipDescription, double yAxisMin, int decimalPlaces, double? yAxisMax = null, bool display = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Parameter 'name' cannot be null or empty");

            if (string.IsNullOrEmpty(suffix))
                throw new ArgumentNullException("Parameter 'suffix' cannot be null or empty");
            
            var subscriberParameters = new Dictionary<string, string>
            {
                {"metric[phone_number]", name},
                {"metric[phone_country]", suffix},
                {"metric[y_axis_min]", yAxisMin.ToString()},
                {"metric[decimal_places]", decimalPlaces.ToString()},
                {"metric[display]", display ? "t" : "f"}
            };

            if (!string.IsNullOrEmpty(tooltipDescription))
            {
                subscriberParameters.Add("metric[tooltip_description]", tooltipDescription);
            }

            if (yAxisMax.HasValue)
            {
                subscriberParameters.Add("metric[y_axis_max]", yAxisMax.Value.ToString());
            }

            return await Post<Metric>(string.Format("pages/{0}/metrics_providers/{1}/metrics.json", pageId, metricProviderId), subscriberParameters);
        }

        public async Task<MetricData> SubmitMetricData(string pageId, string metricId, DateTime timestamp, double value)
        {
            return await SubmitMetricData(pageId, metricId, timestamp, (float) value);
        }

        public async Task<MetricData> SubmitMetricData(string pageId, string metricId, DateTime timestamp, float value)
        {
            var dataParameters = new Dictionary<string, string>
            {
                {"data[timestamp]", ConvertToTimestamp(timestamp).ToString()},
                {"data[value]", value.ToString()}
            };

            return await Post<MetricData>(string.Format("pages/{0}/metrics/{1}/data.json", pageId, metricId), dataParameters);
        }

        public async Task<Metric> DeleteMetricData(string pageId, string metricId)
        {
            return await Delete<Metric>(string.Format("pages/{0}/metrics/{1}/data.json", pageId, metricId));
        }

        /* Private Methods */

        private async Task<T> Get<T>(string relativeUri)
        {
            string response;
            var req = WebRequest.Create(new Uri(BaseUri, relativeUri));
            req.Headers["Authorization"] = "OAuth " + OAuthKey;
            using (var res = await req.GetResponseAsync())
            {
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }
            return JsonConvert.DeserializeObject<T>(response);
        }

        private long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }

        private async Task<T> Post<T>(string relativeUri, Dictionary<string, string> body)
        {
            var response = await SendData(relativeUri, body, "POST");
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<T> Patch<T>(string relativeUri, object body)
        {
            var response = await SendData(relativeUri, body, "PATCH");
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<T> Delete<T>(string relativeUri)
        {
            string response;
            var req = WebRequest.Create(new Uri(BaseUri, relativeUri));
            req.Headers["Authorization"] = "OAuth " + OAuthKey;
            req.Method = "DELETE";
            using (var res = await req.GetResponseAsync())
            {
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }
            return JsonConvert.DeserializeObject<T>(response);
        }

        private async Task<string> SendData(string relativeUri, object body, string verb)
        {
            var requestBody = ExtractMutablePropertiesFromObject(body);
            return await SendData(relativeUri, requestBody, verb);
        }

        private async Task<string> SendData(string relativeUri, Dictionary<string, string> body, string verb)
        {
            var req = WebRequest.Create(new Uri(BaseUri, relativeUri));
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers["Authorization"] = "OAuth " + OAuthKey;
            req.Method = verb;
            
            using (var writer = new StreamWriter(await req.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(string.Join("&", body.Select(p => string.Format("{0}={1}", p.Key, WebUtility.UrlEncode(p.Value)))));
            }
            using (var res = await req.GetResponseAsync())
            {
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        /// <summary>
        /// Use reflection to find editable fields on a model object.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private Dictionary<string, string> ExtractMutablePropertiesFromObject(object body)
        {
            var returnDictionary = new Dictionary<string, string>();
            
            var properties = body.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(MutablePropertyAttribute), false));
            foreach (var propertyInfo in properties)
            {
                var dataMemberName = ((DataMemberAttribute)propertyInfo.GetCustomAttributes(typeof (DataMemberAttribute), true).First()).Name;
                var className = body.GetType().Name.ToLower();

                var parameter = string.Format("{0}[{1}]", className, dataMemberName);
                if (propertyInfo.GetValue(body) != null)
                {
                    switch (propertyInfo.PropertyType.Name)
                    {
                        case "Boolean":
                            returnDictionary.Add(parameter, (bool)propertyInfo.GetValue(body) ? "t" : "f");
                            break;
                        case "ComponentStatus":
                        case "IncidentStatus":
                            string result = string.Concat(propertyInfo.GetValue(body).ToString().Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
                            returnDictionary.Add(parameter, result);
                            break;
                        default:
                            returnDictionary.Add(parameter, propertyInfo.GetValue(body).ToString());
                            break;
                    }
                    
                }
            }

            return returnDictionary;
        }

    }
}
