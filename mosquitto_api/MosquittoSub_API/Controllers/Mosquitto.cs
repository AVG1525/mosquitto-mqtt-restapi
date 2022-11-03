using Microsoft.AspNetCore.Mvc;
using MosquittoSub_API.Domain.DTO;
using RestSharp;
using System;

namespace MosquittoSub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Mosquitto : ControllerBase
    {
        private readonly RestClient _restClient;

        private const string ELASTICSEARCH_PATH = "/search-mqtt-aron";
        private const string AUTORIZATION_KEY = "ApiKey WFNwY2k0TUJjS085am5mTTFIbkI6SkNVUlhaZ09RQXVHeUp6dmFLQnlFdw==";
        private const string ELASTICSEARCH_BASE_URL = "https://cbc086dd99c24e0cb20bc40a8daab8fa.us-central1.gcp.cloud.es.io:443";

        public Mosquitto()
        {
            var client = new RestClient(ELASTICSEARCH_BASE_URL);
            client.AddDefaultHeader("Authorization", AUTORIZATION_KEY);

            _restClient = client;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var restRequest = new RestRequest(ELASTICSEARCH_PATH + "/_search?size=10000&sort=date:desc", Method.Post);
            var response = _restClient.ExecutePost(restRequest);

            return Ok(new
            {
                POD = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                RESPONSE = response.Content
            });
        }

        [HttpPost]
        public IActionResult Post(string value)
        {
            var restRequest = new RestRequest(ELASTICSEARCH_PATH + "/_doc", Method.Post);

            restRequest.AddJsonBody(new ElasticsearchDTO(value.ToString()));
            var response = _restClient.ExecutePost(restRequest);

            return Created(string.Empty, response);
        }
    }
}
