using System;
using System.Collections.Generic;

namespace Awesome
{
    public class Data
    {
        public List<User> Users { get; set; }
    }
    public class User
    {
        public long ChatId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class LogMS
    {
        public long ChatId;
        public List<MSlist> mSlists;
    }
    public class MSlist
    {
        public List<String> Messenges;
        public DateTime dateTime;
    }
}
