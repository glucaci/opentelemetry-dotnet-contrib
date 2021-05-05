﻿using System;
using System.Diagnostics;
using OpenTelemetry.Contrib.Exporter.ElasticApm.Implementation.V2;

namespace OpenTelemetry.Contrib.Exporter.ElasticApm.Implementation
{
    internal static class ElasticApmActivityExtensions
    {
        internal static IJsonSerializable ToElasticApmSpan(
            this Activity activity,
            IntakeApiVersion intakeApiVersion)
        {
            if (intakeApiVersion != IntakeApiVersion.V2)
            {
                throw new NotSupportedException();
            }

            string name = activity.DisplayName;
            string traceId = activity.GetTraceId();
            string id = activity.GetSpanId();
            string parentId = activity.GetParentId();
            long duration = activity.Duration.ToEpochMicroseconds();
            long timestamp = activity.StartTimeUtc.ToEpochMicroseconds();
            string type = activity.GetActivityType();

            if (activity.Kind == ActivityKind.Internal)
            {
                return new ElasticApmSpan(name, traceId, id, parentId, duration, timestamp, type);
            }

            return new ElasticApmTransaction(name, traceId, id, parentId, duration, timestamp, type);
        }

        private static string GetSpanId(this Activity activity)
        {
            return activity.SpanId.ToHexString();
        }

        private static string GetTraceId(this Activity activity)
        {
            return activity.Context.TraceId.ToHexString();
        }

        private static string GetParentId(this Activity activity)
        {
            return activity.ParentSpanId == default
                ? null
                : activity.ParentSpanId.ToHexString();
        }

        private static string GetActivityType(this Activity activity)
        {
            return activity.Kind switch
            {
                ActivityKind.Server => "server",
                ActivityKind.Producer => "producer",
                ActivityKind.Consumer => "consumer",
                ActivityKind.Client => "client",
                _ => null,
            };
        }
    }
}
