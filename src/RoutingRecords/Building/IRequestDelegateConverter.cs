using Microsoft.AspNetCore.Http;
using System;

namespace RoutingRecords.Building
{
	public interface IRequestDelegateConverter
	{
		bool CanConvert(Delegate @delegate);

		RequestDelegate Convert(Type routerecordType, Delegate @delegate);
	}
}
