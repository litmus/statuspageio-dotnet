using System.Linq;
using NUnit.Framework;
using StatusPageIo.Api;
using StatusPageIo.Api.Models.Components;
using StatusPageIo.Api.Models.Incidents;
using System.Threading.Tasks;

namespace StatusPageIo.UnitTests
{
    [TestFixture]
    public class StatusPageIoFixture
    {
        private StatusPageIoApi statusPageIo;
        private const string pageId = "q4ps3byy50wl";
        private const string authToken = "c002b1ccc47731d74b734c6f7d67e909b1d655671773cc34fcb747a425be91da ";
        private const string testSubscriberEmail = "blackhole@litmus.com";
        private const string testSubscriberPhone = "617-555-1212";
        private const string testSubscriberCountry = "US";

        [SetUp]
        public void SetUp()
        {
            statusPageIo = new StatusPageIoApi(authToken);
        }

        [Test]
        public async Task GetPage_ShouldReturnValidPage()
        {
            var page = await statusPageIo.GetPageProfile(pageId);

            Assert.That(page, Is.Not.Null);
        }

        [Test]
        public async Task UpdatePage_ShouldReturnUpdatedPage()
        {
            var page = await statusPageIo.GetPageProfile(pageId);

            Assert.That(page, Is.Not.Null);

            page.Name = page.Name + " - test";

            var updatedPage = await statusPageIo.UpdatePageProfile(page);

            Assert.That(updatedPage.Name == "Test Company - test");

            updatedPage.Name = "Test Company";
            await statusPageIo.UpdatePageProfile(updatedPage);
        }

        [Test]
        public async Task GetComponents_ShouldReturnAtLeastOneComponent()
        {
            var components = await statusPageIo.GetComponents(pageId);
            Assert.That(components.Any());
        }

        [Test]
        public async Task UpdateComponent_ShouldReturnUpdatedComponent()
        {
            var components = await statusPageIo.GetComponents(pageId);
            Assert.That(components.Any());

            var componentToUpdate = components.First();
            var originalName = componentToUpdate.Name;

            componentToUpdate.Name = componentToUpdate.Name + " - Test";
            componentToUpdate.Status = ComponentStatus.PartialOutage;

            var updatedComponent = await statusPageIo.UpdateComponent(pageId, componentToUpdate);

            Assert.That(updatedComponent.Name == originalName + " - Test");

            updatedComponent.Name = originalName;
            updatedComponent.Status = ComponentStatus.Operational;
            await statusPageIo.UpdateComponent(pageId, updatedComponent);
        }

        [Test]
        public async Task GetAllIncidents_ShouldReturnAtLeastOneIncident()
        {
            var components = await statusPageIo.GetAllIncidents(pageId);
            Assert.That(components.Any());
        }

        [Test]
        public async Task GetUnresolvedIncidents_ShouldReturnAtLeastOneIncident()
        {
            var components = await statusPageIo.GetUnresolvedIncidents(pageId);
            Assert.That(components.All(n => n.Status != IncidentStatus.Resolved));
        }

        [Test]
        public async Task GetScheduledIncidents_ShouldReturnAtLeastOneIncident()
        {
            var components = await statusPageIo.GetScheduledIncidents(pageId);
            Assert.That(components.All(n => n.Status == IncidentStatus.Scheduled));
        }

        [Test]
        public async Task CreateRealtimeIncident_ShouldReturnNewIncident()
        {
            var incident = await statusPageIo.CreateRealtimeIncident(pageId, "Test Incident");
            Assert.That(incident.Name == "Test Incident");

            await statusPageIo.DeleteIncident(pageId, incident.Id);

            var incidentsAfterDelete = await statusPageIo.GetAllIncidents(pageId);

            Assert.That(incidentsAfterDelete.All(n => n.Id != incident.Id));
        }

        [Test]
        [Ignore("Trial pages cannot add subscribers who are not team members")]
        public async Task GetSubscribers_ShouldReturnAtLeastOneSubscriber()
        {
            var subscriber1 = await statusPageIo.CreateEmailSubscriber(pageId, testSubscriberEmail);

            var subscribers = await statusPageIo.GetSubscribers(pageId);
            Assert.That(subscribers.Any());

            await statusPageIo.DeleteSubscriber(pageId, subscriber1.Id);
        }

        //this sends real SMS and email
        [Test]
        [Ignore("Trial pages cannot add subscribers who are not team members")]
        public async Task CreateSubscribers_ShouldCreateSubscriber()
        {
            var subscriber1 = await statusPageIo.CreateEmailSubscriber(pageId, testSubscriberEmail);
            Assert.That(subscriber1, Is.Not.Null);

            var subscriber2 = await statusPageIo.CreatePhoneSubscriber(pageId, testSubscriberPhone, testSubscriberCountry);
            Assert.That(subscriber2, Is.Not.Null);

            await statusPageIo.DeleteSubscriber(pageId, subscriber1.Id);
            await statusPageIo.DeleteSubscriber(pageId, subscriber2.Id);
        }

        [Test]
        public async Task GetMetricProviders_ShouldReturnFiveMetricProviders()
        {
            var providers = await statusPageIo.GetMetricProviders();
            Assert.That(providers.Count() == 5);
        }

        [Test]
        public async Task GetMetricProvidersForPage_ShouldReturnAMetricProviders()
        {
            var providers = await statusPageIo.GetMetricProvidersForPage(pageId);
            Assert.That(providers.Any());
        }

    }
}
