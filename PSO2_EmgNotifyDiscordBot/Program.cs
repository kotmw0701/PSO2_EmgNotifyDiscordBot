using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PSO2_EmgNotifyDiscordBot {
    class Program {

        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync() {
            string token = Properties.Settings.Default.Token;

            client = new DiscordSocketClient();
            commands = new CommandService();
            client.Log += Log;

            services = new ServiceCollection().BuildServiceProvider();
            client.MessageReceived += MessageReceived;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);//Botが落ちないようにここでずっと処理を止めてる
        }

        private async Task MessageReceived(SocketMessage socketMessage) {
            var message = socketMessage as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) 
                || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(client, message);

            var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
