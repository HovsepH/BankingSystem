using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

public class SimpleJsonMergeAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var contentBuilder = new JObject();

        foreach (var response in responses)
        {
            var downstreamResponse = await response.Items.DownstreamResponse().Content.ReadAsStringAsync();
            var jsonContent = JObject.Parse(downstreamResponse);

            foreach (var property in jsonContent.Properties())
            {
                contentBuilder[property.Name] = property.Value;
            }
        }

        var mergedContent = new StringContent(contentBuilder.ToString(), System.Text.Encoding.UTF8, "application/json");
        return new DownstreamResponse(mergedContent, System.Net.HttpStatusCode.OK, new List<Header>(), "application/json");
    }
}
