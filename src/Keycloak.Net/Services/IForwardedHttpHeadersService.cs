﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Keycloak.Net.Services
{
    public interface IForwardedHttpHeadersService
    {
        ForwardedHttpHeaders Get();
    }
}
