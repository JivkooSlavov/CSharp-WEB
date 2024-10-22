using ChatApp.Models.Message;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {

        private static List<KeyValuePair<string, string>> s_messanges = new List<KeyValuePair<string, string>>();

        public IActionResult Show()
        {
            if(s_messanges.Count < 1)
            {
                return View(new ChatViewModel());
            }

            var chatmodel = new ChatViewModel()
            {
                Messages = s_messanges
                .Select(m => new MessageViewModel()
                {
                    Sender = m.Key,
                    MessageText = m.Value
                })
                .ToList()
            };

            return View(chatmodel);
        }

        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;
            s_messanges.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

            return RedirectToAction("Show");
        }
    }
}
