using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace uvlo_application.Abstractions.Messaging
{
  public interface ICommand : IRequest, IBaseCommand;

  public interface ICommand<TResponse> : IRequest<TResponse>, IBaseCommand;

  public interface IBaseCommand;
}
