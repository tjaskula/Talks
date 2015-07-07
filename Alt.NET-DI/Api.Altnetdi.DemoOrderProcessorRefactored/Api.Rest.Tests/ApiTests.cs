using System;
using System.Collections.Generic;
using System.Linq;
using Api.Rest.Infrastructure;
using Api.Rest.Infrastructure.Messages;
using Api.Rest.Installers;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Rest.Tests
{
	/// <summary>
	/// Description résumée pour UnitTest1
	/// </summary>
	[TestClass]
	public class ApiTests
	{
		[TestMethod]
		public void Every_message_type_has_a_handler()
		{
			var windsorContainer = new WindsorContainer();
			windsorContainer.Install(new MessageHandlerInstaller());

			var messageTypes = typeof (Message).Assembly.GetExportedTypes().Where(t => typeof (Message).IsAssignableFrom(t) && !t.IsAbstract);

			var messagesWhithoutHandler = new List<Type>();

			foreach (var messageType in messageTypes)
			{
				var handlerType = typeof (IMessageHandler<>).MakeGenericType(messageType);
				if (windsorContainer.Kernel.HasComponent(handlerType) == false)
					messagesWhithoutHandler.Add(handlerType);
			}

			Assert.AreEqual(0, messagesWhithoutHandler.Count);
		}
	}
}
