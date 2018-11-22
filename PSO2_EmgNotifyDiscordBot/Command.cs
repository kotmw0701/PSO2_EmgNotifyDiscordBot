using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2_EmgNotifyDiscordBot {
    public class Command : ModuleBase {

        [Command("test")]
        public async Task CommandTest(string text = "きゅっきゅ") {
            await ReplyAsync(text);
        }
    }
}
