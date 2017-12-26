﻿using Microsoft.AspNetCore.Hosting;

namespace IDI.Digiccy.Transaction.WindowsService
{
    internal abstract class HostService
    {
        public HostService(IWebHost host) { }

        protected virtual void OnStarting(string[] args) { }

        protected virtual void OnStarted() { }

        protected virtual void OnStopped() { }
    }
}