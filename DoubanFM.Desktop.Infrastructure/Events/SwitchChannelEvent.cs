﻿using DoubanFM.Desktop.API.Models;
using Prism.Events;

namespace DoubanFM.Desktop.Infrastructure.Events
{
    public class SwitchChannelEvent : PubSubEvent<Channel>
    {
    }
}
