// Copyright © 2019 ABBYY Production LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;

namespace Abbyy.CloudSdk.V2.Client
{
	internal static class Serializer
	{
		public static string ToQueryString(object dto)
		{
			if (dto is null)
			{
				return string.Empty;
			}

			var query = HttpUtility.ParseQueryString(string.Empty);

			var props = dto.GetType().GetProperties();
			foreach (var prop in props)
			{
				var value = prop.GetValue(dto);
				var stringValue = GetStringValue(value);

				if (string.IsNullOrWhiteSpace(stringValue))
				{
					continue;
				}

				var propertyName = GetPropertyName(prop);

				query[propertyName] = stringValue;
			}

			return query.ToString();
		}

		private static string GetStringValue(object value)
		{
			switch (value)
			{
				case string str:
					return str;
				case IEnumerable collection:
					return GetCollectionValues(collection);
				case DateTime dateTime:
					return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
				default:
					return value?.ToString();
			}
		}

		private static string GetCollectionValues(IEnumerable collection)
		{
			var values = new List<string>();

			foreach (var element in collection)
			{
				var elementType = element.GetType();
				var value = element.ToString();

				if (elementType.IsEnum)
				{
					value = GetEnumValue(elementType, value);
				}

				values.Add(value);
			}

			return string.Join(",", values);
		}

		private static string GetEnumValue(Type enumType, string value)
		{
			var memberInfo = enumType.GetMember(value);

			var attribute = memberInfo[0]
				.GetCustomAttribute(typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

			var actualValue = attribute is null ?
				char.ToLowerInvariant(value[0]) + value.Substring(1) :
				attribute.PropertyName;

			return actualValue;
		}

		private static string GetPropertyName(PropertyInfo prop)
		{
			var attribute = prop
				.GetCustomAttribute(typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

			return attribute is null
				? char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1)
				: attribute.PropertyName;
		}
	}
}
