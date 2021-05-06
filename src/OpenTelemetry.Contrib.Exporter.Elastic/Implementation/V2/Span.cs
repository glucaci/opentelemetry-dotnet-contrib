﻿using System.Text.Json;

namespace OpenTelemetry.Contrib.Exporter.Elastic.Implementation.V2
{
    internal readonly struct Span : IJsonSerializable
    {
        public Span(
            string name,
            string traceId,
            string id,
            string parentId,
            long duration,
            long timestamp,
            string type)
        {
            this.Name = name;
            this.TraceId = traceId;
            this.Id = id;
            this.ParentId = parentId;
            this.Duration = duration;
            this.Timestamp = timestamp;
            this.Type = type;
        }

        public string Id { get; }

        public string TraceId { get; }

        public string ParentId { get; }

        public string Name { get; }

        public long Duration { get; }

        public long Timestamp { get; }

        public string Type { get; }

        public void Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            writer.WritePropertyName(JsonHelper.SpanPropertyName);
            writer.WriteStartObject();

            writer.WriteString(JsonHelper.NamePropertyName, this.Name);
            writer.WriteString(JsonHelper.TraceIdPropertyName, this.TraceId);
            writer.WriteString(JsonHelper.IdPropertyName, this.Id);
            writer.WriteString(JsonHelper.ParentIdPropertyName, this.ParentId);
            writer.WriteNumber(JsonHelper.DurationPropertyName, this.Duration);
            writer.WriteNumber(JsonHelper.TimestampPropertyName, this.Timestamp);
            writer.WriteString(JsonHelper.TypePropertyName, this.Type);

            writer.WriteEndObject();

            writer.WriteEndObject();
        }
    }
}
