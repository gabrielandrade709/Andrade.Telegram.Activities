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
using System.IO;

namespace Andrade.Telegram.Activities
{
    [LocalizedDisplayName(nameof(Resources.TelegramSendAnImageWithAMessage_DisplayName))]
    [LocalizedDescription(nameof(Resources.TelegramSendAnImageWithAMessage_Description))]
    public class TelegramSendAnImageWithAMessage : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TelegramSendAnImageWithAMessage_Image_DisplayName))]
        [LocalizedDescription(nameof(Resources.TelegramSendAnImageWithAMessage_Image_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Image { get; set; }

        [LocalizedDisplayName(nameof(Resources.TelegramSendAnImageWithAMessage_Message_DisplayName))]
        [LocalizedDescription(nameof(Resources.TelegramSendAnImageWithAMessage_Message_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Message { get; set; }

        #endregion


        #region Constructors

        public TelegramSendAnImageWithAMessage()
        {
            Constraints.Add(ActivityConstraints.HasParentType<TelegramSendAnImageWithAMessage, TelegramScope>(string.Format(Resources.ValidationScope_Error, Resources.TelegramScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Image == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Image)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            Console.WriteLine("Sending image...");
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TelegramScope.ParentContainerPropertyTag);

            // Inputs
            string botToken = new NetworkCredential("", objectContainer.Get<System.Security.SecureString>()).Password;
            long chatID = int.Parse(objectContainer.Get<string>());
            var image = Image.Get(context);
            var message = Message.Get(context);
            var bot = new TelegramBotClient(botToken);


            using (var stream = File.Open(image, FileMode.Open))
            { 
                var sendMessageWithImage = await bot.SendPhotoAsync(
                    chatId: chatID,
                    photo: stream,
                    caption: message,
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
            }

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

