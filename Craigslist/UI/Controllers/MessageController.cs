using Data.Models;
using DB.Database;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult CreateMessage(int postId)
        {
            MessageViewModel message_model = new MessageViewModel();
            message_model.Sender = User.Identity.GetUserName();
            message_model.Receiver = UserPost.GetPostById(postId).Author.UserName;
            message_model.postId = postId;
            return View(message_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMessage([Bind(Include = "MessageBody,postId")]MessageViewModel message)
        {
            if (ModelState.IsValid)
            {
                PostMessages.CreateMessage(message.postId, User.Identity.GetUserId(), message.MessageBody);
                return RedirectToAction("Index", "Posts");
            }
            ModelState.AddModelError("", "Message could not be sent");
            return View(message);
        }

        public ActionResult Inbox()
        {
            var messages = PostMessages.GetRecentMessagesByUser(User.Identity.GetUserId());
            return View(messages);
        }

        public ActionResult ViewPostMessages(int postId)
        {
            var messages = PostMessages.GetMessagesByPost(postId);
            return View("Inbox",messages);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var message = PostMessages.GetMessageById(id.Value);

            if (message == null)
                return HttpNotFound();

            return View(message);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostMessages.DeleteResponse(id, User.Identity.GetUserId(),out StringBuilder errors);

            if (errors.Length > 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());

            return RedirectToAction("Inbox");
        }

    }
}