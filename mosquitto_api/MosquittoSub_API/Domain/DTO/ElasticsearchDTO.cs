using System;

namespace MosquittoSub_API.Domain.DTO
{
    public class ElasticsearchDTO
    {
        public ElasticsearchDTO(string value)
        {
            Value = value;
        }

        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Value { get; private set; } = string.Empty;
        public string Pod { get; private set; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public DateTime Date { get; private set; } = DateTime.Now;
    }
}
