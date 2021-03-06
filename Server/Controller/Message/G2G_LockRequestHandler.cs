﻿using System;
using Base;
using Model;

namespace Controller
{
	[MessageHandler(AppType.Gate)]
	public class G2G_LockRequestHandler : AMRpcHandler<G2G_LockRequest, G2G_LockResponse>
	{
		protected override async void Run(Session session, G2G_LockRequest message, Action<G2G_LockResponse> reply)
		{
			G2G_LockResponse response = new G2G_LockResponse();
			try
			{
				Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
				if (unit == null)
				{
					response.Error = ErrorCode.ERR_NotFoundUnit;
					reply(response);
				}

				await unit.GetComponent<MasterComponent>().Lock(message.Address);

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}