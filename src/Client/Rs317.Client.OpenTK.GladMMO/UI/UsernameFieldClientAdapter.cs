﻿using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace Rs317.GladMMO
{
	public sealed class UsernameFieldClientAdapter : IUIText
	{
		public string Text
		{
			get => Client.EnteredUsername;
			set => Client.EnteredUsername = value;
		}

		private GladMMOOpenTkClient Client { get; }

		public UsernameFieldClientAdapter(GladMMOOpenTkClient client)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
		}
	}
}