using System;
using System.Runtime.InteropServices;

namespace discord_rpc
{
    internal class RPCx64
    {
        [DllImport("discord-rpc x64.dll", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.None)]
        internal static extern void Discord_Initialize(IntPtr applicationId, ref DiscordEventHandlers handlers, int autoRegister, IntPtr optionalSteamId);

        [DllImport("discord-rpc x64.dll", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Discord_Shutdown();

        [DllImport("discord-rpc x64.dll", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Discord_RunCallbacks();

        [DllImport("discord-rpc x64.dll", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Discord_UpdatePresence(ref DiscordRichPresence presence);

        [DllImport("discord-rpc x64.dll", EntryPoint = "Discord_ClearPresence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Discord_ClearPresence();

        [DllImport("discord-rpc x64.dll", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Discord_Respond(IntPtr userid, int reply);
    }
}