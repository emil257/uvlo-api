using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace uvlo_application.Abstractions.Messaging
{
  public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;
}
