﻿using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenTelemetry.Contrib.Exporter.Elastic.Implementation;

namespace OpenTelemetry.Contrib.Exporter.Elastic
{
    internal class NdjsonContent : HttpContent
    {
        private static readonly MediaTypeHeaderValue NdjsonHeader =
            new MediaTypeHeaderValue("application/x-ndjson")
            {
                CharSet = new UTF8Encoding(false).WebName,
            };

        private readonly ElasticOptions options;
        private readonly Batch<Activity> batch;
        private readonly IJsonSerializable metadata;
        private Utf8JsonWriter writer;

        public NdjsonContent(ElasticOptions options, in Batch<Activity> batch)
        {
            this.options = options;
            this.batch = batch;
            this.metadata = this.CreateMetadata();

            this.Headers.ContentType = NdjsonHeader;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            this.EnsureWriter(stream);

            this.metadata.Write(this.writer);
            this.writer.WriteNewLine(stream);

            foreach (var activity in this.batch)
            {
                var span = activity.ToElasticApmSpan(this.options.IntakeApiVersion);
                span.Write(this.writer);
                this.writer.WriteNewLine(stream);
            }

            return Task.CompletedTask;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        private void EnsureWriter(Stream stream)
        {
            if (this.writer == null)
            {
                this.writer = new Utf8JsonWriter(stream);
            }
            else
            {
                this.writer.Reset(stream);
            }
        }

        private IJsonSerializable CreateMetadata()
        {
            return new Implementation.V2.Metadata(
                new Implementation.V2.Service(
                    this.options.ServiceName,
                    this.options.Environment,
                    new Implementation.V2.Agent(typeof(ElasticExporter).Assembly)));
        }
    }
}
