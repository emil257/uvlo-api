using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace uvlo_application.Abstractions.Messaging
{
  public interface IQuery<TResponse> : IRequest<TResponse>;
}
