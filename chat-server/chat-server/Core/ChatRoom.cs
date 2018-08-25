using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace chat_server
{
    /// <summary>
    /// A lobby containing multiple users in which messages will propogate to all users.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public class ChatRoom : IRoom
    {
        public string Name { get; private set; }
        public bool IsPersistent { get; private set; }
        public List<IUser> Users { get; }

        public ChatRoom(string name, bool isPersistent)
        {
            Name = name;
            IsPersistent = isPersistent;
            Users = new List<IUser>();
        }

        public async Task Message(IMessage message)
        {
            var taskList = new List<Task>();

            foreach(var user in Users)
            {
                taskList.Add(user.Message(message));
            }

            await Task.WhenAll(taskList.ToArray());
        }

        public async Task Write(string message)
        {
            var taskList = new List<Task>();

            foreach (var user in Users)
            {
                taskList.Add((user as ChatUser).Write(message));
            }

            await Task.WhenAll(taskList.ToArray());
        }
    }
}
