﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Dto.Auth
{
    public class AuthRequest
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
