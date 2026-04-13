using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace uvlo_application.Abstractions.Messaging
{
  public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand>
    where TCommand : ICommand;

  public interface ICommandHandler<in TCommand, TResponse>
      : IRequestHandler<TCommand, TResponse>
      where TCommand : ICommand<TResponse>;
}
