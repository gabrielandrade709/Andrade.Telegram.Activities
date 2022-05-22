using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using Andrade.Telegram.Activities.Design.Designers;
using Andrade.Telegram.Activities.Design.Properties;

namespace Andrade.Telegram.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(TelegramSendMessage), categoryAttribute);
            builder.AddCustomAttributes(typeof(TelegramSendMessage), new DesignerAttribute(typeof(TelegramSendMessageDesigner)));
            builder.AddCustomAttributes(typeof(TelegramSendMessage), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TelegramScope), categoryAttribute);
            builder.AddCustomAttributes(typeof(TelegramScope), new DesignerAttribute(typeof(TelegramScopeDesigner)));
            builder.AddCustomAttributes(typeof(TelegramScope), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(TelegramSendAnImageWithAMessage), categoryAttribute);
            builder.AddCustomAttributes(typeof(TelegramSendAnImageWithAMessage), new DesignerAttribute(typeof(TelegramSendAnImageWithAMessageDesigner)));
            builder.AddCustomAttributes(typeof(TelegramSendAnImageWithAMessage), new HelpKeywordAttribute(""));

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
