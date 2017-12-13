using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using UIKit;
using CoreText;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Straggler5
{
    public partial class SecondViewController : UIViewController
    {
        private static string directLineSecret = "4VUNz7rWeQs.cwA.OcA.KNOt5oohxiG5s3Wa32gBsR4hEUgKHT_WLcWeYmGQq-A";
        private static string botId = "urban-test-bot";
        private static string fromUser = "StragglerApp";

        // for Q&A
        private string knowledgebaseId = "620f6b60-d1f4-4f82-a38c-f7fc57055838"; // Use knowledge base id created.
        private string qnamakerSubscriptionKey = "2d8abc61b06445c7b5e9d85be30ee848"; //Use subscription key assigned to you.

        DirectLineClient client;
        Conversation conversation;

        protected SecondViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            Debug.WriteLine("constructor called");
        }

        public override void ViewWillAppear(bool animated)
        {
            Debug.WriteLine("ViewWillAppear called");
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            Debug.WriteLine("ViewDidLoad called");
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            askButton.TouchUpInside += AskButton_TouchUpInside;
            DirectLineClient client = new DirectLineClient(directLineSecret);

            // StartConversation();

            responseText.Text = "";
        }

        private void AskButton_TouchUpInside(object sender, EventArgs e)
        {
            AskQuestion(askText.Text);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            // EndConversation();
        }

        public async void StartConversation()
        {
            client = new DirectLineClient(directLineSecret);

            conversation = await client.Conversations.StartConversationAsync();

            new System.Threading.Thread(async () => await ReadBotMessagesAsync(client, conversation.ConversationId)).Start();

            AskQuestion("hi");
        }

        private async void AskQuestion(string query)
        {
            /* this code talks to a bot...
            Activity userMessage = new Activity
            {
                From = new ChannelAccount(fromUser),
                Text = askText.Text,
                Type = ActivityTypes.Message
            };

            client.Conversations.PostActivity(conversation.ConversationId, userMessage);
            await ReadBotMessagesAsync(client, conversation.ConversationId);
            */

            // This code talks to Azure Q&A Cognitive Services
            Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0");
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");

            //Add the question as part of the body
            var postBody = $"{{\"question\": \"{query}\"}}";

            //Send the POST request
            using (WebClient client = new WebClient())
            {
                //Set the encoding to UTF8
                client.Encoding = System.Text.Encoding.UTF8;

                //Add the subscription key header
                client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                var responseString = client.UploadString(builder.Uri, postBody);
                Debug.WriteLine(responseString);

                var jObject = JObject.Parse(responseString);
                var answer = jObject["answer"];
                var score = jObject["score"];
                Debug.WriteLine(answer);
                Debug.WriteLine(score);
                responseText.Text = $"{answer} : score {score}";
            }
        }

        private async Task ReadBotMessagesAsync(DirectLineClient client, string conversationId)
        {
            string watermark = null;

            var activitySet = await client.Conversations.GetActivitiesAsync(conversationId, watermark);
            watermark = activitySet?.Watermark;

            var activities = from x in activitySet.Activities
                             where x.From.Id == botId
                             select x;

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.Text);
                responseText.Text = activity.Text;

                if (activity.Attachments != null)
                {
                    foreach (Attachment attachment in activity.Attachments)
                    {
                        switch (attachment.ContentType)
                        {
                            case "application/vnd.microsoft.card.hero":
                                RenderHeroCard(attachment);
                                break;

                            case "image/png":
                                Console.WriteLine($"Opening the requested image '{attachment.ContentUrl}'");

                                Process.Start(attachment.ContentUrl);
                                break;
                        }
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
        }

        private static void RenderHeroCard(Attachment attachment)
        {
            const int Width = 70;
            Func<string, string> contentLine = (content) => string.Format($"{{0, -{Width}}}", string.Format("{0," + ((Width + content.Length) / 2).ToString() + "}", content));

            var heroCard = JsonConvert.DeserializeObject<HeroCard>(attachment.Content.ToString());

            if (heroCard != null)
            {
                Console.WriteLine("/{0}", new string('*', Width + 1));
                Console.WriteLine("*{0}*", contentLine(heroCard.Title));
                Console.WriteLine("*{0}*", new string(' ', Width));
                Console.WriteLine("*{0}*", contentLine(heroCard.Text));
                Console.WriteLine("{0}/", new string('*', Width + 1));
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
