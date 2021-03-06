﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tch.Nuget.SlackClient.Domain.Exceptions;
using Tch.Nuget.SlackClient.Domain.Helpers;
using Tch.Nuget.SlackClient.Domain.Objects;
using Tch.Nuget.SlackClient.Interfaces.Helpers;
using Tch.Nuget.SlackClient.Interfaces.Internals;
using Tch.Nuget.SlackClient.Services.Helpers;

namespace Tch.Nuget.SlackClient.Services.Internals
{
   internal class CrudChannelService : ICrudChannelService
   {
      private class ListSlackChannelResponse : SlackHttpResponseBase
      {
         public SlackChannel[] Channels { get; set; }
      }

      private class CreateSlackChannelResponse : SlackHttpResponseBase
      {
         public SlackChannel Channel { get; set; }
      }

      private readonly ISlackHttpService _httpService;
      private readonly IHttpContentService _httpContentService;

      #region ctor

      public CrudChannelService() : this(new SlackHttpService(), new HttpContentService())
      {
      }

      protected CrudChannelService(ISlackHttpService httpService, IHttpContentService httpContentService)
      {
         _httpContentService = httpContentService ?? throw new ArgumentNullException(nameof(httpContentService));
         _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
      }

      #endregion

      public async Task<IEnumerable<SlackChannel>> GetAll(string token)
      {
         try
         {
            var response = await _httpService.Send<ListSlackChannelResponse>(HttpMethod.Get, "/conversations.list", token);
            return response.Channels;
         }
         catch
         {
            return Enumerable.Empty<SlackChannel>();
         }
      }

      public async Task<SlackChannel> Create(string token, string channelName)
      {
         var payload = new
         {
            name = channelName,
            is_private = false
         };

         var httpContent = _httpContentService.ToHttpContent(payload);

         var result = await _httpService.Send<CreateSlackChannelResponse>(HttpMethod.Post, "/conversations.create", token, httpContent);

         if (!result.Ok)
         {
            if (result.Error == "name_taken")
            {
               if (result.Channel == null)
               {
                  throw new SlackChannelAlreadyExistsException {ChannelName = channelName};
               }

               return result.Channel;
            }

            throw new SlackClientException($"Creation of Slack channel '{channelName}' failed") {Reason = result.Error};
         }

         return result.Channel;
      }

      public async Task CreateIfNotExists(string token, string channelName)
      {
         var channels = await GetAll(token);

         var selectedChannel = channels.FirstOrDefault(c => string.Equals(c.Name, channelName, StringComparison.InvariantCultureIgnoreCase));

         if (selectedChannel == null)
         {
            await Create(token, channelName);
         }
      }

      public async Task<SlackChannel> GetByName(string token, string channelName)
      {
         var channels = await GetAll(token);

         var selectedChannel = channels.FirstOrDefault(c => string.Equals(c.Name, channelName, StringComparison.InvariantCultureIgnoreCase));

         if (selectedChannel == null)
         {
            throw new SlackChannelNotFoundException {ChannelName = channelName};
         }

         return selectedChannel;
      }
   }
}