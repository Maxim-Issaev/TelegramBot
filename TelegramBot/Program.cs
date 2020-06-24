using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Awesome
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main()
        {
            botClient = new TelegramBotClient("Token");
            var me = botClient.GetMeAsync().Result;
            var commandController = new CommandController(botClient);
            List<BotCommand> botCommands = commandController.Update();
            botClient.SetMyCommandsAsync(botCommands);
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id} form {e.Message.Chat.FirstName} {e.Message.Chat.LastName}.");
                Console.WriteLine($"Text: {e.Message.Text}");
                Logger logger = new Logger();
                await logger.RefreshAsync(e);
                if (e.Message.Text[0] == '/')
                {
                    CommandController controller = new CommandController(botClient);
                    await controller.RunCommand(e.Message);
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: "Я получил:\n" + e.Message.Text
                    );
                }
            }
        }
    }
}
