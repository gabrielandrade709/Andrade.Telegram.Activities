using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using System.Security;
using System.Activities.Statements;
using System.ComponentModel;
using Andrade.Telegram.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using System.Linq;

namespace Andrade.Telegram.Activities
{
    [LocalizedDisplayName(nameof(Resources.TelegramScope_DisplayName))]
    [LocalizedDescription(nameof(Resources.TelegramScope_Description))]
    public class TelegramScope : ContinuableAsyncNativeActivity
    {
        #region Properties

        [Browsable(false)]
        public ActivityAction<IObjectContainer​> Body { get; set; }

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.TelegramScope_BotToken_DisplayName))]
        [LocalizedDescription(nameof(Resources.TelegramScope_BotToken_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<SecureString> BotToken { get; set; }

        [LocalizedDisplayName(nameof(Resources.TelegramScope_ChatID_DisplayName))]
        [LocalizedDescription(nameof(Resources.TelegramScope_ChatID_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<long> ChatID { get; set; }

        // A tag used to identify the scope in the activity context
        internal static string ParentContainerPropertyTag => "ScopeActivity";

        // Object Container: Add strongly-typed objects here and they will be available in the scope's child activities.
        private readonly IObjectContainer _objectContainer;

        #endregion


        #region Constructors

        public TelegramScope(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;

            Body = new ActivityAction<IObjectContainer>
            {
                Argument = new DelegateInArgument<IObjectContainer> (ParentContainerPropertyTag),
                Handler = new Sequence { DisplayName = Resources.Do }
            };
        }

        public TelegramScope() : this(new ObjectContainer())
        {

        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            if (BotToken == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(BotToken)));
            if (ChatID == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(ChatID)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<NativeActivityContext>> ExecuteAsync(NativeActivityContext  context, CancellationToken cancellationToken)
        {
            // Inputs
            var botToken = BotToken.Get(context);
            var chatID = ChatID.Get(context);

            _objectContainer.Add<System.Security.SecureString>(botToken);
            _objectContainer.Add<string>(chatID.ToString());

            return (ctx) => {
                // Schedule child activities
                if (Body != null)
				    ctx.ScheduleAction<IObjectContainer>(Body, _objectContainer, OnCompleted, OnFaulted);

                // Outputs
            };
        }

        #endregion


        #region Events

        private void OnFaulted(NativeActivityFaultContext faultContext, Exception propagatedException, ActivityInstance propagatedFrom)
        {
            faultContext.CancelChildren();
            Cleanup();
        }

        private void OnCompleted(NativeActivityContext context, ActivityInstance completedInstance)
        {
            Cleanup();
        }

        #endregion


        #region Helpers
        
        private void Cleanup()
        {
            var disposableObjects = _objectContainer.Where(o => o is IDisposable);
            foreach (var obj in disposableObjects)
            {
                if (obj is IDisposable dispObject)
                    dispObject.Dispose();
            }
            _objectContainer.Clear();
        }

        #endregion
    }
}

