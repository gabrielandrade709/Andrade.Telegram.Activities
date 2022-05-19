using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using Andrade.Telegram.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using System.Net;
using System.Net.Http;

namespace Andrade.Telegram.Activities
{
    [LocalizedDisplayName(nameof(Resources.TelegramSendMessage_DisplayName))]
    [LocalizedDescription(nameof(Resources.TelegramSendMessage_Description))]
    public class TelegramSendMessage : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TelegramSendMessage_Message_DisplayName))]
        [LocalizedDescription(nameof(Resources.TelegramSendMessage_Message_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Message { get; set; }

        #endregion


        #region Constructors

        public TelegramSendMessage()
        {
            Constraints.Add(ActivityConstraints.HasParentType<TelegramSendMessage, TelegramScope>(string.Format(Resources.ValidationScope_Error, Resources.TelegramScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Message == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Message)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            Console.WriteLine("Sending message...");
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TelegramScope.ParentContainerPropertyTag);

            // Inputs
            string botToken = new NetworkCredential("", objectContainer.Get<System.Security.SecureString>()).Password;
            long chatID = int.Parse(objectContainer.Get<string>());
            var message = Message.Get(context);
            var bot = new TelegramBotClient(botToken);


            var sendMessage = await bot.SendTextMessageAsync(chatID, message, ParseMode.Html);


            // Outputs
            return (ctx) =>
            {
            };
        }

        #endregion
    }
}

