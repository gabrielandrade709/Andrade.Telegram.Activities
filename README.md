# UiPath Activity: Telegram activities

## Summary
1. [Package in UiPath](#PackageInUiPath)
2. [Get the BotToken](#GetTheBotToken)
3. [Get the Chat_ID](#GetTheChatId)
4. [Activities](#Activities)
    1. [Telegram Scope](#TelegramScope)
    2. [Telegram Send Message](#TelegramSendMessage)
    3. [Telegram Send an image with a message](#TelegramSendAndImageWithAMessage)

---

Some UiPath activities. To use it you need to have created a bot by <b>BotFather</b> provided by Telegram itself, the <b>BotFather</b> will provide the first param (<b>Bot Token</b>) to use the activity.

## Package in UiPath <a name="PackageInUiPath"></a>

![image](https://user-images.githubusercontent.com/17112000/169677569-8903b4c6-95e3-436d-881f-9482aeeded0f.png)


## Get the BotToken <a name="GetTheBotToken"></a>
To get the bot token, follow the next steps:

1. Enter the chat with [BotFather](https://t.me/botfather);
2. Once with a bot created, type ```/mybots``` for BotFather;
3. Select your bot;
4. Click in ```API Token```, the BotFather gonna show to you the token from your bot, you can see that in the following image:

![image](https://user-images.githubusercontent.com/17112000/124394497-1a763a80-dcd6-11eb-8c57-2f51fa85cd62.png)

## Get the Chat_ID <a name="GetTheChatId"></a>

To get th chat id, follow the next steps:

1. Add the Telegram BOT to the group;
2. Get the list of updates for your BOT, through this link: ```https://api.telegram.org/bot<YourBOTToken>/getUpdates```;
3. Look for the "chat" object, and get the paranm ```id```:

![image](https://user-images.githubusercontent.com/17112000/124395115-abe6ac00-dcd8-11eb-83de-4a9f487224e9.png)

## Activities <a name="Activities"></a>

### Telegram Scope <a name="TelegramScope"></a>

To use the activity you need to fill some params: ```Bot Token``` and ```Chat ID```.

![image](https://user-images.githubusercontent.com/17112000/169197759-29af2fe5-928d-44ee-93b1-b304297abdcd.png)

### Telegram Send Message <a name="TelegramSendMessage"></a>

To use the activity you need to fill the param ```Message```.

![image](https://user-images.githubusercontent.com/17112000/169197721-6164c931-5068-409f-98a4-544dd4fa9777.png)

### Telegram Send an image with a message <a name="TelegramSendAndImageWithAMessage"></a>

To use the activity you need to fill the params: ```Image path``` and ```Message```(optional).

![image](https://user-images.githubusercontent.com/17112000/169677798-b2f38dee-5494-4e8c-adbc-fedbce59b910.png)

Once the parameters are filled in correctly, you can perform the activity and it will work!
