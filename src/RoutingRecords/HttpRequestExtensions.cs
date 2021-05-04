using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;

namespace RoutingRecords
{

	public static class HttpRequestExtensions
	{
		/// <summary>
		/// Gets a value of type <see cref="T"/> from the route values for the specified <see cref="HttpRequest"/>.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="req">The <see cref="HttpRequest"/>.</param>
		/// <param name="name">The name of the route value.</param>
		/// <returns>An object of type <see cref="T"/> found in the route value collection of the <see cref="HttpRequest"/>.</returns>
		public static T FromRoute<T>(this HttpRequest req, string name)
		{
			return req.RouteValues[name].As<T>();
		}

		/// <summary>
		/// Gets a value of type <see cref="T"/> from the query values for the specified <see cref="HttpRequest"/>.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="req">The <see cref="HttpRequest"/>.</param>
		/// <param name="name">The name of the query value.</param>
		/// <returns>An object of type <see cref="T"/> found in the query value collection of the <see cref="HttpRequest"/>.</returns>
		public static T FromQuery<T>(this HttpRequest req, string name)
		{
			return req.Query[name].ToString().As<T>();
		}

		/// <summary>
		/// Tries to get a value of type <see cref="T"/> from the route values for the specified <see cref="HttpRequest"/>.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="req">The <see cref="HttpRequest"/>.</param>
		/// <param name="name">The name of the route value.</param>
		/// <param name="result">An object of type <see cref="T"/> found in the route value collection of the <see cref="HttpRequest"/>.</param>
		/// <returns>If the route value exists.</returns>
		public static bool TryFromRoute<T>(this HttpRequest req, string name, out T result)
		{
			result = req.RouteValues.ContainsKey(name) ? req.RouteValues[name].As<T>() : default;
			return req.RouteValues.ContainsKey(name);
		}

		/// <summary>
		/// Tries to get a value of type <see cref="T"/> from the query values for the specified <see cref="HttpRequest"/>.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="req">The <see cref="HttpRequest"/>.</param>
		/// <param name="name">The name of the query value.</param>
		/// <param name="result">An object of type <see cref="T"/> found in the query value collection of the <see cref="HttpRequest"/>.</param>
		/// <returns>If the query value exists.</returns>
		public static bool TryFromQuery<T>(this HttpRequest req, string name, out T result)
		{
			result = req.Query.ContainsKey(name) ? req.Query[name].As<T>() : default;
			return req.Query.ContainsKey(name);
		}

		private static T As<T>(this object obj)
		{
			if (obj == null) return default;
			if (obj is T t) return t;
			if (obj is StringValues sv) obj = sv.ToString();
			if (obj is string s)
			{
				if (string.IsNullOrWhiteSpace(s)) return default;

				var converter = TypeDescriptor.GetConverter(typeof(T));
				if (converter.CanConvertFrom(typeof(string))) return (T)converter.ConvertFrom(s);
			}

			return default;
		}
	}
}
