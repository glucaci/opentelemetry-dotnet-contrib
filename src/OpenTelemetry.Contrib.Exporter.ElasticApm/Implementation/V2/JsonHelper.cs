﻿using System.Text.Json;

namespace OpenTelemetry.Contrib.Exporter.Elastic.Implementation.V2
{
    internal static class JsonHelper
    {
        internal static readonly JsonEncodedText TransactionPropertyName = JsonEncodedText.Encode("transaction");
        internal static readonly JsonEncodedText SpanPropertyName = JsonEncodedText.Encode("span");

        internal static readonly JsonEncodedText NamePropertyName = JsonEncodedText.Encode("name");
        internal static readonly JsonEncodedText TraceIdPropertyName = JsonEncodedText.Encode("trace_id");
        internal static readonly JsonEncodedText IdPropertyName = JsonEncodedText.Encode("id");
        internal static readonly JsonEncodedText ParentIdPropertyName = JsonEncodedText.Encode("parent_id");
        internal static readonly JsonEncodedText DurationPropertyName = JsonEncodedText.Encode("duration");
        internal static readonly JsonEncodedText TimestampPropertyName = JsonEncodedText.Encode("timestamp");
        internal static readonly JsonEncodedText TypePropertyName = JsonEncodedText.Encode("type");
        internal static readonly JsonEncodedText SpanCountPropertyName = JsonEncodedText.Encode("span_count");
        internal static readonly JsonEncodedText DroppedPropertyName = JsonEncodedText.Encode("dropped");
        internal static readonly JsonEncodedText StartedPropertyName = JsonEncodedText.Encode("started");

        internal static readonly JsonEncodedText MetadataPropertyName = JsonEncodedText.Encode("metadata");
        internal static readonly JsonEncodedText ServicePropertyName = JsonEncodedText.Encode("service");
        internal static readonly JsonEncodedText EnvironmentPropertyName = JsonEncodedText.Encode("environment");
        internal static readonly JsonEncodedText AgentPropertyName = JsonEncodedText.Encode("agent");
        internal static readonly JsonEncodedText VersionPropertyName = JsonEncodedText.Encode("version");
    }
}
