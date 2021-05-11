﻿// <copyright file="Utf8JsonWriterExtensions.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.IO;
using System.Text.Json;

namespace OpenTelemetry.Contrib.Exporter.Elastic
{
    internal static class Utf8JsonWriterExtensions
    {
        private static readonly int MaxStringLength = 1024;
        private static readonly byte NewLine = (byte)'\n';

        internal static void WriteNewLine(this Utf8JsonWriter writer, Stream stream)
        {
            writer.Flush();
            writer.Reset();
            stream.WriteByte(NewLine);
        }

        internal static void WriteStringLimited(this Utf8JsonWriter writer, JsonEncodedText propertyName, string value)
        {
            if (value == null)
            {
                return;
            }

            if (value.Length <= MaxStringLength)
            {
                writer.WriteString(propertyName, value);
            }
            else
            {
                writer.WriteString(propertyName, value.AsSpan().Slice(0, MaxStringLength));
            }
        }
    }
}