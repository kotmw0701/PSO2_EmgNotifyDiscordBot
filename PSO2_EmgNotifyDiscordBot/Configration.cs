using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO2_EmgNotifyDiscordBot {
    public class Configration {
        public static Configration Instance { get; } = new Configration();
        private Configration() {
            //using (var reader = new StreamReader("data.json", Encoding.UTF8))
            //    Datas = JsonConvert.DeserializeObject<PrivateDatas>(reader.ReadToEnd());
        }

        public PrivateDatas Datas { get; }
    }
}
