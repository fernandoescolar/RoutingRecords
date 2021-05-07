using System;

namespace RoutingRecords
{
	/// <summary>
	/// Specifies that a parameter should be bound deserializing from json from request body.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public class FromBodyAttribute : Attribute
	{
	}

	/// <summary>
	/// Specifies that a parameter should be bound from route values.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
	public class FromRouteAttribute : Attribute
	{
	}

	/// <summary>
	/// Specifies that a parameter should be bound from query string values.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
	public class FromQueryAttribute : Attribute
	{
	}

	/// <summary>
	/// Specifies that a parameter should be bound from header values.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
	public class FromHeaderAttribute : Attribute
	{
	}
}
