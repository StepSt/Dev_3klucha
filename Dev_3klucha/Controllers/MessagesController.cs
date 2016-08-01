using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using System.Collections.Generic;

namespace Dev_3klucha
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        [Serializable]
        public class EchoDialog : IDialog<object>
        {
            
            protected int count = 1;
            string order = "";
            double price = 0.0;
            public async Task StartAsync(IDialogContext context)
            {
                context.Wait(MessageReceivedAsync);
            }
            public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
            {
                My_Telegram telegram = new My_Telegram();
                var message = await argument;
                if (message.Text == "reset")
                {
                    PromptDialog.Confirm(
                        context,
                        AfterResetAsync,
                        "Are you sure you want to reset the count?",
                        "Didn't get that!",
                        promptStyle: PromptStyle.None);
                }
                else
                {
                    switch (message.ChannelId)
                    {
                        #region Telegram
                        case "telegram":
                            switch (message.Text)
                            {
                                case "/start":
                                    await context.PostAsync(message.From.Id);
                                    telegram.Start(argument);
                                    order = "";
                                    break;
                                case "О воде":
                                    await context.PostAsync(telegram.Abut_water());                                
                                    break;
                                case "Покупка":
                                    telegram.By(argument);
                                    break;
                                case "Акции":
                                    await context.PostAsync(telegram.Sale());
                                    break;
                                case "Объем 18,9л":
                                    order = $"{message.Text} ";
                                    price = Properties.Settings.Default.Price_18;
                                    telegram.Count(argument);
                                    break;
                                case "Объем 5л":
                                    order = $"{message.Text} ";
                                    price = Properties.Settings.Default.Price_5;
                                    telegram.Count(argument);
                                    break;
                                case "Объем 1,5л":
                                    order = $"{message.Text} ";
                                    price = Properties.Settings.Default.Price_1;
                                    telegram.Count(argument);
                                    break;
                                case "2":
                                case "3":
                                case "4":
                                    order += $"Количество бутылок: {message.Text} Цена: {double.Parse(message.Text)*price}";
                                    telegram.Asc(argument);
                                    break;
                                case "Заказать":
                                    //отправка сообщения
                                    await context.PostAsync($"Спасибо за заказ {order}");
                                    telegram.Start(argument);
                                    break;
                            }
                            break;
                        #endregion
                        #region Emulator
                        case "emulator":
                            DbDev3klucha customers = new DbDev3klucha();
                            Product product = customers.GetProductEf_id(1);
                            await context.PostAsync($"Продук {product.Name} + {product.Price}");

                            switch (message.Text)
                            {
                                case "start":
                                    await context.PostAsync($"Добро пожаловать, хотите получать уведомления? Да/Нет");
                                    break;
                                case "Да":
                                    //customers.SetCustomerUser_id(message.Recipient.Id.ToString());
                                    break;
                                case "price":
                                    customers.SetProductEf_id(1, 200.0);
                                    break;
                            }
                            break;
                            #endregion
                    }
                    //await context.PostAsync(string.Format($"{this.count++}: You said {message.Text}; order {order}"));
                    context.Wait(MessageReceivedAsync);
                }
            }
            public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
            {
                var confirm = await argument;
                if (confirm)
                {
                    this.count = 1;
                    order = "";
                    await context.PostAsync("Reset count.");
                }
                else
                {
                    await context.PostAsync("Did not reset count.");
                }
                context.Wait(MessageReceivedAsync);
            }
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new EchoDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}