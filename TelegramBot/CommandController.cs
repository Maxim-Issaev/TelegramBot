using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Awesome
{
    public class CommandController
    {
        public CommandController(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
        }
        private ITelegramBotClient botClient;
        public List<BotCommand> Update()
        {
            List<BotCommand> botCommands = new List<BotCommand>();
            BotCommand command = new BotCommand
            {
                Command = "time",
                Description = "Узнать время"
            };
            botCommands.Add(command);
            command = new BotCommand
            {
                Command = "help",
                Description = "Помощь"
            };
            botCommands.Add(command);
            return botCommands;

        }
        public async Task RunCommand(Message message)
        {
            string command = message.Text;
            string outString;
            if (command == "/help")
            {
                outString = "/time - узнать время";
                await botClient.SendTextMessageAsync(text:outString,chatId: message.Chat.Id );
            }
            else if (command=="/time")
            {
                outString = Convert.ToString(DateTime.Now);
                await botClient.SendTextMessageAsync(text: outString, chatId: message.Chat.Id);
            }
            else
            {
                outString = "Я не знаю такой команды :(";
                await botClient.SendTextMessageAsync(text: outString, chatId: message.Chat.Id);
            }
        }
    }
}
