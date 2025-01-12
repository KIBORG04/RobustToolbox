﻿using System.Threading.Tasks;
using Robust.Shared;
using Robust.Shared.Resources;
using SixLabors.ImageSharp;

namespace Robust.Client;

/// <summary>
/// Logic for "warming up" things like slow static constructors concurrently.
/// </summary>
internal static class ClientWarmup
{
    public static void RunWarmup()
    {
        Task.Run(WarmupCore);
    }

    private static void WarmupCore()
    {
        // Get ImageSharp loaded.
        _ = Configuration.Default;

        SharedWarmup.WarmupCore();

        // Preload the JSON loading code.
        RsiLoading.Warmup();
    }
}
