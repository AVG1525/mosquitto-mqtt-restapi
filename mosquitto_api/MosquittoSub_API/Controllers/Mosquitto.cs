using Microsoft.AspNetCore.Mvc;
using MosquittoSub_API.Domain.DTO;
using RestSharp;

namespace MosquittoSub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Mosquitto : ControllerBase
    {
        private readonly RestClient _restClient;
        private readonly RestRequest _restRequest;

        private const string ELASTICSEARCH_PATH = "/search-mqtt-aron/_doc";
        private const string AUTORIZATION_KEY = "ApiKey WFNwY2k0TUJjS085am5mTTFIbkI6SkNVUlhaZ09RQXVHeUp6dmFLQnlFdw==";
        private const string ELASTICSEARCH_BASE_URL = "https://cbc086dd99c24e0cb20bc40a8daab8fa.us-central1.gcp.cloud.es.io:443";

        public Mosquitto()
        {
            var client = new RestClient(ELASTICSEARCH_BASE_URL);
            client.AddDefaultHeader("Authorization", AUTORIZATION_KEY);

            var request = new RestRequest(ELASTICSEARCH_PATH, Method.Post);

            _restClient = client;
            _restRequest = request;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(string value)
        {
            _restRequest.AddJsonBody(new ElasticsearchDTO(value.ToString()));
            var response = _restClient.ExecutePost(_restRequest);

            return Created(string.Empty, response);
        }
    }
}
