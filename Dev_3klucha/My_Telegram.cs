using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev_3klucha
{
    public class My_Telegram
    {

        #region KeyBot_Telegram
        Telegram.Bot.Api bot = new Telegram.Bot.Api("247849259:AAEaAWQG10yLZIMbq6wUscJWo0eqVchY4pg");
        Telegram.Bot.Types.ReplyMarkups.IReplyMarkup keybot = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
        {
            Keyboard = new Telegram.Bot.Types.KeyboardButton[1][]
            {
            new Telegram.Bot.Types.KeyboardButton[] {"О воде","Покупка","Акции"},
            }
        };
        Telegram.Bot.Types.ReplyMarkups.IReplyMarkup keybot_product = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
        {
            Keyboard = new Telegram.Bot.Types.KeyboardButton[1][]
           {
        new Telegram.Bot.Types.KeyboardButton[] {"Объем 18,9л", "Объем 5л", "Объем 1,5л" }
           }
        };
        Telegram.Bot.Types.ReplyMarkups.IReplyMarkup keybot_product_qt = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
        {
            Keyboard = new Telegram.Bot.Types.KeyboardButton[1][]
           {
        new Telegram.Bot.Types.KeyboardButton[] {"2", "3", "4"},
           }
        };
        Telegram.Bot.Types.ReplyMarkups.IReplyMarkup keybot_ok = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
        {
            Keyboard = new Telegram.Bot.Types.KeyboardButton[1][]
           {
        new Telegram.Bot.Types.KeyboardButton[] {"Заказать"},
           }
        };
        #endregion

        public async void Start(IAwaitable<IMessageActivity> argument)
        {
            //return $"Текст из класса";
            var message = await argument;
            await bot.SendTextMessageAsync(message.From.Id.ToString(), "Текст приведствия", false, false, 0, keybot);
        }
        public string Abut_water()
        {
            return $"Водоподготовка и розлив натуральной воды «3 Ключа» производится на современном оборудовании, позволяющем сохранить природное качество и состав. Вода, обладающая сбалансированным составом, проходит механическую очистку от различных взвесей на фильтрах из нержавеющей стали. При таком методе обработки сохраняются все микроэлементы, которые даны ей от природы, и что самое главное, не нарушается структура воды, она остается живой.";
        }
        public async void By(IAwaitable<IMessageActivity> argument)
        {
            //return $"Текст из класса";
            var message = await argument;
            await bot.SendTextMessageAsync(message.From.Id.ToString(), "О товарах", false, false, 0, keybot_product);
        }
        public string Sale()
        {
            return $"Акции";
        }
        public async void Count(IAwaitable<IMessageActivity> argument)
        {
            //return $"Текст из класса";
            var message = await argument;
            await bot.SendTextMessageAsync(message.From.Id.ToString(), "Количество", false, false, 0, keybot_product_qt);
        }
        public async void Asc(IAwaitable<IMessageActivity> argument)
        {
            //return $"Текст из класса";
            var message = await argument;
            await bot.SendTextMessageAsync(message.From.Id.ToString(), "Подтверждение", false, false, 0, keybot_ok);
        }
    }
}