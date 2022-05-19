# UiPath Activity: Telegram Activities

This activity is intended to send messages to a Telegram user or group. To use it you need to have created a bot by BotFather provided by Telegram itself, the BotFather will provide the first param (Bot Token) to use the activity.

## Get the BotToken
To get the bot token, follow the next steps:

1. Enter the chat with [BotFather](https://t.me/botfather);
2. Once with a bot created, type ```/mybots``` for BotFather;
3. Select your bot;
4. Click in ```API Token```, the BotFather gonna show to you the token from your bot, you can see that in the following image:

![image](https://user-images.githubusercontent.com/17112000/124394497-1a763a80-dcd6-11eb-8c57-2f51fa85cd62.png)

## Get the Chat_ID

To get th chat id, follow the next steps:

1. Add the Telegram BOT to the group;
2. Get the list of updates for your BOT, through this link: ```https://api.telegram.org/bot<YourBOTToken>/getUpdates```;
3. Look for the "chat" object, and get the paranm ```id```:

![image](https://user-images.githubusercontent.com/17112000/124395115-abe6ac00-dcd8-11eb-83de-4a9f487224e9.png)

## Activities

### Telegram Scope

To use the activity you need to fill some params: ```Bot Token``` and ```Chat ID```.

![image](https://user-images.githubusercontent.com/17112000/169197759-29af2fe5-928d-44ee-93b1-b304297abdcd.png)

### Telegram Send Message

To use the activity you need to fill the param ```Message```.

![image](https://user-images.githubusercontent.com/17112000/169197721-6164c931-5068-409f-98a4-544dd4fa9777.png)


Once the parameters are filled in correctly, you can perform the activity and it will work!
