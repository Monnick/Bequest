using FakeBus.Subscribe;
using System;
using System.Collections.Generic;
using System.Text;
using FakeBus.Contracts;
using System.Net.Sockets;
using FakeBus.Http.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Project.Shared.Messages;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FakeBus.Http.Subscribe
{
	public class HttpSubscriber : Subscriber
	{
		private TcpListener _listener { get; set; }
		private bool _listen = false;
		private CancellationTokenSource _cancellation { get; set; }

		private SubscriberConfiguration SubscriberConfiguration
		{
			get
			{
				return Configuration as SubscriberConfiguration;
			}
		}

		public HttpSubscriber(ILogger logger, ISubscriberConfiguration config) : base(logger, config)
		{
			if (!typeof(SubscriberConfiguration).IsInstanceOfType(config))
				throw new InvalidCastException("wrong configuration type");

			_cancellation = null;
		}

		public override void Initialize()
		{
			Logger.LogDebug("HttpSubscriber - start Initialize()");

			var myip = System.Net.Dns.GetHostEntry(""); //lookup my address 
			Logger.LogInformation("HttpSubscriber - Listen on port: " + SubscriberConfiguration.ListeningPort);
			_listener = new TcpListener(myip.AddressList.First(), SubscriberConfiguration.ListeningPort);
			_cancellation = new CancellationTokenSource();

			Logger.LogDebug("HttpSubscriber - finish Initialize()");
		}

		public override void Start()
		{
			Logger.LogDebug("HttpSubscriber - start Start()");

			if (_listen)
			{
				Logger.LogError("HttpSubscriber - Already listening. Stop first or continue listening");
				throw new InvalidOperationException("already listening");
			}

			_listener.Start();
			Task.Run((Action)Listen, _cancellation.Token);
			_listen = true;

			Logger.LogDebug("HttpSubscriber - finish Start()");
		}

		public override void Stop()
		{
			Logger.LogDebug("HttpSubscriber - start Stop()");

			_listen = false;
			_cancellation.Cancel();

			Logger.LogDebug("HttpSubscriber - finish Stop()");
		}

		private async void Listen()
		{
			int errorCount = 0;

			while (_listen && !_cancellation.IsCancellationRequested)
			{
				try
				{
					var client = await Task.Run(
						() => _listener.AcceptTcpClientAsync(),
								_cancellation.Token);
					
					var transmission = ReadTransmission(client);

					HandleTransmission(transmission);

					client.GetStream().Dispose();

					if(errorCount > 0) // on successfull run -> decrease error count
						errorCount--;
				}
				catch (Exception ex)
				{
					if (++errorCount == 3) // too much errors
					{
						Logger.LogError(ex, "HttpSubscriber - Error counter exceeded!");
						throw ex;
					}
					else
					{
						Logger.LogWarning(ex, "HttpSubscriber - Error occured!");
					}
				}
			}
		}

		private Transmission.Transmission ReadTransmission(TcpClient client)
		{
			var serializer = new JsonSerializer();
			using (var reader = new StreamReader(client.GetStream()))
			{
				using (var jsonReader = new JsonTextReader(reader))
				{
					return serializer.Deserialize<Transmission.Transmission>(jsonReader);
				}
			}
		}

		private void HandleTransmission(Transmission.Transmission transmission)
		{
			if (transmission == null || transmission.MessageType == null)
			{
				Logger.LogInformation("HttpSubscriber - Message or message type is null!");
				return;
			}

			Logger.LogDebug("HttpSubscriber - Message type received: " + transmission.MessageType.ToString());

			var serializer = new JsonSerializer();
			using (var reader = new StringReader(transmission.Message))
			{
				using (var jsonReader = new JsonTextReader(reader))
				{
					var message = serializer.Deserialize(jsonReader, transmission.MessageType) as Message;
					base.HandleMessage(message, transmission.MessageType);
				}
			}
		}
	}
}
