using Telegram.Bot.Args;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Awesome
{
    public class Logger
    {
        public Logger()
        {

        }
        public async System.Threading.Tasks.Task RefreshAsync(MessageEventArgs message)
        {
            await BaseUpdateAsync(message);
            await MessageLogAsync(message);
        }
        private async System.Threading.Tasks.Task BaseUpdateAsync(MessageEventArgs message)
        {
            long ChatId = message.Message.Chat.Id;
            string LastName = message.Message.Chat.LastName;
            string FirstName = message.Message.Chat.FirstName;
            Data OldData = new Data();
            string FindeJSON;
            try
           {
                StreamReader reader = new StreamReader("./logs/Base.json");
                FindeJSON = await reader.ReadLineAsync();
                reader.Close();
                OldData = JsonConvert.DeserializeObject<Data>(FindeJSON);
            }
            catch(System.IO.FileNotFoundException)
            {
                User NewUser = new User();
                OldData.Users = new List<User>();
                NewUser.ChatId = ChatId;
                NewUser.LastName = LastName;
                NewUser.FirstName = FirstName;
                OldData.Users.Add(NewUser);
                using (StreamWriter writer = new StreamWriter("./logs/Base.json"))
                {
                    await writer.WriteLineAsync(JsonConvert.SerializeObject(OldData));
                    writer.Close();
                }
            }
            bool IsNew = true;
            for (int i = 0; i < OldData.Users.Count(); i++)
            {
                if (OldData.Users[i].ChatId == ChatId)
                    IsNew = false;
            }
            if (IsNew)
            {
                User NewUser = new User();
                NewUser.ChatId = ChatId;
                NewUser.LastName = LastName;
                NewUser.FirstName = FirstName;
                OldData.Users.Add(NewUser);
                using (StreamWriter writer = new StreamWriter("./logs/Base.json"))
                {
                    await writer.WriteLineAsync(JsonConvert.SerializeObject(OldData));
                    writer.Close();
                }
            }
        }
        private async System.Threading.Tasks.Task MessageLogAsync(MessageEventArgs message)
        {
            using (StreamWriter writer = new StreamWriter($"./logs/ms{message.Message.Chat.Id}.txt", true))
            { 
                await writer.WriteLineAsync($"{message.Message.Text}\t{message.Message.Date}");
                writer.Close();
            }
        }
    }
}
