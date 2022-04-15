namespace CodeAid.UI.Data
{
    public class MessageManager
    {
        public async Task<List<MessageModel>> GetMessages()
        {
            ApiManager apiManager = new ApiManager();
            var Messages = await apiManager.GetAllMessages();
            return Messages;
        }

        public async Task<List<MessageModel>> GetMessagesFomUser(string id)
        {
            ApiManager apiManager = new ApiManager();
            var Messages = await apiManager.GetUserMessages(id);
            return Messages;
        }

        public async Task<List<MessageModel>> GetThreadMessages(string accessToken, int id)
        {
            ApiManager apiManager = new ApiManager();
            var Messages = await apiManager.GetThreadMessages(accessToken, id);
            return Messages;
        }



        public async Task<bool> CreateMessage(MessageDto message, string id)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.PostMessages(message, id);
            if (result)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> EditMessage(MessageModel message, string id)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.UpdateMessage(message, id);
            if (result)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> DeleteMessage(MessageModel message, string id)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.DeleteMessage(message, id);
            if (result)
            {
                return true;
            }
            return false;

        }
    }

}

